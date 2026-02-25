using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 最后结账年度
/// </summary>
[SugarTable("T_ENG_EQLASTYEAR")]
public class TEngEqlastyear : BaseEntity
{
    /// <summary>
    /// 最后结账年度
    /// </summary>
    [SugarColumn(ColumnName = "FLASTYEAR")]
    public string Flastyear { get; set; } = string.Empty;

    /// <summary>
    /// 开账年度
    /// </summary>
    [SugarColumn(ColumnName = "FOPENYEAR")]
    public int Fopenyear { get; set; }
}
