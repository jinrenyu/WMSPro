using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 改善意见单-改善部门
/// </summary>
[SugarTable("T_QM_IMPROVEMENTSUGGESTIONENTRY2")]
public class TQmImprovementsuggestionentry2 : BaseEntity
{
    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 职员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 改善意见
    /// </summary>
    [SugarColumn(ColumnName = "FOPINION")]
    public string Fopinion { get; set; } = string.Empty;

    /// <summary>
    /// 改善时间
    /// </summary>
    [SugarColumn(ColumnName = "FIMPROVETINE")]
    public DateTime? Fimprovetine { get; set; }

    /// <summary>
    /// 关闭
    /// </summary>
    [SugarColumn(ColumnName = "FISCLOSE")]
    public bool Fisclose { get; set; }

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
