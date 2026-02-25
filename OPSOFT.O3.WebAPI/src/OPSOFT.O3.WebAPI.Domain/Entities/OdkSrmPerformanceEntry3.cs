using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 绩效考评表体4考核汇总
/// </summary>
[SugarTable("ODK_SRM_PerformanceEntry3")]
public class OdkSrmPerformanceEntry3 : BaseEntity
{
    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 物料分组
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID")]
    public string Fmatgroupid { get; set; } = string.Empty;

    /// <summary>
    /// 评级(A;B;C;D;E;F
    /// </summary>
    [SugarColumn(ColumnName = "FSCOREGRADE")]
    public string Fscoregrade { get; set; } = string.Empty;
}
