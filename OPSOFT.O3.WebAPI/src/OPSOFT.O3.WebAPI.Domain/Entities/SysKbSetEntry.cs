using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 看板配置表-数据源
/// </summary>
[SugarTable("SYS_KBSETENTRY")]
public class SysKbSetEntry : BaseEntity
{
    /// <summary>
    /// SQL语句
    /// </summary>
    [SugarColumn(ColumnName = "FSQL")]
    public string Fsql { get; set; } = string.Empty;

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
