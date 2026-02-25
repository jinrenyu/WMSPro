using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 发货通知条码明细
/// </summary>
[SugarTable("T_SAL_DELIVERYNOTICEENTRY1")]
public class TSalDeliverynoticeentry1 : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 仓库内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROID")]
    public string Fauxproid { get; set; } = string.Empty;

    /// <summary>
    /// 客户内码
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMERID")]
    public string Fcustomerid { get; set; } = string.Empty;

    /// <summary>
    /// 发货通知单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSALDELIVERYNOTICEID")]
    public string Fsaldeliverynoticeid { get; set; } = string.Empty;

    /// <summary>
    /// 发货通知单号
    /// </summary>
    [SugarColumn(ColumnName = "FSALDELIVERYNOTICEBILLNO")]
    public string Fsaldeliverynoticebillno { get; set; } = string.Empty;

    /// <summary>
    /// 发货通知单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSALDELIVERYNOTICEDETAILID")]
    public string Fsaldeliverynoticedetailid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERBILLNO")]
    public string Forderbillno { get; set; } = string.Empty;

    /// <summary>
    /// 订单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERDETAILID")]
    public string Forderdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 订单明细行号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERENTRYID")]
    public int Forderentryid { get; set; }

    /// <summary>
    /// 订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERINTERID")]
    public string Forderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
