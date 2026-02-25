using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序汇报表体ENTRY的表体-不良信息
/// </summary>
[SugarTable("T_SFC_REPORTENTRY5")]
public class TSfcReportentry5 : BaseEntity
{
    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID")]
    public string Fresourcesid { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "FOPTIME")]
    public DateTime? Foptime { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 不良原因内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREASONSID")]
    public string Fbadreasonsid { get; set; } = string.Empty;

    /// <summary>
    /// 不良所属流转卡号
    /// </summary>
    [SugarColumn(ColumnName = "FPREFLOWCARDID")]
    public string Fpreflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 不良所属工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FPREFLOWCARDDETAILID")]
    public string Fpreflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 不良所属工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPREPROCESSID")]
    public string Fpreprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 不良所属工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FPRECODE")]
    public string Fprecode { get; set; } = string.Empty;

    /// <summary>
    /// 不良品数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 已经处理的数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTYSETTLED")]
    public decimal Fbadqtysettled { get; set; }

    /// <summary>
    /// 不良来源类型
    /// </summary>
    [SugarColumn(ColumnName = "FBADSOURCETYPE")]
    public int Fbadsourcetype { get; set; }

    /// <summary>
    /// 改判合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FMODIFYQTY")]
    public decimal Fmodifyqty { get; set; }

    /// <summary>
    /// 委外检验内码
    /// </summary>
    [SugarColumn(ColumnName = "FOUTINPECTIONID")]
    public string Foutinpectionid { get; set; } = string.Empty;

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

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
