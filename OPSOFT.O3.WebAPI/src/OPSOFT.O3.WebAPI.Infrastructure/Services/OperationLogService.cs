using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 操作日志服务实现
/// </summary>
public class OperationLogService : IOperationLogService
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICorrelationIdProvider _correlationIdProvider;
    private readonly ILogger<OperationLogService> _logger;

    public OperationLogService(
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IHttpContextAccessor httpContextAccessor,
        ICorrelationIdProvider correlationIdProvider,
        ILogger<OperationLogService> logger)
    {
        _db = db;
        _currentUser = currentUser;
        _httpContextAccessor = httpContextAccessor;
        _correlationIdProvider = correlationIdProvider;
        _logger = logger;
    }

    public async Task LogAsync(string prgKey, string operationType,
        string? targetUid = null, string? billNo = null,
        string? description = null, bool success = true, string? errorMsg = null)
    {
        try
        {
            var log = new SysOperationLog
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = Guid.NewGuid().ToString("N"),
                Fdate = DateTime.Now,
                Fuserid = _currentUser.UserId ?? string.Empty,
                Fusername = _currentUser.UserName ?? string.Empty,
                Fprgkey = prgKey,
                Foperationtype = operationType,
                Ftargetuid = targetUid ?? string.Empty,
                Fbillno = billNo ?? string.Empty,
                Fip = GetClientIp(),
                Fsuccess = success,
                Ferrormsg = errorMsg ?? string.Empty,
                Fcorrelationid = _correlationIdProvider.CorrelationId ?? string.Empty,
                Fstatement = description ?? string.Empty,
                FCompanyId = _currentUser.CompanyId ?? string.Empty,
                CYmd = DateTime.Now,
                CUser = _currentUser.UserId ?? string.Empty,
                MYmd = DateTime.Now,
                MUser = _currentUser.UserId ?? string.Empty
            };

            await _db.Insertable(log).ExecuteCommandAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "记录操作日志失败: PrgKey={PrgKey}, OperationType={OperationType}", prgKey, operationType);
        }
    }

    public async Task<PagedResult<OperationLogDto>> GetPagedListAsync(OperationLogQueryRequest request)
    {
        var query = _db.Queryable<SysOperationLog>()
            .Where(l => !l.FDeleted)
            .WhereIF(!string.IsNullOrEmpty(request.UserId), l => l.Fuserid == request.UserId)
            .WhereIF(!string.IsNullOrEmpty(request.PrgKey), l => l.Fprgkey == request.PrgKey)
            .WhereIF(!string.IsNullOrEmpty(request.OperationType), l => l.Foperationtype == request.OperationType)
            .WhereIF(!string.IsNullOrEmpty(request.TargetUid), l => l.Ftargetuid == request.TargetUid)
            .WhereIF(request.StartDate.HasValue, l => l.Fdate >= request.StartDate)
            .WhereIF(request.EndDate.HasValue, l => l.Fdate <= request.EndDate)
            .WhereIF(!string.IsNullOrEmpty(request.Keyword), l =>
                l.Fuserid.Contains(request.Keyword!) ||
                l.Fusername.Contains(request.Keyword!) ||
                l.Fprgkey.Contains(request.Keyword!));

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(l => l.Fdate, OrderByType.Desc)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<OperationLogDto>
        {
            Items = items.Select(l => new OperationLogDto
            {
                Uid = l.Uid,
                Fdate = l.Fdate,
                Fuserid = l.Fuserid,
                Fusername = l.Fusername,
                Fprgkey = l.Fprgkey,
                Foperationtype = l.Foperationtype,
                Ftargetuid = l.Ftargetuid,
                Fbillno = l.Fbillno,
                Fip = l.Fip,
                Fsuccess = l.Fsuccess,
                Ferrormsg = l.Ferrormsg,
                Fstatement = l.Fstatement
            }).ToList(),
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    public async Task<OperationLogSummaryDto> GetStatisticsAsync(OperationLogStatisticsRequest request)
    {
        var baseQuery = _db.Queryable<SysOperationLog>()
            .Where(l => !l.FDeleted)
            .WhereIF(request.StartDate.HasValue, l => l.Fdate >= request.StartDate)
            .WhereIF(request.EndDate.HasValue, l => l.Fdate <= request.EndDate)
            .WhereIF(!string.IsNullOrEmpty(request.PrgKey), l => l.Fprgkey == request.PrgKey);

        var today = DateTime.Today;
        var trendStartDate = DateTime.Today.AddDays(-(request.TrendDays - 1));

        var totalCount = await baseQuery.Clone().CountAsync();
        var successCount = await baseQuery.Clone().Where(l => l.Fsuccess).CountAsync();
        var failureCount = totalCount - successCount;
        var todayCount = await baseQuery.Clone().Where(l => l.Fdate >= today).CountAsync();

        var byType = await baseQuery.Clone()
            .GroupBy(l => l.Foperationtype)
            .Select(l => new OperationCountByTypeDto
            {
                OperationType = l.Foperationtype,
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .ToListAsync();

        var topUsers = await baseQuery.Clone()
            .GroupBy(l => new { l.Fuserid, l.Fusername })
            .Select(l => new OperationCountByUserDto
            {
                UserId = l.Fuserid,
                UserName = l.Fusername,
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .OrderBy(l => l.Count, OrderByType.Desc)
            .Take(request.TopUserCount)
            .ToListAsync();

        var byModule = await baseQuery.Clone()
            .GroupBy(l => l.Fprgkey)
            .Select(l => new OperationCountByModuleDto
            {
                PrgKey = l.Fprgkey,
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .ToListAsync();

        var dailyTrend = await baseQuery.Clone()
            .Where(l => l.Fdate >= trendStartDate)
            .GroupBy(l => SqlFunc.DateValue(l.Fdate!.Value, DateType.Year))
            .GroupBy(l => SqlFunc.DateValue(l.Fdate!.Value, DateType.Month))
            .GroupBy(l => SqlFunc.DateValue(l.Fdate!.Value, DateType.Day))
            .Select(l => new OperationCountByDateDto
            {
                Date = l.Fdate!.Value.ToString("yyyy-MM-dd"),
                Count = SqlFunc.AggregateCount(l.Uid)
            })
            .OrderBy(l => l.Date)
            .ToListAsync();

        return new OperationLogSummaryDto
        {
            TotalCount = totalCount,
            SuccessCount = successCount,
            FailureCount = failureCount,
            TodayCount = todayCount,
            ByType = byType,
            TopUsers = topUsers,
            ByModule = byModule,
            DailyTrend = dailyTrend
        };
    }

    private string GetClientIp()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return string.Empty;

        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
            return forwardedFor.Split(',')[0].Trim();

        return context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }
}
