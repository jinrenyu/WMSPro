using System.Text.Json;
using AlibabaCloud.SDK.Dysmsapi20170525;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using AlibabaCloud.TeaUtil.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tea;

namespace OPSOFT.O3.WebAPI.Infrastructure.Sms;

/// <summary>
/// 阿里云短信(Dysmsapi)真实发送实现。通过 SmsOptions.Provider=Aliyun 启用。
/// </summary>
public class AliyunSmsSender : ISmsSender
{
    private readonly SmsOptions _options;
    private readonly ILogger<AliyunSmsSender> _logger;
    private Client? _client;

    public AliyunSmsSender(IOptions<SmsOptions> options, ILogger<AliyunSmsSender> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    private Client GetClient()
    {
        if (_client != null) return _client;
        var config = new AlibabaCloud.OpenApiClient.Models.Config
        {
            AccessKeyId = _options.AccessKeyId,
            AccessKeySecret = _options.AccessKeySecret,
            Endpoint = string.IsNullOrWhiteSpace(_options.Endpoint) ? "dysmsapi.aliyuncs.com" : _options.Endpoint
        };
        _client = new Client(config);
        return _client;
    }

    public async Task<SmsSendResult> SendAsync(string phoneNumber, string code)
    {
        var paramName = string.IsNullOrWhiteSpace(_options.TemplateParamName) ? "code" : _options.TemplateParamName;
        var request = new SendSmsRequest
        {
            PhoneNumbers = phoneNumber,
            SignName = _options.SignName,
            TemplateCode = _options.TemplateCode,
            TemplateParam = JsonSerializer.Serialize(new Dictionary<string, string> { [paramName] = code })
        };

        try
        {
            var resp = await GetClient().SendSmsWithOptionsAsync(request, new RuntimeOptions());
            var body = resp.Body;
            var ok = string.Equals(body?.Code, "OK", StringComparison.OrdinalIgnoreCase);
            if (!ok)
            {
                _logger.LogWarning("阿里云短信发送失败 手机={Phone} Code={Code} Message={Message}",
                    MaskPhone(phoneNumber), body?.Code, body?.Message);
            }
            return new SmsSendResult
            {
                Success = ok,
                Code = body?.Code,
                Message = body?.Message,
                BizId = body?.BizId
            };
        }
        catch (TeaException ex)
        {
            _logger.LogError(ex, "阿里云短信发送异常 手机={Phone} Code={Code}", MaskPhone(phoneNumber), ex.Code);
            return new SmsSendResult { Success = false, Code = ex.Code, Message = ex.Message };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "阿里云短信发送未知异常 手机={Phone}", MaskPhone(phoneNumber));
            return new SmsSendResult { Success = false, Code = "UNKNOWN", Message = ex.Message };
        }
    }

    private static string MaskPhone(string phone)
        => (!string.IsNullOrEmpty(phone) && phone.Length == 11) ? $"{phone[..3]}****{phone[7..]}" : phone;
}
