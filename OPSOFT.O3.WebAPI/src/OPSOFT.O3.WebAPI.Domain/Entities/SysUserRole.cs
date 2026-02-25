using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户角色信息
/// </summary>
[SugarTable("SYS_USERROLE")]
public class SysUserRole : BaseEntity
{
    /// <summary>
    /// 角色代码
    /// </summary>
    [SugarColumn(ColumnName = "FROLENUMBER")]
    public string Frolenumber { get; set; } = string.Empty;

    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(ColumnName = "FROLENAME")]
    public string Frolename { get; set; } = string.Empty;

    /// <summary>
    /// 角色类别
    /// </summary>
    [SugarColumn(ColumnName = "FROLETYPE")]
    public int Froletype { get; set; }

    /// <summary>
    /// 是否系统默认
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;
}
