using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户组织
/// </summary>
[SugarTable("SYS_USERORG")]
public class SysUserOrg : BaseEntity
{
    /// <summary>
    /// 用户账号
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 组织内码
    /// </summary>
    [SugarColumn(ColumnName = "FORGID")]
    public string Forgid { get; set; } = string.Empty;

    /// <summary>
    /// 默认组织
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULT")]
    public bool Fisdefault { get; set; }
}
