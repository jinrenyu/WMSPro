using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良品处理表体-改判列表
/// </summary>
[SugarTable("T_SFC_REJECTENTRYMODIFY")]
public class TSfcRejectentrymodify : BaseEntity
{
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
    /// 源条码号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBARCODE")]
    public string Fsourcebarcode { get; set; } = string.Empty;

    /// <summary>
    /// 条码号
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 不良原因内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREASONID")]
    public string Fbadreasonid { get; set; } = string.Empty;

    /// <summary>
    /// 历史不良原因
    /// </summary>
    [SugarColumn(ColumnName = "FHISBADREASONID")]
    public string Fhisbadreasonid { get; set; } = string.Empty;

    /// <summary>
    /// 改判结果
    /// </summary>
    [SugarColumn(ColumnName = "FMODRESULT")]
    public int Fmodresult { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 改判类型
    /// </summary>
    [SugarColumn(ColumnName = "FGPTYPE")]
    public int Fgptype { get; set; }

    /// <summary>
    /// 任务单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 指定的工序流转卡工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FAPPOINTFLOWCARDDETAILID")]
    public string Fappointflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 创建新流转卡的表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FNEWFLOWCARDID")]
    public string Fnewflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 指定放行工序创建的新条码
    /// </summary>
    [SugarColumn(ColumnName = "FNEWBARCODE")]
    public string Fnewbarcode { get; set; } = string.Empty;

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
