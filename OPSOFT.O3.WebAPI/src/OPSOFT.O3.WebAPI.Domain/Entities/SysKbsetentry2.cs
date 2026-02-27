using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 看板配置表-图表信息
/// </summary>
[SugarTable("SYS_KBSETENTRY2")]
public class SysKbsetentry2 : BaseEntity
{
    /// <summary>
    /// 看板数据源
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCE")]
    public string Fsource { get; set; } = string.Empty;

    /// <summary>
    /// 看板类别
    /// </summary>
    [SugarColumn(ColumnName = "FKBTYPE")]
    public string Fkbtype { get; set; } = string.Empty;

    /// <summary>
    /// 同行统一前景色
    /// </summary>
    [SugarColumn(ColumnName = "FFULLROWCOLOR")]
    public bool Ffullrowcolor { get; set; }

    /// <summary>
    /// 是否表头
    /// </summary>
    [SugarColumn(ColumnName = "FHASHEADER")]
    public bool Fhasheader { get; set; }

    /// <summary>
    /// 是否有背景颜色
    /// </summary>
    [SugarColumn(ColumnName = "FHASBKGROUP")]
    public bool Fhasbkgroup { get; set; }

    /// <summary>
    /// 自定义时的脚本
    /// </summary>
    [SugarColumn(ColumnName = "FXAML")]
    public string Fxaml { get; set; } = string.Empty;

    /// <summary>
    /// 看板滚动时间(秒
    /// </summary>
    [SugarColumn(ColumnName = "FTIME")]
    public int Ftime { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID", IsNullable = true)]
    public string FBODYID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }
}
