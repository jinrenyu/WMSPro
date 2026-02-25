using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 审批流表体字段配置
/// </summary>
[SugarTable("T_OA_APPROVALCONFIGURATIONENTRY1")]
public class TOaApprovalconfigurationentry1 : BaseEntity
{
    /// <summary>
    /// 钉钉控件名
    /// </summary>
    [SugarColumn(ColumnName = "FDINGCONTROLNAME")]
    public string Fdingcontrolname { get; set; } = string.Empty;

    /// <summary>
    /// O3字段名
    /// </summary>
    [SugarColumn(ColumnName = "FO3FIELDBODY")]
    public string Fo3fieldbody { get; set; } = string.Empty;

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
