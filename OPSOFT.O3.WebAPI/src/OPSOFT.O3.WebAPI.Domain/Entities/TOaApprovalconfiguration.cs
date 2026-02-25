using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 审批流配置
/// </summary>
[SugarTable("T_OA_APPROVALCONFIGURATION")]
public class TOaApprovalconfiguration : BaseEntity
{
    /// <summary>
    /// 系统
    /// </summary>
    [SugarColumn(ColumnName = "FSYSTEM")]
    public int Fsystem { get; set; }

    /// <summary>
    /// 审批路径ID
    /// </summary>
    [SugarColumn(ColumnName = "FAPPROPATHID")]
    public string Fappropathid { get; set; } = string.Empty;

    /// <summary>
    /// 表单单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTEMPLATE")]
    public string Fbilltemplate { get; set; } = string.Empty;

    /// <summary>
    /// 表体标识
    /// </summary>
    [SugarColumn(ColumnName = "FBODYNUM")]
    public int Fbodynum { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
