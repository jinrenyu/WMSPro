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
    /// <summary>关联代码（职员/厂商/客户内码）</summary>
    public string PaId { get; set; } = string.Empty;
    /// <summary>是否智能终端用户</summary>
    public bool IsPda { get; set; }
    /// <summary>是否系统预置</summary>
    public bool IsDefault { get; set; }
    /// <summary>域用户帐号</summary>
    public string DomainUser { get; set; } = string.Empty;
    /// <summary>用户ID卡</summary>
    public string CardId { get; set; } = string.Empty;
    /// <summary>第三方ERP帐号</summary>
    public string ErpId { get; set; } = string.Empty;
    /// <summary>用户分组内码</summary>
    public string FGroupId { get; set; } = string.Empty;
    /// <summary>用户分组名称</summary>
    public string FGroupName { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}

/// <summary>
/// 用户详情 DTO
/// </summary>
public class UserDetailDto : UserListDto
{
    public string CompanyId { get; set; } = string.Empty;
    /// <summary>最后修改密码时间</summary>
    public DateTime? LastCpTime { get; set; }
    /// <summary>密码错误次数</summary>
    public int PwdErrTimes { get; set; }
    /// <summary>最后锁定/解锁时间</summary>
    public DateTime? LastLockTime { get; set; }
    /// <summary>用户照片（纯 Base64，无 dataURL 前缀）</summary>
    public string? PhotoBase64 { get; set; }
    public List<UserRoleDto> RoleDetails { get; set; } = new();
    public List<UserOrgDto> Organizations { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}

public class UserRoleDto
{
    public string RoleId { get; set; } = string.Empty;
    public string RoleNumber { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}

/// <summary>
/// 用户组织明细 DTO
/// </summary>
public class UserOrgDto
{
    public string OrgId { get; set; } = string.Empty;
    public string OrgNumber { get; set; } = string.Empty;
    public string OrgName { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
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
    public bool IsPda { get; set; }
    public string ErpId { get; set; } = string.Empty;
    public string CardId { get; set; } = string.Empty;
    public string DomainUser { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
    public string? PhotoBase64 { get; set; }
    public List<string>? RoleIds { get; set; }
    public List<string>? OrgIds { get; set; }
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
    public bool IsPda { get; set; }
    public string ErpId { get; set; } = string.Empty;
    public string CardId { get; set; } = string.Empty;
    public string DomainUser { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
    public string? PhotoBase64 { get; set; }
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

/// <summary>
/// 分配组织请求
/// </summary>
public class AssignOrgsRequest
{
    public List<string> OrgIds { get; set; } = new();
}
