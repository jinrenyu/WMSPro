using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源表体-模治具
/// </summary>
[SugarTable("T_ENG_RESOURCEENTRY2")]
public class TEngResourceentry2 : BaseEntity
{
    /// <summary>
    /// 模治具类别内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOULDTYPEID")]
    public string Fmouldtypeid { get; set; } = string.Empty;

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
