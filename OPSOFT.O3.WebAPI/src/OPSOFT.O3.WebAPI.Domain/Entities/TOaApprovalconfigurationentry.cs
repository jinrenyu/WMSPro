using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 审批流表头字段配置
/// </summary>
[SugarTable("T_OA_APPROVALCONFIGURATIONENTRY")]
public class TOaApprovalconfigurationentry : BaseEntity
{
    /// <summary>
    /// 钉钉控件名
    /// </summary>
    [SugarColumn(ColumnName = "FDINGCONTROLNAME")]
    public string Fdingcontrolname { get; set; } = string.Empty;

    /// <summary>
    /// O3字段名
    /// </summary>
    [SugarColumn(ColumnName = "FO3FIELD")]
    public string Fo3field { get; set; } = string.Empty;

    /// <summary>
    /// O3字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FO3FIELDTYPE")]
    public int Fo3fieldtype { get; set; }

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
