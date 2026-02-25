using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良品处理
/// </summary>
[SugarTable("T_SFC_REJECTSETTLE")]
public class TSfcRejectsettle : BaseEntity
{
    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    [SugarColumn(ColumnName = "FOASTATUS")]
    public string Foastatus { get; set; } = string.Empty;

    /// <summary>
    /// 审批结果
    /// </summary>
    [SugarColumn(ColumnName = "FOARESULT")]
    public string Foaresult { get; set; } = string.Empty;

    /// <summary>
    /// 来源不良类型
    /// </summary>
    [SugarColumn(ColumnName = "FBADSOURCETYPE")]
    public int Fbadsourcetype { get; set; }

    /// <summary>
    /// 来源不良内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADSOURCEID")]
    public string Fbadsourceid { get; set; } = string.Empty;

    /// <summary>
    /// 来源不良单编号
    /// </summary>
    [SugarColumn(ColumnName = "FBADBILLNO")]
    public string Fbadbillno { get; set; } = string.Empty;

    /// <summary>
    /// 任务单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
