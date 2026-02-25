using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商评估表体
/// </summary>
[SugarTable("ODK_SRM_AssessEntry")]
public class OdkSrmAssessEntry : BaseEntity
{
    /// <summary>
    /// 评估项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSITEMID")]
    public string Fassessitemid { get; set; } = string.Empty;

    /// <summary>
    /// 实际得分
    /// </summary>
    [SugarColumn(ColumnName = "FACTSCORE")]
    public decimal Factscore { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

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
