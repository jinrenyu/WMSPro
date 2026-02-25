using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 年度结转之备件信息
/// </summary>
[SugarTable("T_ENG_EQANNUALCARRYFORWARDENTRY")]
public class TEngEqannualcarryforwardentry : BaseEntity
{
    /// <summary>
    /// 本年
    /// </summary>
    [SugarColumn(ColumnName = "FYEAR")]
    public int Fyear { get; set; }

    /// <summary>
    /// 备件内码
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTSID")]
    public string Fsparepartsid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public int Famount { get; set; }

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
