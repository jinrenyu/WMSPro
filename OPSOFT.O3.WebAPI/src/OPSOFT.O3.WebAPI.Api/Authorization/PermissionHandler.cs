using Microsoft.AspNetCore.Authorization;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Authorization;

/// <summary>
/// 权限验证处理器
/// </summary>
public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ISqlSugarClient _db;

    public PermissionHandler(ISqlSugarClient db)
    {
        _db = db;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            context.Fail();
            return;
        }

        // superadmin 拥有所有权限，直接放行
        if (userId == "superadmin")
        {
            context.Succeed(requirement);
            return;
        }

        // 查询用户角色
        var roleIds = await _db.Queryable<SysUserRoleRelation>()
            .Where(r => r.UserId == userId && !r.FDeleted)
            .Select(r => r.Froleid)
            .ToListAsync();

        if (!roleIds.Any())
        {
            context.Fail();
            return;
        }

        // 查询角色是否拥有指定权限
        var hasPermission = await _db.Queryable<SysUserPermission>()
            .Where(p => roleIds.Contains(p.Froleid) && p.PrCode == requirement.PermissionCode && !p.FDeleted)
            .AnyAsync();

        if (!hasPermission)
        {
            context.Fail();
            return;
        }

        // 检查该权限对应的菜单按钮是否被禁用
        var isDisabled = await _db.Queryable<SysMenu>()
            .Where(m => m.MenuType == "B" && m.PermCode == requirement.PermissionCode && !m.FDeleted && m.FStatus != 0)
            .AnyAsync();

        if (isDisabled)
        {
            context.Fail();
        }
        else
        {
            context.Succeed(requirement);
        }
    }
}
