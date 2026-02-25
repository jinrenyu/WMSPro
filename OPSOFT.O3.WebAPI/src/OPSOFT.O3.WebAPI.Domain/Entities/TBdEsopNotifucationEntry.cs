using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP广播/通知表体
/// </summary>
[SugarTable("T_BD_ESOPNOTIFUCATIONENTRY")]
public class TBdEsopnotifucationentry : BaseEntity
{
    /// <summary>
    /// 工位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTATIONID")]
    public string Fstationid { get; set; } = string.Empty;

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
