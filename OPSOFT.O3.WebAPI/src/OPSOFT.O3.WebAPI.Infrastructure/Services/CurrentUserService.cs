using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 当前用户上下文服务 - 从 JWT Claims 中提取用户信息
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string? Uid => User?.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? User?.FindFirstValue("sub");

    public string? UserId => User?.FindFirstValue("userId");

    public string? UserName => User?.FindFirstValue("userName");

    public string? CompanyId => User?.FindFirstValue("companyId");

    public string? PaType => User?.FindFirstValue("paType");

    public string? PaId => User?.FindFirstValue("paId");

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
}
