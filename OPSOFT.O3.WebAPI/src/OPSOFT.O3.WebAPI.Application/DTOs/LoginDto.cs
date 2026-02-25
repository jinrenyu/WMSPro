using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class LoginRequest
{
    [Required(ErrorMessage = "用户ID不能为空")]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }
    public UserInfo? UserInfo { get; set; }
}

public class UserInfo
{
    public string Uid { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;
    public string PaType { get; set; } = string.Empty;
    public string PaId { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
    public List<UserOrgInfo> Organizations { get; set; } = new();
}

public class UserOrgInfo
{
    public string OrgId { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
}

public class RefreshTokenRequest
{
    [Required(ErrorMessage = "Token不能为空")]
    public string Token { get; set; } = string.Empty;
}
