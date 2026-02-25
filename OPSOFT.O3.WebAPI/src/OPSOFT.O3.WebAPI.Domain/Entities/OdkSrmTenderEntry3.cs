using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 招标单-专家信息
/// </summary>
[SugarTable("ODK_SRM_TenderEntry3")]
public class OdkSrmTenderEntry3 : BaseEntity
{
    /// <summary>
    /// 专家
    /// </summary>
    [SugarColumn(ColumnName = "FPROFICIENTID")]
    public string Fproficientid { get; set; } = string.Empty;

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
