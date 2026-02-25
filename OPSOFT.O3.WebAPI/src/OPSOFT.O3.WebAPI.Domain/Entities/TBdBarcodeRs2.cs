using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档-成品源单表
/// </summary>
[SugarTable("T_BD_BARCODERS2")]
public class TBdBarcoders2 : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单内码【主单据】
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public int Fmoentryid { get; set; }

    /// <summary>
    /// 生产任务单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 客户内码
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTID")]
    public string Fcustid { get; set; } = string.Empty;

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
    /// 汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID")]
    public string Freportid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单单号
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTBILLNO")]
    public string Freportbillno { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单行号
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTENTRYID")]
    public int Freportentryid { get; set; }

    /// <summary>
    /// 汇报类型
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTTYPE")]
    public int Freporttype { get; set; }
}
