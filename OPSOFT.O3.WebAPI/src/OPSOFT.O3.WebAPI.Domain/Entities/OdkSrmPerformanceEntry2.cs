using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 绩效考评表体3考核分明细
/// </summary>
[SugarTable("ODK_SRM_PerformanceEntry2")]
public class OdkSrmPerformanceEntry2 : BaseEntity
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
    /// 考评项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FPFITEMID")]
    public string Fpfitemid { get; set; } = string.Empty;

    /// <summary>
    /// 实际得分
    /// </summary>
    [SugarColumn(ColumnName = "FSCORE")]
    public decimal Fscore { get; set; }

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
