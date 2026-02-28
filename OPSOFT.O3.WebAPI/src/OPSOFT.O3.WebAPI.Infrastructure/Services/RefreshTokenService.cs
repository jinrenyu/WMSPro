using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repository;
    private readonly ILoginUserRepository _loginUserRepository;
    private readonly IJwtService _jwtService;
    private readonly ISqlSugarClient _db;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RefreshTokenService> _logger;

    public RefreshTokenService(
        IRefreshTokenRepository repository,
        ILoginUserRepository loginUserRepository,
        IJwtService jwtService,
        ISqlSugarClient db,
        IConfiguration configuration,
        ILogger<RefreshTokenService> logger)
    {
        _repository = repository;
        _loginUserRepository = loginUserRepository;
        _jwtService = jwtService;
        _db = db;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> GenerateRefreshTokenAsync(string userUid, string userId, string ipAddress, string userAgent)
    {
        // 撤销该用户旧的 refresh token
        await _repository.RevokeByUserUidAsync(userUid, "新登录");

        var refreshTokenDays = _configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationDays", 7);

        // 生成随机 token
        var rawToken = GenerateRawToken();
        var tokenHash = ComputeSha256Hash(rawToken);

        var now = DateTime.Now;
        var entity = new SysRefreshToken
        {
            Uid = Guid.NewGuid().ToString(),
            UserUid = userUid,
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = now.AddDays(refreshTokenDays),
            CreatedIp = ipAddress ?? string.Empty,
            CreatedUserAgent = userAgent?.Length > 500 ? userAgent[..500] : userAgent ?? string.Empty,
            CYmd = now,
            CUser = userId,
            MYmd = now,
            MUser = userId
        };

        await _repository.InsertAsync(entity);

        _logger.LogInformation("为用户 {UserId} 生成了新的 Refresh Token", userId);

        return rawToken;
    }

    public async Task<(string AccessToken, string RefreshToken)?> RefreshAsync(string refreshToken, string ipAddress, string userAgent)
    {
        var tokenHash = ComputeSha256Hash(refreshToken);
        var storedToken = await _repository.GetByTokenHashAsync(tokenHash);

        if (storedToken == null)
        {
            _logger.LogWarning("Refresh Token 不存在或已撤销");
            return null;
        }

        if (storedToken.ExpiresAt < DateTime.Now)
        {
            _logger.LogWarning("Refresh Token 已过期，用户: {UserId}", storedToken.UserId);
            await _repository.RevokeByTokenHashAsync(tokenHash, "已过期");
            return null;
        }

        // 查询用户信息
        var user = await _loginUserRepository.GetByUserIdAsync(storedToken.UserId);
        if (user == null || user.FDeleted || user.FStatus != 0)
        {
            _logger.LogWarning("用户不存在或已被禁用: {UserId}", storedToken.UserId);
            await _repository.RevokeByUserUidAsync(storedToken.UserUid, "用户已禁用或删除");
            return null;
        }

        // 查询用户角色
        var roleRelations = await _db.Queryable<SysUserRoleRelation>()
            .Where(r => r.UserId == user.UserId && !r.FDeleted)
            .ToListAsync();

        var roleIds = roleRelations.Select(r => r.Froleid).ToList();
        var roleNames = new List<string>();
        if (roleIds.Any())
        {
            roleNames = await _db.Queryable<SysUserRole>()
                .Where(r => roleIds.Contains(r.Uid) && !r.FDeleted)
                .Select(r => r.Frolename)
                .ToListAsync();
        }

        // 生成新的 access token
        var accessTokenMinutes = _configuration.GetValue<int>("JwtSettings:AccessTokenExpirationMinutes", 15);
        var newAccessToken = _jwtService.GenerateToken(
            user.Uid, user.UserId, user.UserName, user.FCompanyId,
            roleNames, user.PaType, user.PaId,
            accessTokenMinutes);

        // Token 轮换：撤销旧 token，生成新 refresh token
        await _repository.RevokeByTokenHashAsync(tokenHash, "Token 轮换");

        var newRefreshToken = await GenerateRefreshTokenAsync(
            storedToken.UserUid, storedToken.UserId, ipAddress, userAgent);

        _logger.LogInformation("用户 {UserId} 的 Token 刷新成功", storedToken.UserId);

        return (newAccessToken, newRefreshToken);
    }

    public async Task RevokeByUserUidAsync(string userUid, string reason)
    {
        await _repository.RevokeByUserUidAsync(userUid, reason);
    }

    public async Task RevokeByTokenAsync(string refreshToken, string reason)
    {
        var tokenHash = ComputeSha256Hash(refreshToken);
        await _repository.RevokeByTokenHashAsync(tokenHash, reason);
    }

    private static string GenerateRawToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private static string ComputeSha256Hash(string rawToken)
    {
        var bytes = Encoding.UTF8.GetBytes(rawToken);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexStringLower(hash);
    }
}
