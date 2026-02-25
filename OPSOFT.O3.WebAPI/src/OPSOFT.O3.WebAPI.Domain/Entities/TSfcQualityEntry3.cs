using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// MES检验单记录项目
/// </summary>
[SugarTable("T_SFC_QUALITY_ENTRY3")]
public class TSfcQualityEntry3 : BaseEntity
{
    /// <summary>
    /// 记录项目
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTITEMLOGID")]
    public string Finspectitemlogid { get; set; } = string.Empty;

    /// <summary>
    /// 记录内容
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
