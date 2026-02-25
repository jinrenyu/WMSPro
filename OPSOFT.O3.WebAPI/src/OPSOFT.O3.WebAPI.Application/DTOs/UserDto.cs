using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 用户列表 DTO
/// </summary>
public class UserListDto
{
    public string Uid { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public int LockStatus { get; set; }
    public DateTime? LastLoginTime { get; set; }
    public DateTime? CYmd { get; set; }
    public string PaType { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}

/// <summary>
/// 用户详情 DTO
/// </summary>
public class UserDetailDto : UserListDto
{
    public string PaId { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public string CompanyId { get; set; } = string.Empty;
    public List<UserRoleDto> RoleDetails { get; set; } = new();
    public List<UserOrgInfo> Organizations { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}

public class UserRoleDto
{
    public string RoleId { get; set; } = string.Empty;
    public string RoleNumber { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}

/// <summary>
/// 创建用户请求
/// </summary>
public class CreateUserRequest
{
    [Required(ErrorMessage = "用户ID不能为空")]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能少于6位")]
    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string PaType { get; set; } = string.Empty;
    public string PaId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public List<string>? RoleIds { get; set; }
}

/// <summary>
/// 更新用户请求
/// </summary>
public class UpdateUserRequest
{
    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string PaType { get; set; } = string.Empty;
    public string PaId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
}

/// <summary>
/// 修改密码请求
/// </summary>
public class ChangePasswordRequest
{
    [Required(ErrorMessage = "旧密码不能为空")]
    public string OldPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "新密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能少于6位")]
    public string NewPassword { get; set; } = string.Empty;
}

/// <summary>
/// 重置密码请求
/// </summary>
public class ResetPasswordRequest
{
    [Required(ErrorMessage = "用户ID不能为空")]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "新密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能少于6位")]
    public string NewPassword { get; set; } = string.Empty;
}

/// <summary>
/// 分配角色请求
/// </summary>
public class AssignRolesRequest
{
    [Required(ErrorMessage = "角色ID列表不能为空")]
    public List<string> RoleIds { get; set; } = new();
}
