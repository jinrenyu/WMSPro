using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 考核项目
/// </summary>
[SugarTable("ODK_SRM_PerformanceItem")]
public class OdkSrmPerformanceItem : BaseEntity
{
    /// <summary>
    /// 考核项目代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 考核项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 评分方式
    /// </summary>
    [SugarColumn(ColumnName = "FSCOREMETHOD")]
    public string Fscoremethod { get; set; } = string.Empty;

    /// <summary>
    /// 目标分
    /// </summary>
    [SugarColumn(ColumnName = "FSTANDARDSCORE")]
    public decimal Fstandardscore { get; set; }

    /// <summary>
    /// 评分细则
    /// </summary>
    [SugarColumn(ColumnName = "FSCORENOTE")]
    public string Fscorenote { get; set; } = string.Empty;

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public int Fchecklevel { get; set; }

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
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
