using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP下发表体工位-表体作业文件信息
/// </summary>
[SugarTable("T_BD_ESOPISSUED_ENTRY_ENTRY")]
public class TBdEsopissuedEntryEntry : BaseEntity
{
    /// <summary>
    /// 文件内码
    /// </summary>
    [SugarColumn(ColumnName = "FFILEID")]
    public string Ffileid { get; set; } = string.Empty;

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
