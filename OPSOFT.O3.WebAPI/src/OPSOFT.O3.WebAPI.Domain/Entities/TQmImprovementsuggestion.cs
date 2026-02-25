using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 改善意见单
/// </summary>
[SugarTable("T_QM_IMPROVEMENTSUGGESTION")]
public class TQmImprovementsuggestion : BaseEntity
{
    /// <summary>
    /// MRB评审单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMRBID")]
    public string Fmrbid { get; set; } = string.Empty;

    /// <summary>
    /// 检验单内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKID")]
    public string Fcheckid { get; set; } = string.Empty;

    /// <summary>
    /// 完成截止时间
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPLETECUTOFF")]
    public DateTime? Fcompletecutoff { get; set; }

    /// <summary>
    /// MRB评审结果
    /// </summary>
    [SugarColumn(ColumnName = "FMRBRRESULT")]
    public int Fmrbrresult { get; set; }

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 改善意见
    /// </summary>
    [SugarColumn(ColumnName = "FSUGGESTION")]
    public string Fsuggestion { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSSTATUS")]
    public int Fprocessstatus { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
