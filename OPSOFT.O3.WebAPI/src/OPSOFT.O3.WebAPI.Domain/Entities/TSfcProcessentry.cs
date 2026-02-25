using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序表体-不良原因
/// </summary>
[SugarTable("T_SFC_PROCESSENTRY")]
public class TSfcProcessentry : BaseEntity
{
    /// <summary>
    /// 不良原因内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREASONID")]
    public string Fbadreasonid { get; set; } = string.Empty;

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
