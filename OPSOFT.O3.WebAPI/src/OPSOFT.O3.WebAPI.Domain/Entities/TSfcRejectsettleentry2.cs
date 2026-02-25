using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良品处理表体的表体-线上返工（流转卡单据）
/// </summary>
[SugarTable("T_SFC_REJECTSETTLEENTRY2")]
public class TSfcRejectsettleentry2 : BaseEntity
{
    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 历史流转卡记录（多个按照,隔开）
    /// </summary>
    [SugarColumn(ColumnName = "FOLDFLOWCARDID")]
    public string Foldflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 指定的工序流转卡工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FAPPOINTFLOWCARDDETAILID")]
    public string Fappointflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 创建新流转卡的内码
    /// </summary>
    [SugarColumn(ColumnName = "FNEWFLOWCARDID")]
    public string Fnewflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 创建的新条码
    /// </summary>
    [SugarColumn(ColumnName = "FNEWBARCODE")]
    public string Fnewbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

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
