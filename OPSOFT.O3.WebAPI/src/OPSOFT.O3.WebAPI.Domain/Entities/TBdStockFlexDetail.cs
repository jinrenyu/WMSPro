using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓库仓位值集明细
/// </summary>
[SugarTable("T_BD_STOCKFLEXDETAIL")]
public class TBdStockflexdetail : BaseEntity
{
    /// <summary>
    /// 仓位值集内码（关联 T_BAS_FLEXVALUESENTRY.FDETAILID，仓位集值代码/名称/描述由此 JOIN 得到）
    /// </summary>
    [SugarColumn(ColumnName = "FFLEXENTRYID")]
    public string Fflexentryid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
