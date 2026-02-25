using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 审计追踪日志
/// </summary>
[SugarTable("SYS_AUDITLOG")]
public class SysAuditLog : BaseEntity
{
    /// <summary>
    /// 客户端IP地址
    /// </summary>
    [SugarColumn(ColumnName = "FIP")]
    public string Fip { get; set; } = string.Empty;

    /// <summary>
    /// 单据UID
    /// </summary>
    [SugarColumn(ColumnName = "FUID")]
    public string Fuid { get; set; } = string.Empty;

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRGKEY")]
    public string Fprgkey { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
