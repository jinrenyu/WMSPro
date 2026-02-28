using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILoginInfoService _loginInfoService;
    private readonly IRsaKeyService _rsaKeyService;

    public AuthController(IAuthService authService, ILoginInfoService loginInfoService, IRsaKeyService rsaKeyService)
    {
        _authService = authService;
        _loginInfoService = loginInfoService;
        _rsaKeyService = rsaKeyService;
    }

    /// <summary>
    /// 获取 RSA 公钥（用于密码加密传输）
    /// </summary>
    [HttpGet("public-key")]
    [AllowAnonymous]
    public ActionResult<ApiResponse<object>> GetPublicKey()
    {
        var publicKey = _rsaKeyService.GetPublicKey();
        return Ok(ApiResponse<object>.Ok(new { publicKey }));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest request)
    {
        request.Password = TryDecryptPassword(request.Password);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        var result = await _authService.LoginAsync(request, ipAddress, userAgent);

        // 记录登录审计
        await _loginInfoService.RecordLoginAsync(request.UserId, ipAddress, userAgent, result.Success, result.Message);

        if (!result.Success)
        {
            return Ok(ApiResponse<LoginResponse>.Fail(result.Message));
        }

        return Ok(ApiResponse<LoginResponse>.Ok(result, result.Message));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<RefreshTokenResponse>>> Refresh([FromBody] RefreshTokenRequest request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        var result = await _authService.RefreshTokenAsync(request.RefreshToken, ipAddress, userAgent);

        if (result == null)
        {
            return Ok(ApiResponse<RefreshTokenResponse>.Fail("Refresh Token 无效或已过期，请重新登录", 401));
        }

        return Ok(ApiResponse<RefreshTokenResponse>.Ok(result));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<object>>> Logout([FromBody] RefreshTokenRequest? request = null)
    {
        var userUid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("sub")?.Value;

        if (!string.IsNullOrEmpty(userUid))
        {
            await _authService.LogoutAsync(userUid, request?.RefreshToken);
        }

        return Ok(ApiResponse<object>.Ok(new { }, "退出成功"));
    }

    /// <summary>
    /// 获取当前用户的实时权限列表
    /// </summary>
    [HttpGet("permissions")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<List<string>>>> GetPermissions()
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
            return Ok(ApiResponse<List<string>>.Fail("未获取到用户信息", 401));

        var permissions = await _authService.GetCurrentPermissionsAsync(userId);
        return Ok(ApiResponse<List<string>>.Ok(permissions));
    }

    /// <summary>
    /// 尝试 RSA 解密密码，失败时返回原始密码（向后兼容）
    /// </summary>
    private string TryDecryptPassword(string password)
    {
        try
        {
            return _rsaKeyService.Decrypt(password);
        }
        catch (Exception ex) when (ex is FormatException or CryptographicException)
        {
            return password;
        }
    }
}
