using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良品处理表体-不良列表
/// </summary>
[SugarTable("T_SFC_REJECTSETTLEENTRY")]
public class TSfcRejectsettleentry : BaseEntity
{
    /// <summary>
    /// 汇报不良表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 处理数量
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECOUNT")]
    public decimal Fsettlecount { get; set; }

    /// <summary>
    /// 汇报不良品数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTCOUNT")]
    public decimal Freportcount { get; set; }

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 条码号
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 是否批量
    /// </summary>
    [SugarColumn(ColumnName = "FISBATCH")]
    public bool Fisbatch { get; set; }

    /// <summary>
    /// 是否改判
    /// </summary>
    [SugarColumn(ColumnName = "FISMODIFY")]
    public bool Fismodify { get; set; }

    /// <summary>
    /// 归零情况
    /// </summary>
    [SugarColumn(ColumnName = "FZEROSITUATION")]
    public string Fzerosituation { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 不良品数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADCOUNT")]
    public decimal Fbadcount { get; set; }

    /// <summary>
    /// 不良条码表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADDETAILID")]
    public string Fbaddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 源条码号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBARCODE")]
    public string Fsourcebarcode { get; set; } = string.Empty;

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
