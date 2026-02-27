using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 请求日志服务
/// </summary>
public interface IRequestLogService
{
    /// <summary>
    /// 记录请求日志
    /// </summary>
    Task LogAsync(string method, string path, string queryString, int statusCode,
        long elapsedMs, string ip, string userId, string userAgent,
        string correlationId, string responseBody, string requestBody = "");

    /// <summary>
    /// 分页查询请求日志
    /// </summary>
    Task<PagedResult<RequestLogDto>> GetPagedListAsync(RequestLogQueryRequest request);

    /// <summary>
    /// 获取请求日志统计汇总
    /// </summary>
    Task<RequestLogSummaryDto> GetStatisticsAsync(RequestLogStatisticsRequest request);
}
