using Microsoft.Extensions.Logging;

namespace OPSOFT.O3.WebAPI.Infrastructure.Sms;

/// <summary>
/// 开发 / 联调用短信实现：不真正发短信，只把验证码打到日志(Warning 级便于查看)。
/// 通过 SmsOptions.Provider=Stub 启用(默认)。
/// </summary>
public class StubSmsSender : ISmsSender
{
    private readonly ILogger<StubSmsSender> _logger;

    public StubSmsSender(ILogger<StubSmsSender> logger)
    {
        _logger = logger;
    }

    public Task<SmsSendResult> SendAsync(string phoneNumber, string code)
    {
        _logger.LogWarning("[StubSms] 模拟发送短信验证码 → 手机号 {Phone}，验证码 {Code}（开发模式不真实发送）", phoneNumber, code);
        return Task.FromResult(new SmsSendResult
        {
            Success = true,
            Code = "OK",
            Message = "stub",
            BizId = "STUB-" + Guid.NewGuid().ToString("N")[..8]
        });
    }
}
