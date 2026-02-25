using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档-受托加工源单表
/// </summary>
[SugarTable("T_BD_BARCODERS5")]
public class TBdBarcoders5 : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 受托加工收料单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FOEMRECEIVEID")]
    public string Foemreceiveid { get; set; } = string.Empty;

    /// <summary>
    /// 受托加工收料单编号
    /// </summary>
    [SugarColumn(ColumnName = "FOEMRECEIVEBILLNO")]
    public string Foemreceivebillno { get; set; } = string.Empty;

    /// <summary>
    /// 受托加工收料表体行号
    /// </summary>
    [SugarColumn(ColumnName = "FOEMRECEIVEENTRYID")]
    public int Foemreceiveentryid { get; set; }

    /// <summary>
    /// 受托加工收料表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FOEMRECEIVEDETAILID")]
    public string Foemreceivedetailid { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FODID")]
    public string Fodid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FODDETAILID")]
    public string Foddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FODENTRYID")]
    public int Fodentryid { get; set; }

    /// <summary>
    /// 销售订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FODBILLNO")]
    public string Fodbillno { get; set; } = string.Empty;
}
