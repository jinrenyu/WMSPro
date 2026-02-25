using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商档案样品申请单
/// </summary>
[SugarTable("ODK_SRM_SupplierEntry7")]
public class OdkSrmSupplierEntry7 : BaseEntity
{
    /// <summary>
    /// 样品单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEDETAILID")]
    public string Fsampledetailid { get; set; } = string.Empty;

    /// <summary>
    /// 申请单号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 物料代码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 通过状态
    /// </summary>
    [SugarColumn(ColumnName = "FPASSSTATUS")]
    public string Fpassstatus { get; set; } = string.Empty;

    /// <summary>
    /// 采购员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

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
