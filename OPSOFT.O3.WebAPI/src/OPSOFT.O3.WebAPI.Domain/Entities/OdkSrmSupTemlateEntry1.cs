using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料组明细
/// </summary>
[SugarTable("ODK_SRM_SupTemlateEntry1")]
public class OdkSrmSupTemlateEntry1 : BaseEntity
{
    /// <summary>
    /// 关联内码
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONID")]
    public string Frelationid { get; set; } = string.Empty;

    /// <summary>
    /// 物料分组
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID")]
    public string Fmatgroupid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
