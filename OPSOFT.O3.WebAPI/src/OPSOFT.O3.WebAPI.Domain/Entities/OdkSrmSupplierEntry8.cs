using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商档案审核意见
/// </summary>
[SugarTable("ODK_SRM_SupplierEntry8")]
public class OdkSrmSupplierEntry8 : BaseEntity
{
    /// <summary>
    /// 审核角色
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKROLE")]
    public string Fcheckrole { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核结果
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKRESULT")]
    public string Fcheckresult { get; set; } = string.Empty;

    /// <summary>
    /// 审核意见
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKOPINION")]
    public string Fcheckopinion { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

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
