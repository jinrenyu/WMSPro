using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统日志表
/// </summary>
[SugarTable("SYS_OPERATIONLOG")]
public class SysOperationLog : BaseEntity
{
    /// <summary>
    /// 操作日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 操作用户
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// 操作用户名称
    /// </summary>
    [SugarColumn(ColumnName = "FUSERNAME")]
    public string Fusername { get; set; } = string.Empty;

    /// <summary>
    /// 系统功能
    /// </summary>
    [SugarColumn(ColumnName = "FPRGKEY")]
    public string Fprgkey { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型 (Create/Update/Delete/Approve等)
    /// </summary>
    [SugarColumn(ColumnName = "FOPERATIONTYPE")]
    public string Foperationtype { get; set; } = string.Empty;

    /// <summary>
    /// 目标记录UID
    /// </summary>
    [SugarColumn(ColumnName = "FTARGETUID")]
    public string Ftargetuid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;

    /// <summary>
    /// 客户端IP
    /// </summary>
    [SugarColumn(ColumnName = "FIP")]
    public string Fip { get; set; } = string.Empty;

    /// <summary>
    /// 是否成功
    /// </summary>
    [SugarColumn(ColumnName = "FSUCCESS")]
    public bool Fsuccess { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    [SugarColumn(ColumnName = "FERRORMSG")]
    public string Ferrormsg { get; set; } = string.Empty;

    /// <summary>
    /// 关联ID
    /// </summary>
    [SugarColumn(ColumnName = "FCORRELATIONID")]
    public string Fcorrelationid { get; set; } = string.Empty;

    /// <summary>
    /// 日志状态信息(扩展用)
    /// </summary>
    [SugarColumn(ColumnName = "FSTATEMENT")]
    public string Fstatement { get; set; } = string.Empty;

    /// <summary>
    /// 日志描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION", IsNullable = true)]
    public string FDESCRIPTION { get; set; } = string.Empty;

    /// <summary>
    /// 操作实体表名
    /// </summary>
    [SugarColumn(ColumnName = "FTABLENAME", IsNullable = true)]
    public string FTABLENAME { get; set; } = string.Empty;
}
