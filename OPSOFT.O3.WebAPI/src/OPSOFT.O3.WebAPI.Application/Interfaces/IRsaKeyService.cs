namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// RSA 密钥管理服务，用于密码传输加密
/// </summary>
public interface IRsaKeyService
{
    /// <summary>
    /// 获取 RSA 公钥 (PEM 格式)
    /// </summary>
    string GetPublicKey();

    /// <summary>
    /// 使用 RSA 私钥解密密文
    /// </summary>
    string Decrypt(string encryptedBase64);
}
