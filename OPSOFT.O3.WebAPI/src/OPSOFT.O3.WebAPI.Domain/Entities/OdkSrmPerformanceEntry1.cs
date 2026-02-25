using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 绩效考评表体2物料明细
/// </summary>
[SugarTable("ODK_SRM_PerformanceEntry1")]
public class OdkSrmPerformanceEntry1 : BaseEntity
{
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
