using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户短信MFA配置表。一个登录用户一行，独立于 SYS_LOGINUSER（不侵入 K3 登录表）。
/// 存该用户的接收验证码手机号与是否启用MFA的用户级开关。
/// </summary>
[SugarTable("SYS_USERMFA")]
public class SysUserMfa : BaseEntity
{
    /// <summary>登录账号(关联 SYS_LOGINUSER.USER_ID)</summary>
    [SugarColumn(ColumnName = "USER_ID", Length = 20)]
    public string UserId { get; set; } = string.Empty;

    /// <summary>手机号（接收短信验证码）</summary>
    [SugarColumn(ColumnName = "MOBILE", Length = 30)]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>是否启用短信MFA(1=是, 0=否)</summary>
    [SugarColumn(ColumnName = "MFA_ENABLED")]
    public int MfaEnabled { get; set; }
}
