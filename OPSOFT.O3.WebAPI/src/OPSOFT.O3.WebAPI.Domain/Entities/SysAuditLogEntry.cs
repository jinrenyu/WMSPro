using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 审计追踪日志表体
/// </summary>
[SugarTable("SYS_AUDITLOGENTRY")]
public class SysAuditLogEntry : BaseEntity
{
    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 表名称
    /// </summary>
    [SugarColumn(ColumnName = "FTABLENAME")]
    public string Ftablename { get; set; } = string.Empty;

    /// <summary>
    /// 字段名称
    /// </summary>
    [SugarColumn(ColumnName = "FCOLUMNNAME")]
    public string Fcolumnname { get; set; } = string.Empty;

    /// <summary>
    /// 修改前数据
    /// </summary>
    [SugarColumn(ColumnName = "FBEFOREDATA")]
    public string Fbeforedata { get; set; } = string.Empty;

    /// <summary>
    /// 修改后数据
    /// </summary>
    [SugarColumn(ColumnName = "FAFTERDATA")]
    public string Fafterdata { get; set; } = string.Empty;

    /// <summary>
    /// 数据
    /// </summary>
    [SugarColumn(ColumnName = "FDATA")]
    public string Fdata { get; set; } = string.Empty;

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
