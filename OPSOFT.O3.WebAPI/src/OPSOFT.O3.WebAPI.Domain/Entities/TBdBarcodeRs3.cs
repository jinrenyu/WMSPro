using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档-委外源单表
/// </summary>
[SugarTable("T_BD_BARCODERS3")]
public class TBdBarcoders3 : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单内码【主单据】
    /// </summary>
    [SugarColumn(ColumnName = "FREQID")]
    public string Freqid { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FREQBILLNO")]
    public string Freqbillno { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FREQDETAILID")]
    public string Freqdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单表体行号
    /// </summary>
    [SugarColumn(ColumnName = "FREQENTRYID")]
    public int Freqentryid { get; set; }
}
