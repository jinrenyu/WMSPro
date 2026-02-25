using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源生产状态变更履历
/// </summary>
[SugarTable("T_SFC_RESOURCEWORKSTATEHIS")]
public class TSfcResourceworkstatehis : BaseEntity
{
    /// <summary>
    /// 任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡号
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 数据状态
    /// </summary>
    [SugarColumn(ColumnName = "FDATASTATUS")]
    public string Fdatastatus { get; set; } = string.Empty;

    /// <summary>
    /// 呼叫类别
    /// </summary>
    [SugarColumn(ColumnName = "FCALLSTATUS")]
    public string Fcallstatus { get; set; } = string.Empty;

    /// <summary>
    /// 当前状态开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTATUSTIME")]
    public DateTime? Fstatustime { get; set; }

    /// <summary>
    /// 汇报数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTQTY")]
    public decimal Freportqty { get; set; }

    /// <summary>
    /// 计划生产数量
    /// </summary>
    [SugarColumn(ColumnName = "FPLANMAKEQTY")]
    public decimal Fplanmakeqty { get; set; }

    /// <summary>
    /// 当前生产率(数量/小时
    /// </summary>
    [SugarColumn(ColumnName = "ACTUALRATIO")]
    public decimal Actualratio { get; set; }
}
