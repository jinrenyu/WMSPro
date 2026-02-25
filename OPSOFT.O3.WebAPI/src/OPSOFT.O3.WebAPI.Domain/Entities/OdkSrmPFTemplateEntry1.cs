using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 考评模板表体—等级定义
/// </summary>
[SugarTable("ODK_SRM_PFTemplateEntry1")]
public class OdkSrmPFTemplateEntry1 : BaseEntity
{
    /// <summary>
    /// 分数从
    /// </summary>
    [SugarColumn(ColumnName = "FBSCORE")]
    public decimal Fbscore { get; set; }

    /// <summary>
    /// 分数至
    /// </summary>
    [SugarColumn(ColumnName = "FESCORE")]
    public decimal Fescore { get; set; }

    /// <summary>
    /// 评级
    /// </summary>
    [SugarColumn(ColumnName = "FSCOREGRADE")]
    public string Fscoregrade { get; set; } = string.Empty;

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
