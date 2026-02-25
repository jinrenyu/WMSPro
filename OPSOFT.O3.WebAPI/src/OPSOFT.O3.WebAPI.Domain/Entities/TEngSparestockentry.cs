using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 备件盘点表体
/// </summary>
[SugarTable("T_ENG_SPARESTOCKENTRY")]
public class TEngSparestockentry : BaseEntity
{
    /// <summary>
    /// 备件清单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSBOM")]
    public string Fsbom { get; set; } = string.Empty;

    /// <summary>
    /// 实际数量
    /// </summary>
    [SugarColumn(ColumnName = "FREALQUANTITY")]
    public int Frealquantity { get; set; }

    /// <summary>
    /// 盘盈数量
    /// </summary>
    [SugarColumn(ColumnName = "FSURPLUS")]
    public int Fsurplus { get; set; }

    /// <summary>
    /// 盘亏数量
    /// </summary>
    [SugarColumn(ColumnName = "FLOSE")]
    public int Flose { get; set; }

    /// <summary>
    /// 帐存数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public int Fcount { get; set; }

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
