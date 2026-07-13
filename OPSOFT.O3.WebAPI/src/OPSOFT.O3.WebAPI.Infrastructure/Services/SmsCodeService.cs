using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Infrastructure.Sms;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 短信验证码 / 登录MFA 服务实现。验证码与票据均以 SHA256 哈希落库(SYS_SMSCODE)，不存明文。
/// </summary>
public class SmsCodeService : ISmsCodeService
{
    private readonly ISqlSugarClient _db;
    private readonly ISmsSender _smsSender;
    private readonly SmsOptions _options;
    private readonly ILogger<SmsCodeService> _logger;

    // 按用户串行化发码的进程内闸门(闭合发码 TOCTOU 竞态；单实例部署有效，多实例需改分布式锁)
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _userSendGates = new();

    public SmsCodeService(
        ISqlSugarClient db,
        ISmsSender smsSender,
        IOptions<SmsOptions> options,
        ILogger<SmsCodeService> logger)
    {
        _db = db;
        _smsSender = smsSender;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<MfaSessionResult?> TryBeginMfaAsync(string userUid, string userId, string? ip)
    {
        // 全局总开关关闭：所有用户仅密码登录
        if (!_options.Enabled) return null;

        // 读该用户的 MFA 配置（独立表 SYS_USERMFA，不侵入 SYS_LOGINUSER）
        var cfg = await _db.Queryable<SysUserMfa>()
            .Where(x => x.UserId == userId && !x.FDeleted)
            .FirstAsync();
        if (cfg == null || cfg.MfaEnabled != 1) return null;
        if (string.IsNullOrWhiteSpace(cfg.Mobile))
        {
            _logger.LogWarning("用户 {UserId} 已启用短信MFA但未维护手机号，本次降级为仅密码登录", userId);
            return null;
        }

        // 创建一次性 MFA 会话（票据）
        var rawTicket = GenerateRawToken();
        var now = DateTime.Now;

        // 单活跃会话：作废该用户此前未消费的登录会话，避免同一用户多张有效票据并存
        await _db.Updateable<SysSmsCode>()
            .SetColumns(x => x.IsConsumed == true)
            .SetColumns(x => x.ConsumedAt == now)
            .SetColumns(x => x.MYmd == now)
            .Where(x => x.UserId == userId && x.Purpose == "login" && !x.IsConsumed && !x.FDeleted)
            .ExecuteCommandAsync();

        var sessionMinutes = _options.SessionExpireMinutes <= 0 ? 10 : _options.SessionExpireMinutes;
        var entity = new SysSmsCode
        {
            Uid = Guid.NewGuid().ToString(),
            FInterId = Guid.NewGuid().ToString("N"),
            UserUid = userUid,
            UserId = userId,
            Mobile = cfg.Mobile,
            TicketHash = ComputeSha256Hash(rawTicket),
            CodeHash = string.Empty,
            Purpose = "login",
            ExpiresAt = now.AddMinutes(sessionMinutes),
            SendCount = 0,
            AttemptCount = 0,
            IsConsumed = false,
            CreatedIp = ip ?? string.Empty,
            CYmd = now,
            CUser = userId,
            MYmd = now,
            MUser = userId
        };
        await _db.Insertable(entity).ExecuteCommandAsync();
        return new MfaSessionResult { Ticket = rawTicket, MaskedMobile = MaskMobile(cfg.Mobile) };
    }

    public async Task<SmsCodeSendResult> SendCodeAsync(string ticket, string? ip)
    {
        var probe = await GetActiveSessionAsync(ticket);
        if (probe == null)
            return new SmsCodeSendResult { Success = false, Message = "登录会话已失效，请重新登录" };

        // 同一用户串行化发码：闭合「查频控→发送→写时间戳」非原子导致的 TOCTOU 并发绕过(短信轰炸/费用放大)
        var gate = _userSendGates.GetOrAdd(probe.UserId, _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync();
        try
        {
            // 进入临界区后重取会话，确保 SendCount 等为最新值
            var session = await GetActiveSessionAsync(ticket);
            if (session == null)
                return new SmsCodeSendResult { Success = false, Message = "登录会话已失效，请重新登录" };

            var now = DateTime.Now;

            // 单会话发送次数上限
            if (session.SendCount >= _options.MaxSendCount)
                return new SmsCodeSendResult { Success = false, Message = "验证码发送次数过多，请稍后重新登录" };

            // 跨会话发送频控（按用户）：防止反复调用 /auth/login 新建会话来绕过单会话间隔，
            // 造成对同一手机号的短信轰炸与短信费用滥用。以 CodeSentAt 时间戳跨所有会话行统计。
            var intervalStart = now.AddSeconds(-_options.SendIntervalSeconds);
            var sentWithinInterval = await _db.Queryable<SysSmsCode>()
                .Where(x => x.UserId == session.UserId && x.CodeSentAt != null && x.CodeSentAt > intervalStart)
                .AnyAsync();
            if (sentWithinInterval)
                return new SmsCodeSendResult { Success = false, Message = $"验证码发送过于频繁，请 {_options.SendIntervalSeconds} 秒后再试", CooldownSeconds = _options.SendIntervalSeconds };

            // 跨会话当日发送上限（按用户）
            var dayStart = now.Date;
            var dailyLimit = _options.DailySendLimit <= 0 ? 10 : _options.DailySendLimit;
            var sentToday = await _db.Queryable<SysSmsCode>()
                .Where(x => x.UserId == session.UserId && x.CodeSentAt != null && x.CodeSentAt >= dayStart)
                .CountAsync();
            if (sentToday >= dailyLimit)
                return new SmsCodeSendResult { Success = false, Message = "今日验证码发送次数已达上限，请明日再试或联系管理员" };

            var code = GenerateNumericCode(_options.CodeLength <= 0 ? 6 : _options.CodeLength);
            var sendResult = await _smsSender.SendAsync(session.Mobile, code);
            if (!sendResult.Success)
            {
                // 下游发送失败也推进频控时间戳与次数，避免失败可被无限高频重试冲击短信网关
                session.CodeSentAt = now;
                session.SendCount += 1;
                session.MYmd = now;
                await _db.Updateable(session)
                    .UpdateColumns(x => new { x.CodeSentAt, x.SendCount, x.MYmd })
                    .ExecuteCommandAsync();
                _logger.LogWarning("短信验证码发送失败 用户={UserId} 错误码={Code} 原因={Message}", session.UserId, sendResult.Code, sendResult.Message);
                return new SmsCodeSendResult { Success = false, Message = MapSendError(sendResult.Code) };
            }

            // 发送成功：写入验证码哈希，记录发码时间(用于验证码独立过期)，重置校验尝试次数，累计发送次数
            session.CodeHash = ComputeSha256Hash(code);
            session.CodeSentAt = now;
            session.SendCount += 1;
            session.AttemptCount = 0;
            session.MYmd = now;
            await _db.Updateable(session)
                .UpdateColumns(x => new { x.CodeHash, x.CodeSentAt, x.SendCount, x.AttemptCount, x.MYmd })
                .ExecuteCommandAsync();

            return new SmsCodeSendResult { Success = true, Message = "验证码已发送", CooldownSeconds = _options.SendIntervalSeconds };
        }
        finally
        {
            gate.Release();
        }
    }

    public async Task<MfaVerifyResult> VerifyCodeAsync(string ticket, string code)
    {
        var session = await GetActiveSessionAsync(ticket);
        if (session == null)
            return new MfaVerifyResult { Success = false, Message = "登录会话已失效，请重新登录" };
        if (string.IsNullOrEmpty(session.CodeHash))
            return new MfaVerifyResult { Success = false, Message = "请先获取短信验证码" };

        // 验证码独立过期：会话(票据)有效期通常长于验证码有效期，旧码不得在会话存活期内一直可用
        var codeExpireMinutes = _options.ExpireMinutes <= 0 ? 5 : _options.ExpireMinutes;
        if (session.CodeSentAt == null || session.CodeSentAt.Value.AddMinutes(codeExpireMinutes) < DateTime.Now)
            return new MfaVerifyResult { Success = false, Message = "验证码已过期，请重新获取" };

        if (session.AttemptCount >= _options.MaxVerifyAttempts)
        {
            await ConsumeAsync(session);
            return new MfaVerifyResult { Success = false, Message = "验证码错误次数过多，请重新登录" };
        }

        var inputHash = ComputeSha256Hash((code ?? string.Empty).Trim());
        var matched = CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(inputHash),
            Encoding.UTF8.GetBytes(session.CodeHash));

        if (!matched)
        {
            session.AttemptCount += 1;
            session.MYmd = DateTime.Now;
            await _db.Updateable(session).UpdateColumns(x => new { x.AttemptCount, x.MYmd }).ExecuteCommandAsync();
            var remaining = Math.Max(0, _options.MaxVerifyAttempts - session.AttemptCount);
            return new MfaVerifyResult { Success = false, Message = $"验证码错误，还剩 {remaining} 次机会", RemainingAttempts = remaining };
        }

        await ConsumeAsync(session);
        return new MfaVerifyResult { Success = true, Message = "验证通过", UserId = session.UserId };
    }

    // ==================== helpers ====================

    private async Task<SysSmsCode?> GetActiveSessionAsync(string ticket)
    {
        if (string.IsNullOrWhiteSpace(ticket)) return null;
        var hash = ComputeSha256Hash(ticket);
        var now = DateTime.Now;
        return await _db.Queryable<SysSmsCode>()
            .Where(x => x.TicketHash == hash && !x.IsConsumed && !x.FDeleted && x.ExpiresAt > now)
            .OrderBy(x => x.CYmd, OrderByType.Desc)
            .FirstAsync();
    }

    private async Task ConsumeAsync(SysSmsCode session)
    {
        session.IsConsumed = true;
        session.ConsumedAt = DateTime.Now;
        session.MYmd = DateTime.Now;
        await _db.Updateable(session).UpdateColumns(x => new { x.IsConsumed, x.ConsumedAt, x.MYmd }).ExecuteCommandAsync();
    }

    private static string MapSendError(string? code) => code switch
    {
        "isv.BUSINESS_LIMIT_CONTROL" => "验证码发送过于频繁，请稍后再试",
        "isv.MOBILE_NUMBER_ILLEGAL" => "手机号格式不正确",
        "isv.OUT_OF_SERVICE" or "isv.AMOUNT_NOT_ENOUGH" => "短信服务暂不可用，请联系管理员",
        _ => "验证码发送失败，请稍后重试"
    };

    private static string GenerateNumericCode(int length)
    {
        if (length < 4) length = 4;
        if (length > 8) length = 8;
        var min = (int)Math.Pow(10, length - 1);
        var max = (int)Math.Pow(10, length);
        return RandomNumberGenerator.GetInt32(min, max).ToString();
    }

    private static string GenerateRawToken()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToHexStringLower(bytes);
    }

    private static string ComputeSha256Hash(string raw)
    {
        var bytes = Encoding.UTF8.GetBytes(raw);
        return Convert.ToHexStringLower(SHA256.HashData(bytes));
    }

    private static string MaskMobile(string mobile)
    {
        if (string.IsNullOrEmpty(mobile)) return string.Empty;
        if (mobile.Length == 11) return $"{mobile[..3]}****{mobile[7..]}";
        if (mobile.Length > 4) return $"{mobile[..^4]}****";
        return "****";
    }
}
