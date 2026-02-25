using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 专家评分表头
/// </summary>
[SugarTable("ODK_SRM_Evaluate")]
public class OdkSrmEvaluate : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 专家内码
    /// </summary>
    [SugarColumn(ColumnName = "FEXPECTID")]
    public string Fexpectid { get; set; } = string.Empty;

    /// <summary>
    /// 招标单编号
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERBILLNO")]
    public string Ftenderbillno { get; set; } = string.Empty;

    /// <summary>
    /// 招标单内码
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERINTERID")]
    public string Ftenderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
