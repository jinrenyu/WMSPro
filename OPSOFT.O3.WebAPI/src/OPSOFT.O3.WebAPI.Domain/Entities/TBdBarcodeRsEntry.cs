using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档-装箱清单
/// </summary>
[SugarTable("T_BD_BARCODERSENTRY")]
public class TBdBarcodersentry : BaseEntity
{
    /// <summary>
    /// 父阶条码
    /// </summary>
    [SugarColumn(ColumnName = "FBOXCODE")]
    public string Fboxcode { get; set; } = string.Empty;

    /// <summary>
    /// 子阶条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

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
