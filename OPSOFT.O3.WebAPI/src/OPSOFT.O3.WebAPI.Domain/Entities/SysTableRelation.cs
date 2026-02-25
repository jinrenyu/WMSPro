using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统表结构之间的关系
/// </summary>
[SugarTable("SYS_TABLERELATION")]
public class SysTableRelation : BaseEntity
{
    /// <summary>
    /// 子表内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTABLEID")]
    public string Fstableid { get; set; } = string.Empty;

    /// <summary>
    /// 关联类别
    /// </summary>
    [SugarColumn(ColumnName = "FRETYPE")]
    public string Fretype { get; set; } = string.Empty;

    /// <summary>
    /// 主表关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FMTABLEKEY")]
    public string Fmtablekey { get; set; } = string.Empty;

    /// <summary>
    /// 子表关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FRTABLEKEY")]
    public string Frtablekey { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
