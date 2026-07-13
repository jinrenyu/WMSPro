namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 短信验证码 / 登录MFA 服务。封装「是否需要MFA」判断(读 SYS_USERMFA)、会话票据创建、发码、校验，
/// 供 AuthService 在登录流程中调用（AuthService 不直接依赖短信实现、配置与 MFA 配置表）。
/// </summary>
public interface ISmsCodeService
{
    /// <summary>
    /// 密码校验通过后调用：若该用户需短信MFA(全局开关 &amp;&amp; 用户级开关 &amp;&amp; 已维护手机号)，
    /// 则创建一次性会话并返回票据 + 掩码手机号；否则返回 null（表示无需MFA、直接放行登录）。
    /// </summary>
    Task<MfaSessionResult?> TryBeginMfaAsync(string userUid, string userId, string? ip);

    /// <summary>按票据发送短信验证码(含发送频控)</summary>
    Task<SmsCodeSendResult> SendCodeAsync(string ticket, string? ip);

    /// <summary>按票据 + 验证码校验，成功返回用户账号</summary>
    Task<MfaVerifyResult> VerifyCodeAsync(string ticket, string code);
}

/// <summary>MFA 会话创建结果</summary>
public class MfaSessionResult
{
    public string Ticket { get; set; } = string.Empty;
    public string MaskedMobile { get; set; } = string.Empty;
}

/// <summary>验证码发送结果</summary>
public class SmsCodeSendResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    /// <summary>再次发送需等待的秒数(用于前端倒计时)</summary>
    public int CooldownSeconds { get; set; }
}

/// <summary>验证码校验结果</summary>
public class MfaVerifyResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public int RemainingAttempts { get; set; }
}
