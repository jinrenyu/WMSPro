using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯呼叫配置表体
/// </summary>
[SugarTable("T_AS_REPORTSETTINGSENTRY")]
public class TAsReportsettingsentry : BaseEntity
{
    /// <summary>
    /// 字段内码
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDID")]
    public string Ffieldid { get; set; } = string.Empty;

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
