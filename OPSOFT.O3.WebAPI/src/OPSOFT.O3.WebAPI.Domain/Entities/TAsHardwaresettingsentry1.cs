using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯硬件配置表体的表体-事件配置
/// </summary>
[SugarTable("T_AS_HARDWARESETTINGSENTRY1")]
public class TAsHardwaresettingsentry1 : BaseEntity
{
    /// <summary>
    /// 事件内码
    /// </summary>
    [SugarColumn(ColumnName = "FEVENTID")]
    public string Feventid { get; set; } = string.Empty;

    /// <summary>
    /// 多色灯配置内码
    /// </summary>
    [SugarColumn(ColumnName = "FLAMPCONFIGID")]
    public string Flampconfigid { get; set; } = string.Empty;

    /// <summary>
    /// 喇叭配置内码
    /// </summary>
    [SugarColumn(ColumnName = "FSPEAKERCONFIGID")]
    public string Fspeakerconfigid { get; set; } = string.Empty;

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
