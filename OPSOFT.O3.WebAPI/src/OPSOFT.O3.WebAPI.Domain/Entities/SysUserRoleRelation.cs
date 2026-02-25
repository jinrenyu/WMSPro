using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 帐号及角色对照表
/// </summary>
[SugarTable("SYS_USERROLERELATION")]
public class SysUserRoleRelation : BaseEntity
{
    /// <summary>
    /// 用户帐号
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(ColumnName = "FROLEID")]
    public string Froleid { get; set; } = string.Empty;
}
