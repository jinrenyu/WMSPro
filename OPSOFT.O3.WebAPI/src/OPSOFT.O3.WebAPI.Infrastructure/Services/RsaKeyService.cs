using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// RSA 密钥服务 - 启动时生成密钥对，用于密码传输加密
/// </summary>
public class RsaKeyService : IRsaKeyService, IDisposable
{
    private readonly RSA _rsa;
    private readonly string _publicKeyPem;

    public RsaKeyService(ILogger<RsaKeyService> logger)
    {
        _rsa = RSA.Create(2048);
        _publicKeyPem = ExportPublicKeyPem();
        logger.LogInformation("RSA 2048-bit key pair generated for password encryption");
    }

    public string GetPublicKey() => _publicKeyPem;

    public string Decrypt(string encryptedBase64)
    {
        var encryptedBytes = Convert.FromBase64String(encryptedBase64);
        var decryptedBytes = _rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    private string ExportPublicKeyPem()
    {
        var publicKeyBytes = _rsa.ExportSubjectPublicKeyInfo();
        var base64 = Convert.ToBase64String(publicKeyBytes);

        var sb = new StringBuilder();
        sb.AppendLine("-----BEGIN PUBLIC KEY-----");
        for (var i = 0; i < base64.Length; i += 64)
        {
            sb.AppendLine(base64.Substring(i, Math.Min(64, base64.Length - i)));
        }
        sb.AppendLine("-----END PUBLIC KEY-----");

        return sb.ToString();
    }

    public void Dispose()
    {
        _rsa.Dispose();
    }
}
