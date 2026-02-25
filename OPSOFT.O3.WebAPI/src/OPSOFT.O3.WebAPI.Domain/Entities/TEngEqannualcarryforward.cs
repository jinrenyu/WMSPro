using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 年度结转
/// </summary>
[SugarTable("T_ENG_EQANNUALCARRYFORWARD")]
public class TEngEqannualcarryforward : BaseEntity
{
    /// <summary>
    /// 下一记账年度
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTTALLYYEAR")]
    public int Fnexttallyyear { get; set; }

    /// <summary>
    /// 当年记账年度
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENTTALLYYEAR")]
    public int Fcurrenttallyyear { get; set; }

    /// <summary>
    /// 禁用人内码
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }
}
