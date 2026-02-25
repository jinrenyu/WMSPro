using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 计量单位
/// </summary>
[SugarTable("T_BD_UNIT")]
public class TBdUnit : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 单位代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 单位名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 单位描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string FDescription { get; set; } = string.Empty;

    /// <summary>
    /// 单位组内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITGROUPID")]
    public string FUnitGroupId { get; set; } = string.Empty;

    /// <summary>
    /// 基准计量单位
    /// </summary>
    [SugarColumn(ColumnName = "FISBASEUNIT")]
    public bool FIsBaseUnit { get; set; }

    /// <summary>
    /// 精度
    /// </summary>
    [SugarColumn(ColumnName = "FPRECISION")]
    public int FPrecision { get; set; }

    /// <summary>
    /// 舍入类型
    /// </summary>
    [SugarColumn(ColumnName = "FROUNDTYPE")]
    public string FRoundType { get; set; } = string.Empty;

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
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 转换类型
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTTYPE")]
    public string FConvertType { get; set; } = string.Empty;

    /// <summary>
    /// 与主副单位换算率
    /// </summary>
    [SugarColumn(ColumnName = "FCOEFFICIENT")]
    public decimal FCoefficient { get; set; }
}
