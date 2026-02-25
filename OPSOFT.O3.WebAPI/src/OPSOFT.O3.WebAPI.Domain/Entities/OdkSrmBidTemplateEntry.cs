using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 评标模板表体
/// </summary>
[SugarTable("ODK_SRM_BidTemplateEntry")]
public class OdkSrmBidTemplateEntry : BaseEntity
{
    /// <summary>
    /// 评标项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FBIDITEMID")]
    public string Fbiditemid { get; set; } = string.Empty;

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
    /// 权重（%）
    /// </summary>
    [SugarColumn(ColumnName = "FRATIO")]
    public decimal Fratio { get; set; }

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
