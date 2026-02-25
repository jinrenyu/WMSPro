using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 异常扣款记录明细
/// </summary>
[SugarTable("T_BD_DEDUCTPAYRECORDENTRY")]
public class TBdDeductpayrecordentry : BaseEntity
{
    /// <summary>
    /// 员工内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 扣款金额
    /// </summary>
    [SugarColumn(ColumnName = "FDEDUCTAMOUNT")]
    public decimal Fdeductamount { get; set; }

    /// <summary>
    /// 扣款原因
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
