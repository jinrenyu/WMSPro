using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 送修单表体-维修单
/// </summary>
[SugarTable("T_SFC_REPAIRORDERENTRY")]
public class TSfcRepairorderentry : BaseEntity
{
    /// <summary>
    /// 维修单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRID")]
    public string Frepairid { get; set; } = string.Empty;

    /// <summary>
    /// 维修处理量
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRQTY")]
    public decimal Frepairqty { get; set; }

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
