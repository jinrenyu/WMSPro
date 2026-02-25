using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 派工表体-班组
/// </summary>
[SugarTable("T_SFC_DISPATCHENTRY1")]
public class TSfcDispatchentry1 : BaseEntity
{
    /// <summary>
    /// 班组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID")]
    public string Fteamid { get; set; } = string.Empty;

    /// <summary>
    /// 关闭状态
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSED")]
    public bool Fclosed { get; set; }

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
