using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序汇报
/// </summary>
[SugarTable("T_SFC_REPORT")]
public class TSfcReport : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID")]
    public string Fresourcesid { get; set; } = string.Empty;

    /// <summary>
    /// 班组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID")]
    public string Fteamid { get; set; } = string.Empty;

    /// <summary>
    /// 班次内码
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTID")]
    public string Fshiftid { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡号
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 任务单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码(工序流转卡中生成的唯一编码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;
}
