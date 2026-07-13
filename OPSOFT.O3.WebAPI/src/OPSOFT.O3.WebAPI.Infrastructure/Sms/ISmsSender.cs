namespace OPSOFT.O3.WebAPI.Infrastructure.Sms;

/// <summary>
/// 短信发送抽象。按 SmsOptions.Provider 在 DI 中择一实现
/// (AliyunSmsSender 真实发送 / StubSmsSender 开发期打日志)，呼应 ERP 集成的 Stub/K3Cloud Provider 范式。
/// </summary>
public interface ISmsSender
{
    /// <summary>发送验证码短信</summary>
    Task<SmsSendResult> SendAsync(string phoneNumber, string code);
}

/// <summary>短信发送结果</summary>
public class SmsSendResult
{
    public bool Success { get; set; }
    /// <summary>服务商返回码(阿里云成功为 "OK")</summary>
    public string? Code { get; set; }
    public string? Message { get; set; }
    /// <summary>回执ID(阿里云 BizId)</summary>
    public string? BizId { get; set; }
}
