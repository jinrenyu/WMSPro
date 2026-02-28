namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface IRefreshTokenService
{
    /// <summary>
    /// 生成 Refresh Token（返回原始 token 字符串，数据库存哈希）
    /// </summary>
    Task<string> GenerateRefreshTokenAsync(string userUid, string userId, string ipAddress, string userAgent);

    /// <summary>
    /// 验证并刷新 Token（返回新的 access token 和 refresh token）
    /// </summary>
    Task<(string AccessToken, string RefreshToken)?> RefreshAsync(string refreshToken, string ipAddress, string userAgent);

    /// <summary>
    /// 撤销指定用户的所有 Refresh Token
    /// </summary>
    Task RevokeByUserUidAsync(string userUid, string reason);

    /// <summary>
    /// 撤销指定 Refresh Token
    /// </summary>
    Task RevokeByTokenAsync(string refreshToken, string reason);
}
