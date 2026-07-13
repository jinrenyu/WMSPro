namespace OPSOFT.O3.WebAPI.Infrastructure.Sms;

/// <summary>
/// 短信 / MFA 配置节（appsettings: "Sms"）。
/// </summary>
public class SmsOptions
{
    public const string SectionName = "Sms";

    /// <summary>短信MFA全局总开关：false=完全不启用(所有用户仅密码登录)；true=对已启用MFA且有手机号的用户在登录时要求短信验证</summary>
    public bool Enabled { get; set; } = false;

    /// <summary>短信发送实现：Stub=开发期打日志不真发(默认) / Aliyun=阿里云真实发送</summary>
    public string Provider { get; set; } = "Stub";

    // ===== 阿里云短信参数（Provider=Aliyun 时生效）=====
    /// <summary>阿里云 AccessKeyId</summary>
    public string AccessKeyId { get; set; } = string.Empty;
    /// <summary>阿里云 AccessKeySecret（生产建议走环境变量覆盖，勿明文入库）</summary>
    public string AccessKeySecret { get; set; } = string.Empty;
    /// <summary>短信服务接入点</summary>
    public string Endpoint { get; set; } = "dysmsapi.aliyuncs.com";
    /// <summary>短信签名(需在阿里云控制台审核通过)</summary>
    public string SignName { get; set; } = string.Empty;
    /// <summary>短信模板 CODE(验证码模板)</summary>
    public string TemplateCode { get; set; } = string.Empty;
    /// <summary>模板中验证码变量名(模板含 ${code} 则填 code)</summary>
    public string TemplateParamName { get; set; } = "code";

    // ===== 验证码策略 =====
    /// <summary>验证码位数</summary>
    public int CodeLength { get; set; } = 6;
    /// <summary>验证码有效期(分钟)</summary>
    public int ExpireMinutes { get; set; } = 5;
    /// <summary>同一会话两次发送最小间隔(秒)，防频繁发送</summary>
    public int SendIntervalSeconds { get; set; } = 60;
    /// <summary>同一会话最多发送次数</summary>
    public int MaxSendCount { get; set; } = 5;
    /// <summary>同一用户当日最多发送次数(跨会话，防短信轰炸/费用滥用)</summary>
    public int DailySendLimit { get; set; } = 10;
    /// <summary>同一验证码最多校验尝试次数</summary>
    public int MaxVerifyAttempts { get; set; } = 5;
    /// <summary>MFA 会话(票据)有效期(分钟)</summary>
    public int SessionExpireMinutes { get; set; } = 10;
}
