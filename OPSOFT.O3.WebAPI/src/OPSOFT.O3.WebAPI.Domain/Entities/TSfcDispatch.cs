using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 派工表
/// </summary>
[SugarTable("T_SFC_DISPATCH")]
public class TSfcDispatch : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡编号
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDBILLNO")]
    public string Fflowcardbillno { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// (此工序)数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 工单（任务单）内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 工单（任务单）编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 工单（任务单）明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 单位标准工时(分
    /// </summary>
    [SugarColumn(ColumnName = "FSTADTIME")]
    public decimal Fstadtime { get; set; }
}
