using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单位换算率
/// </summary>
[SugarTable("T_BD_UNITCONVERTRATE")]
public class TBdUnitconvertrate : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 当前单位
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENTUNITID")]
    public string Fcurrentunitid { get; set; } = string.Empty;

    /// <summary>
    /// 目标单位
    /// </summary>
    [SugarColumn(ColumnName = "FDESTUNITID")]
    public string Fdestunitid { get; set; } = string.Empty;

    /// <summary>
    /// 使用组织
    /// </summary>
    [SugarColumn(ColumnName = "FUSEORGID")]
    public string Fuseorgid { get; set; } = string.Empty;

    /// <summary>
    /// 换算类型
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTTYPE")]
    public string Fconverttype { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 转换分子
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTNUMERATOR")]
    public decimal Fconvertnumerator { get; set; }

    /// <summary>
    /// 转换分母
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTDENOMINATOR")]
    public decimal Fconvertdenominator { get; set; }

    /// <summary>
    /// 单位关联内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
