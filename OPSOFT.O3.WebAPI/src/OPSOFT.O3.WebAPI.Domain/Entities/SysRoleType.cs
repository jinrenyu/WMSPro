using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 角色类别
/// </summary>
[SugarTable("SYS_ROLETYPE")]
public class SysRoleType : BaseEntity
{
    /// <summary>
    /// 类别代码
    /// </summary>
    [SugarColumn(ColumnName = "FROLETYPE")]
    public int Froletype { get; set; }

    /// <summary>
    /// 类别名称
    /// </summary>
    [SugarColumn(ColumnName = "FROLENAME")]
    public string Frolename { get; set; } = string.Empty;
}
