using Microsoft.AspNetCore.Authorization;

namespace OPSOFT.O3.WebAPI.Api.Authorization;

/// <summary>
/// 权限验证特性 - 用法: [RequirePermission("P_USER_CREATE")]
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequirePermissionAttribute : AuthorizeAttribute
{
    public RequirePermissionAttribute(string permissionCode)
        : base(policy: $"Permission_{permissionCode}")
    {
    }
}
