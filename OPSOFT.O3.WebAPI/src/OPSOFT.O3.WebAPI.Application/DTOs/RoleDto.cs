using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 角色列表 DTO
/// </summary>
public class RoleDto
{
    public string Uid { get; set; } = string.Empty;
    public string RoleNumber { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public int RoleType { get; set; }
    public string RoleTypeName { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
}

/// <summary>
/// 角色详情 DTO（含权限列表）
/// </summary>
public class RoleDetailDto : RoleDto
{
    public List<string> Permissions { get; set; } = new();
}

/// <summary>
/// 创建角色请求
/// </summary>
public class CreateRoleRequest
{
    [Required(ErrorMessage = "角色代码不能为空")]
    public string RoleNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "角色名称不能为空")]
    public string RoleName { get; set; } = string.Empty;

    public int RoleType { get; set; }
    public string Note { get; set; } = string.Empty;
}

/// <summary>
/// 更新角色请求
/// </summary>
public class UpdateRoleRequest
{
    [Required(ErrorMessage = "角色名称不能为空")]
    public string RoleName { get; set; } = string.Empty;

    public int RoleType { get; set; }
    public string Note { get; set; } = string.Empty;
}

/// <summary>
/// 分配权限请求
/// </summary>
public class AssignPermissionsRequest
{
    [Required(ErrorMessage = "权限代码列表不能为空")]
    public List<string> PermissionCodes { get; set; } = new();
}
