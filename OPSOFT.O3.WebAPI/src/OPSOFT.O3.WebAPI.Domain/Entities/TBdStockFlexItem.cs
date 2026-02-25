using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓库仓位值集
/// </summary>
[SugarTable("T_BD_STOCKFLEXITEM")]
public class TBdStockflexitem : BaseEntity
{
    /// <summary>
    /// 仓位值集内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLEXID")]
    public string Fflexid { get; set; } = string.Empty;

    /// <summary>
    /// 是否必录
    /// </summary>
    [SugarColumn(ColumnName = "FISMUSTINPUT")]
    public bool Fismustinput { get; set; }

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
