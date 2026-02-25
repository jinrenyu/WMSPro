using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 开箱验收表体-费用明细
/// </summary>
[SugarTable("T_ENG_EQOUTBOXACCEPTENTRY")]
public class TEngEqoutboxacceptentry : BaseEntity
{
    /// <summary>
    /// 费用名称
    /// </summary>
    [SugarColumn(ColumnName = "FCHNAME")]
    public string Fchname { get; set; } = string.Empty;

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FCHAMT")]
    public decimal Fchamt { get; set; }

    /// <summary>
    /// 合同号
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACT")]
    public string Fcontract { get; set; } = string.Empty;

    /// <summary>
    /// 票据号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNUMBER")]
    public string Fbillnumber { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
