using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源表体-终端工位
/// </summary>
[SugarTable("T_ENG_RESOURCEENTRY6")]
public class TEngResourceentry6 : BaseEntity
{
    /// <summary>
    /// 终端工位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTATIONID")]
    public string Fstationid { get; set; } = string.Empty;

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
