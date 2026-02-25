using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PermissionController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public PermissionController(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取权限定义树（顶级目录 → 二级菜单/目录 → [三级菜单] → 按钮权限）
    /// </summary>
    [HttpGet("definitions")]
    public async Task<ActionResult<ApiResponse<List<PermissionGroupDto>>>> GetDefinitions()
    {
        var allMenus = await _db.Queryable<SysMenu>()
            .Where(m => !m.FDeleted && m.FStatus == 0)
            .OrderBy(m => m.SortOrder)
            .ToListAsync();

        var menuDict = allMenus.ToDictionary(m => m.Uid);
        var buttons = allMenus.Where(m => m.MenuType == "B" && !string.IsNullOrEmpty(m.PermCode)).ToList();

        // 按 顶级目录 → 二级模块 → (可选三级菜单) 分组
        var groups = new Dictionary<string, PermissionGroupDto>();

        foreach (var btn in buttons)
        {
            // 沿 ParentId 向上收集祖先链（不含按钮自身）
            var ancestors = new List<SysMenu>();
            var current = btn;
            while (!string.IsNullOrEmpty(current.ParentId) && menuDict.TryGetValue(current.ParentId, out var parent))
            {
                ancestors.Add(parent);
                current = parent;
            }
            // ancestors: [直接父菜单, ..., 顶级目录]
            ancestors.Reverse(); // 变成 [顶级目录, ..., 直接父菜单]

            if (ancestors.Count < 2) continue; // 至少需要顶级目录 + 一个菜单

            var topMenu = ancestors[0];
            var topKey = topMenu.Uid;
            if (!groups.TryGetValue(topKey, out var group))
            {
                group = new PermissionGroupDto
                {
                    GroupName = topMenu.MenuName,
                    Icon = topMenu.Icon,
                    Modules = new List<PermissionModuleDto>()
                };
                groups[topKey] = group;
            }

            // 二级节点
            var level2 = ancestors[1];
            var module = group.Modules.FirstOrDefault(m => m.ModuleId == level2.Uid);
            if (module == null)
            {
                module = new PermissionModuleDto
                {
                    ModuleId = level2.Uid,
                    ModuleName = level2.MenuName,
                    IsDirectory = level2.MenuType == "D",
                    Permissions = new List<PermissionItemDto>(),
                    Children = new List<PermissionModuleDto>()
                };
                group.Modules.Add(module);
            }

            if (ancestors.Count >= 3)
            {
                // 有三级菜单：按钮挂在三级菜单下
                var level3 = ancestors[2];
                var child = module.Children.FirstOrDefault(c => c.ModuleId == level3.Uid);
                if (child == null)
                {
                    child = new PermissionModuleDto
                    {
                        ModuleId = level3.Uid,
                        ModuleName = level3.MenuName,
                        IsDirectory = false,
                        Permissions = new List<PermissionItemDto>(),
                        Children = new List<PermissionModuleDto>()
                    };
                    module.Children.Add(child);
                }
                child.Permissions.Add(new PermissionItemDto { Code = btn.PermCode, Name = btn.MenuName });
            }
            else
            {
                // 二级菜单直接挂按钮
                module.Permissions.Add(new PermissionItemDto { Code = btn.PermCode, Name = btn.MenuName });
            }
        }

        return Ok(ApiResponse<List<PermissionGroupDto>>.Ok(groups.Values.ToList()));
    }
}

/// <summary>
/// 权限分组（顶级目录）
/// </summary>
public class PermissionGroupDto
{
    public string GroupName { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<PermissionModuleDto> Modules { get; set; } = new();
}

/// <summary>
/// 权限模块（二级/三级菜单）
/// </summary>
public class PermissionModuleDto
{
    public string ModuleId { get; set; } = string.Empty;
    public string ModuleName { get; set; } = string.Empty;
    public bool IsDirectory { get; set; }
    public List<PermissionItemDto> Permissions { get; set; } = new();
    public List<PermissionModuleDto> Children { get; set; } = new();
}

/// <summary>
/// 权限项（按钮）
/// </summary>
public class PermissionItemDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
