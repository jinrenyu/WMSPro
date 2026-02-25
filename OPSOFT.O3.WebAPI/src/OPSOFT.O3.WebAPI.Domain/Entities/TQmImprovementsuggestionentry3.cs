using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 改善意见单-被通知人
/// </summary>
[SugarTable("T_QM_IMPROVEMENTSUGGESTIONENTRY3")]
public class TQmImprovementsuggestionentry3 : BaseEntity
{
    /// <summary>
    /// 被通知人
    /// </summary>
    [SugarColumn(ColumnName = "FNOTICERID")]
    public string Fnoticerid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
