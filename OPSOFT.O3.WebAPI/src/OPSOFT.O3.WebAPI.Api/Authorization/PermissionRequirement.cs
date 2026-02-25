using Microsoft.AspNetCore.Authorization;

namespace OPSOFT.O3.WebAPI.Api.Authorization;

/// <summary>
/// 权限需求
/// </summary>
public class PermissionRequirement : IAuthorizationRequirement
{
    public string PermissionCode { get; }

    public PermissionRequirement(string permissionCode)
    {
        PermissionCode = permissionCode;
    }
}
