using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商-物料组列表
/// </summary>
[SugarTable("ODK_SRM_SupplierEntry5")]
public class OdkSrmSupplierEntry5 : BaseEntity
{
    /// <summary>
    /// 物料组内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID")]
    public string Fmatgroupid { get; set; } = string.Empty;

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
