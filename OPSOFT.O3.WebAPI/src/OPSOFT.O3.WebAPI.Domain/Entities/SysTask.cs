using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 智能调度平台
/// </summary>
[SugarTable("SYS_TASK")]
public class SysTask : BaseEntity
{
    /// <summary>
    /// 调度代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 调度名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 作业名称
    /// </summary>
    [SugarColumn(ColumnName = "FJOBNAME")]
    public string Fjobname { get; set; } = string.Empty;

    /// <summary>
    /// 作业分组
    /// </summary>
    [SugarColumn(ColumnName = "FJOBGROUP")]
    public string Fjobgroup { get; set; } = string.Empty;

    /// <summary>
    /// 作业描述
    /// </summary>
    [SugarColumn(ColumnName = "FJOBDES")]
    public string Fjobdes { get; set; } = string.Empty;

    /// <summary>
    /// 作业参数
    /// </summary>
    [SugarColumn(ColumnName = "FJOBPARAM")]
    public string Fjobparam { get; set; } = string.Empty;

    /// <summary>
    /// 作业程序集
    /// </summary>
    [SugarColumn(ColumnName = "FJOBTYPE")]
    public string Fjobtype { get; set; } = string.Empty;

    /// <summary>
    /// 触发器名称
    /// </summary>
    [SugarColumn(ColumnName = "FTRIGGERNAME")]
    public string Ftriggername { get; set; } = string.Empty;

    /// <summary>
    /// 触发器分组
    /// </summary>
    [SugarColumn(ColumnName = "FTRIGGERGROUP")]
    public string Ftriggergroup { get; set; } = string.Empty;

    /// <summary>
    /// 触发器描述
    /// </summary>
    [SugarColumn(ColumnName = "FTRIGGERDES")]
    public string Ftriggerdes { get; set; } = string.Empty;

    /// <summary>
    /// Cron表达式
    /// </summary>
    [SugarColumn(ColumnName = "FTRIGGERCRON")]
    public string Ftriggercron { get; set; } = string.Empty;

    /// <summary>
    /// Cron描述
    /// </summary>
    [SugarColumn(ColumnName = "FCRONREMARK")]
    public string Fcronremark { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSDTIME")]
    public DateTime? Fsdtime { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
