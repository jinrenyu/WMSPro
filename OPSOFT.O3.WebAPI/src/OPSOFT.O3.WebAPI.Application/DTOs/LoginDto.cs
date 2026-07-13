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
    public string? RefreshToken { get; set; }
    public UserInfo? UserInfo { get; set; }

    /// <summary>密码正确但需短信MFA第二步(此时不返回 Token)</summary>
    public bool MfaRequired { get; set; }
    /// <summary>MFA 一次性票据(用于发码/校验)</summary>
    public string? MfaTicket { get; set; }
    /// <summary>掩码手机号(如 138****5678)，用于前端提示</summary>
    public string? MaskedMobile { get; set; }
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
    public string RefreshToken { get; set; } = string.Empty;
}

public class RefreshTokenResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>发送短信验证码请求(登录第二步)</summary>
public class SendSmsCodeRequest
{
    [Required(ErrorMessage = "登录票据不能为空")]
    public string MfaTicket { get; set; } = string.Empty;
}

/// <summary>校验短信验证码请求(登录第三步)</summary>
public class VerifyMfaRequest
{
    [Required(ErrorMessage = "登录票据不能为空")]
    public string MfaTicket { get; set; } = string.Empty;

    [Required(ErrorMessage = "验证码不能为空")]
    public string Code { get; set; } = string.Empty;
}
