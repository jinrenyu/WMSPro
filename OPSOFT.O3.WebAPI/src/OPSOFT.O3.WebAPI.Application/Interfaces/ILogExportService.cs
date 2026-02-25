using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 日志导出服务
/// </summary>
public interface ILogExportService
{
    /// <summary>
    /// 导出操作日志为CSV
    /// </summary>
    Task<byte[]> ExportOperationLogsCsvAsync(OperationLogQueryRequest request);

    /// <summary>
    /// 导出审计日志为CSV
    /// </summary>
    Task<byte[]> ExportAuditLogsCsvAsync(AuditLogQueryRequest request);

    /// <summary>
    /// 导出请求日志为CSV
    /// </summary>
    Task<byte[]> ExportRequestLogsCsvAsync(RequestLogQueryRequest request);
}
