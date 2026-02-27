using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 请求日志服务实现
/// </summary>
public class RequestLogService : IRequestLogService
{
    private readonly ISqlSugarClient _db;
    private readonly ILogger<RequestLogService> _logger;

    public RequestLogService(ISqlSugarClient db, ILogger<RequestLogService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task LogAsync(string method, string path, string queryString, int statusCode,
        long elapsedMs, string ip, string userId, string userAgent,
        string correlationId, string responseBody, string requestBody = "")
    {
        try
        {
            var log = new SysRequestLog
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = Guid.NewGuid().ToString("N"),
                Fmethod = method,
                Fpath = path,
                Fquerystring = queryString ?? string.Empty,
                Fstatuscode = statusCode,
                Felapsedms = elapsedMs,
                Fip = ip ?? string.Empty,
                Fuserid = userId ?? string.Empty,
                Fuseragent = userAgent ?? string.Empty,
                Fcorrelationid = correlationId ?? string.Empty,
                Frequesttime = DateTime.Now,
                Frequestbody = requestBody ?? string.Empty,
                Fresponsebody = responseBody ?? string.Empty,
                CYmd = DateTime.Now,
                CUser = userId ?? string.Empty,
                MYmd = DateTime.Now,
                MUser = userId ?? string.Empty
            };

            await _db.Insertable(log).ExecuteCommandAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "记录请求日志失败: {Method} {Path}", method, path);
        }
    }

    public async Task<PagedResult<RequestLogDto>> GetPagedListAsync(RequestLogQueryRequest request)
    {
        var query = _db.Queryable<SysRequestLog>()
            .Where(l => !l.FDeleted)
            .WhereIF(!string.IsNullOrEmpty(request.Method), l => l.Fmethod == request.Method)
            .WhereIF(!string.IsNullOrEmpty(request.Path), l => l.Fpath.Contains(request.Path!))
            .WhereIF(request.StatusCode.HasValue, l => l.Fstatuscode == request.StatusCode)
            .WhereIF(!string.IsNullOrEmpty(request.UserId), l => l.Fuserid == request.UserId)
            .WhereIF(request.StartDate.HasValue, l => l.Frequesttime >= request.StartDate)
            .WhereIF(request.EndDate.HasValue, l => l.Frequesttime <= request.EndDate)
            .WhereIF(request.MinElapsedMs.HasValue, l => l.Felapsedms >= request.MinElapsedMs)
            .WhereIF(!string.IsNullOrEmpty(request.Keyword), l =>
                l.Fpath.Contains(request.Keyword!) ||
                l.Fuserid.Contains(request.Keyword!) ||
                l.Fip.Contains(request.Keyword!));

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(l => l.Frequesttime, OrderByType.Desc)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<RequestLogDto>
        {
            Items = items.Select(l => new RequestLogDto
            {
                Uid = l.Uid,
                Fmethod = l.Fmethod,
                Fpath = l.Fpath,
                Fquerystring = l.Fquerystring,
                Fstatuscode = l.Fstatuscode,
                Felapsedms = l.Felapsedms,
                Fip = l.Fip,
                Fuserid = l.Fuserid,
                Fuseragent = l.Fuseragent,
                Fcorrelationid = l.Fcorrelationid,
                Frequesttime = l.Frequesttime,
                Frequestbody = l.Frequestbody,
                Fresponsebody = l.Fresponsebody
            }).ToList(),
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    public async Task<RequestLogSummaryDto> GetStatisticsAsync(RequestLogStatisticsRequest request)
    {
        var baseQuery = _db.Queryable<SysRequestLog>()
            .Where(l => !l.FDeleted)
            .WhereIF(request.StartDate.HasValue, l => l.Frequesttime >= request.StartDate)
            .WhereIF(request.EndDate.HasValue, l => l.Frequesttime <= request.EndDate);

        var today = DateTime.Today;
        var trendStartDate = DateTime.Today.AddDays(-(request.TrendDays - 1));

        var totalCount = await baseQuery.Clone().CountAsync();
        var todayCount = await baseQuery.Clone().Where(l => l.Frequesttime >= today).CountAsync();
        var errorCount = await baseQuery.Clone().Where(l => l.Fstatuscode >= 400).CountAsync();
        var avgResponseMs = totalCount > 0
            ? await baseQuery.Clone().AvgAsync(l => l.Felapsedms)
            : 0;

        var byMethod = await baseQuery.Clone()
            .GroupBy(l => l.Fmethod)
            .Select(l => new RequestCountByMethodDto
            {
                Method = l.Fmethod,
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .ToListAsync();

        var byStatusCode = await baseQuery.Clone()
            .GroupBy(l => l.Fstatuscode)
            .Select(l => new RequestCountByStatusCodeDto
            {
                StatusCode = l.Fstatuscode,
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .OrderBy(l => l.StatusCode)
            .ToListAsync();

        var topPaths = await baseQuery.Clone()
            .GroupBy(l => l.Fpath)
            .Select(l => new RequestCountByPathDto
            {
                Path = l.Fpath,
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .OrderBy(l => l.Count, OrderByType.Desc)
            .Take(20)
            .ToListAsync();

        var dailyTrend = await baseQuery.Clone()
            .Where(l => l.Frequesttime >= trendStartDate)
            .GroupBy(l => l.Frequesttime!.Value.Date)
            .Select(l => new RequestCountByDateDto
            {
                Date = l.Frequesttime!.Value.Date.ToString("yyyy-MM-dd"),
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .OrderBy(l => l.Date)
            .ToListAsync();

        return new RequestLogSummaryDto
        {
            TotalCount = totalCount,
            TodayCount = todayCount,
            AvgResponseMs = Math.Round((double)avgResponseMs, 2),
            ErrorCount = errorCount,
            ByMethod = byMethod,
            ByStatusCode = byStatusCode,
            TopPaths = topPaths,
            DailyTrend = dailyTrend
        };
    }
}
