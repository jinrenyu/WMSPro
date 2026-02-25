using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源表体-物料类型
/// </summary>
[SugarTable("T_ENG_RESOURCEENTRY3")]
public class TEngResourceentry3 : BaseEntity
{
    /// <summary>
    /// 物料类别内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALTYPEID")]
    public string Fmaterialtypeid { get; set; } = string.Empty;

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
