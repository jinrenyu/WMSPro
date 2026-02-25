using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 部门
/// </summary>
[SugarTable("T_BD_DEPARTMENT")]
public class TBdDepartment : BaseEntity
{
    /// <summary>
    /// 部门代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 部门描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string FDescription { get; set; } = string.Empty;

    /// <summary>
    /// 创建组织
    /// </summary>
    [SugarColumn(ColumnName = "FCREATEORGID")]
    public string FCreateOrgId { get; set; } = string.Empty;

    /// <summary>
    /// 助记码
    /// </summary>
    [SugarColumn(ColumnName = "FHELPCODE")]
    public string FHelpCode { get; set; } = string.Empty;

    /// <summary>
    /// 部门全称
    /// </summary>
    [SugarColumn(ColumnName = "FFULLNAME")]
    public string FFullName { get; set; } = string.Empty;

    /// <summary>
    /// 成本核算类型
    /// </summary>
    [SugarColumn(ColumnName = "FCOSTACCOUNTTYPE")]
    public bool FCostAccountType { get; set; }

    /// <summary>
    /// 是否产线
    /// </summary>
    [SugarColumn(ColumnName = "FISLINE")]
    public bool FIsLine { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(ColumnName = "FMANAGER")]
    public string FManager { get; set; } = string.Empty;

    /// <summary>
    /// 上级部门
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTID")]
    public string FParentId { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FEFFECTDATE")]
    public DateTime FEffectDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FLAPSEDATE")]
    public DateTime FLapseDate { get; set; } = DateTime.MaxValue;

    /// <summary>
    /// 部门属性
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTPROPERTY")]
    public string FDeptProperty { get; set; } = string.Empty;

    /// <summary>
    /// 使用组织
    /// </summary>
    [SugarColumn(ColumnName = "FUSEORGID")]
    public string FUseOrgId { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string FDisableId { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime FDisableDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// ERP编码
    /// </summary>
    [SugarColumn(ColumnName = "FERPNUMBER")]
    public string FErpNumber { get; set; } = string.Empty;
}
