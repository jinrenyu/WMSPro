using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 币别
/// </summary>
[SugarTable("T_BD_CURRENCY")]
public class TBdCurrency : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 币别代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 货币代码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string FCode { get; set; } = string.Empty;

    /// <summary>
    /// 币别名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 单价精度
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEDIGITS")]
    public int FPriceDigits { get; set; }

    /// <summary>
    /// 币别描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string FDescription { get; set; } = string.Empty;

    /// <summary>
    /// 金额精度
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNTDIGITS")]
    public int FAmountDigits { get; set; }

    /// <summary>
    /// 换算方式
    /// </summary>
    [SugarColumn(ColumnName = "FFIXRATE")]
    public int FFixRate { get; set; }

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public decimal FExchangeRate { get; set; }

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
}
