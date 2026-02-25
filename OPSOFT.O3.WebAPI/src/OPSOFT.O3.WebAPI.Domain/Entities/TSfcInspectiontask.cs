using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 巡检任务
/// </summary>
[SugarTable("T_SFC_INSPECTIONTASK")]
public class TSfcInspectiontask : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 检验计划
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKPLANID")]
    public string Fcheckplanid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 执行人
    /// </summary>
    [SugarColumn(ColumnName = "FEXECTOR")]
    public string Fexector { get; set; } = string.Empty;

    /// <summary>
    /// 计划执行时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANDATE")]
    public DateTime? Fplandate { get; set; }

    /// <summary>
    /// 实际执行时间
    /// </summary>
    [SugarColumn(ColumnName = "FREALDATE")]
    public DateTime? Frealdate { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FEFFECTIVEDATE")]
    public DateTime? Feffectivedate { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE")]
    public DateTime? Fenddate { get; set; }

    /// <summary>
    /// 巡检状态，0=待检验；1=执行中；2=已完成
    /// </summary>
    [SugarColumn(ColumnName = "FDATESTATUS")]
    public int Fdatestatus { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID")]
    public string Freportid { get; set; } = string.Empty;
}
