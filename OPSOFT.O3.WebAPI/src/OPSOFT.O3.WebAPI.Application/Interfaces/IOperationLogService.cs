using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 操作日志服务
/// </summary>
public interface IOperationLogService
{
    /// <summary>
    /// 记录操作日志
    /// </summary>
    Task LogAsync(string prgKey, string operationType, string? targetUid = null,
        string? billNo = null, string? description = null,
        bool success = true, string? errorMsg = null);

    /// <summary>
    /// 分页查询操作日志
    /// </summary>
    Task<PagedResult<OperationLogDto>> GetPagedListAsync(OperationLogQueryRequest request);

    /// <summary>
    /// 获取操作日志统计汇总
    /// </summary>
    Task<OperationLogSummaryDto> GetStatisticsAsync(OperationLogStatisticsRequest request);
}
