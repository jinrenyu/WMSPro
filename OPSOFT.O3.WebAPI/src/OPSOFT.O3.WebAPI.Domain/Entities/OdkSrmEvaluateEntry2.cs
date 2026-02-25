using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 专家评分表体3-打分明细
/// </summary>
[SugarTable("ODK_SRM_EvaluateEntry2")]
public class OdkSrmEvaluateEntry2 : BaseEntity
{
    /// <summary>
    /// 物料表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATDETAILID")]
    public string Fmatdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 专家评分表体2-投标方明细行内码
    /// </summary>
    [SugarColumn(ColumnName = "FBIDDETAILID")]
    public string Fbiddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 评估项目
    /// </summary>
    [SugarColumn(ColumnName = "FBIDITEMID")]
    public string Fbiditemid { get; set; } = string.Empty;

    /// <summary>
    /// 权重（%）
    /// </summary>
    [SugarColumn(ColumnName = "FRATIO")]
    public decimal Fratio { get; set; }

    /// <summary>
    /// 实际得分
    /// </summary>
    [SugarColumn(ColumnName = "FSCORE")]
    public decimal Fscore { get; set; }

    /// <summary>
    /// 说明
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
