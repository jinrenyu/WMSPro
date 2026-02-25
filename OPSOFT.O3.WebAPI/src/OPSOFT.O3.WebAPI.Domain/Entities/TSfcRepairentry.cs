using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报-送修单表体-不良原因
/// </summary>
[SugarTable("T_SFC_REPAIRENTRY")]
public class TSfcRepairentry : BaseEntity
{
    /// <summary>
    /// 不良原因内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREASONID")]
    public string Fbadreasonid { get; set; } = string.Empty;

    /// <summary>
    /// 表体单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 汇报状态
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTSTATUS")]
    public int Freportstatus { get; set; }

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

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
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 派工单内码
    /// </summary>
    [SugarColumn(ColumnName = "FDISPATCHID")]
    public string Fdispatchid { get; set; } = string.Empty;

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 第一次开工时间
    /// </summary>
    [SugarColumn(ColumnName = "FFSDTIME")]
    public DateTime? Ffsdtime { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSDTIME")]
    public DateTime? Fsdtime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FEDTIME")]
    public DateTime? Fedtime { get; set; }

    /// <summary>
    /// 累计用时(秒
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALSEC")]
    public decimal Ftotalsec { get; set; }
}
