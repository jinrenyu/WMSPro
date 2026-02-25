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

    public AuthController(IAuthService authService, ILoginInfoService loginInfoService)
    {
        _authService = authService;
        _loginInfoService = loginInfoService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        var result = await _authService.LoginAsync(request);

        // 记录登录审计
        await _loginInfoService.RecordLoginAsync(request.UserId, ipAddress, userAgent, result.Success, result.Message);

        if (!result.Success)
        {
            return Ok(ApiResponse<LoginResponse>.Fail(result.Message));
        }

        return Ok(ApiResponse<LoginResponse>.Ok(result, result.Message));
    }

    [HttpPost("refresh")]
    [Authorize]
    public ActionResult<ApiResponse<object>> Refresh()
    {
        // Token 刷新 - 简单实现：前端在 token 即将过期时重新登录
        return Ok(ApiResponse<object>.Fail("请重新登录获取新Token", 401));
    }

    [HttpPost("logout")]
    [Authorize]
    public ActionResult<ApiResponse<object>> Logout()
    {
        // JWT 无状态，客户端删除 token 即可
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
}
