using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源表体-人设备
/// </summary>
[SugarTable("T_ENG_RESOURCEENTRY")]
public class TEngResourceentry : BaseEntity
{
    /// <summary>
    /// 资源类型
    /// </summary>
    [SugarColumn(ColumnName = "FRESTYPE")]
    public int Frestype { get; set; }

    /// <summary>
    /// 人设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESID")]
    public string Fresid { get; set; } = string.Empty;

    /// <summary>
    /// 人设备代码
    /// </summary>
    [SugarColumn(ColumnName = "FRESNUMBER")]
    public string Fresnumber { get; set; } = string.Empty;

    /// <summary>
    /// 人设备名称
    /// </summary>
    [SugarColumn(ColumnName = "FRESNAME")]
    public string Fresname { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 工种类型
    /// </summary>
    [SugarColumn(ColumnName = "FGZ")]
    public int Fgz { get; set; }

    /// <summary>
    /// 班次
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTID")]
    public string Fshiftid { get; set; } = string.Empty;

    /// <summary>
    /// 班组
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID")]
    public string Fteamid { get; set; } = string.Empty;

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
