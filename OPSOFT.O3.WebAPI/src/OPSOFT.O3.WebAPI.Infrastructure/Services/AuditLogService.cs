using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 审计追踪日志服务实现
/// </summary>
public class AuditLogService : IAuditLogService
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuditLogService> _logger;

    public AuditLogService(
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuditLogService> logger)
    {
        _db = db;
        _currentUser = currentUser;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task LogChangeAsync(string prgKey, string targetUid, string? billNo,
        string description, List<AuditChangeItem> changes)
    {
        if (!changes.Any()) return;

        try
        {
            var auditLog = new SysAuditLog
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = Guid.NewGuid().ToString("N"),
                Fip = GetClientIp(),
                Fuid = targetUid,
                Fprgkey = prgKey,
                Fdescription = description,
                Fbillno = billNo ?? string.Empty,
                FCompanyId = _currentUser.CompanyId ?? string.Empty,
                CYmd = DateTime.Now,
                CUser = _currentUser.UserId ?? string.Empty,
                MYmd = DateTime.Now,
                MUser = _currentUser.UserId ?? string.Empty
            };

            await _db.Insertable(auditLog).ExecuteCommandAsync();

            var entries = changes.Select(c => new SysAuditLogEntry
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = auditLog.Uid,
                Ftype = c.Type,
                Ftablename = c.TableName,
                Fcolumnname = c.ColumnName,
                Fbeforedata = c.BeforeData,
                Fafterdata = c.AfterData,
                Fdata = c.Type == 1 ? c.AfterData : string.Empty,
                Fentryid = c.EntryId,
                Fdetailid = c.DetailId,
                FCompanyId = _currentUser.CompanyId ?? string.Empty,
                CYmd = DateTime.Now,
                CUser = _currentUser.UserId ?? string.Empty,
                MYmd = DateTime.Now,
                MUser = _currentUser.UserId ?? string.Empty
            }).ToList();

            await _db.Insertable(entries).ExecuteCommandAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "记录审计日志失败: PrgKey={PrgKey}, TargetUid={TargetUid}", prgKey, targetUid);
        }
    }

    public async Task<List<AuditLogDetailDto>> GetHistoryAsync(string targetUid)
    {
        var logs = await _db.Queryable<SysAuditLog>()
            .Where(l => l.Fuid == targetUid && !l.FDeleted)
            .OrderBy(l => l.CYmd, OrderByType.Desc)
            .ToListAsync();

        if (!logs.Any()) return new List<AuditLogDetailDto>();

        var logUids = logs.Select(l => l.Uid).ToList();
        var allEntries = await _db.Queryable<SysAuditLogEntry>()
            .Where(e => logUids.Contains(e.FInterId) && !e.FDeleted)
            .ToListAsync();

        return logs.Select(l => new AuditLogDetailDto
        {
            Uid = l.Uid,
            Fdescription = l.Fdescription,
            CUser = l.CUser,
            CYmd = l.CYmd,
            Entries = allEntries
                .Where(e => e.FInterId == l.Uid)
                .Select(e => new AuditLogEntryDto
                {
                    Ftype = e.Ftype,
                    Ftablename = e.Ftablename,
                    Fcolumnname = e.Fcolumnname,
                    Fbeforedata = e.Fbeforedata,
                    Fafterdata = e.Fafterdata,
                    Fentryid = e.Fentryid
                }).ToList()
        }).ToList();
    }

    public async Task<PagedResult<AuditLogListDto>> GetPagedListAsync(AuditLogQueryRequest request)
    {
        var query = _db.Queryable<SysAuditLog>()
            .Where(l => !l.FDeleted)
            .WhereIF(!string.IsNullOrEmpty(request.PrgKey), l => l.Fprgkey == request.PrgKey)
            .WhereIF(!string.IsNullOrEmpty(request.TargetUid), l => l.Fuid == request.TargetUid)
            .WhereIF(request.StartDate.HasValue, l => l.CYmd >= request.StartDate)
            .WhereIF(request.EndDate.HasValue, l => l.CYmd <= request.EndDate)
            .WhereIF(!string.IsNullOrEmpty(request.Keyword), l =>
                l.Fdescription.Contains(request.Keyword!) ||
                l.Fbillno.Contains(request.Keyword!));

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(l => l.CYmd, OrderByType.Desc)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        // 批量查询entry数量
        var uids = items.Select(l => l.Uid).ToList();
        var entryCountDict = new Dictionary<string, int>();
        if (uids.Any())
        {
            var counts = await _db.Queryable<SysAuditLogEntry>()
                .Where(e => uids.Contains(e.FInterId))
                .GroupBy(e => e.FInterId)
                .Select(e => new { FInterId = e.FInterId, Count = SqlFunc.AggregateCount(e.Uid) })
                .ToListAsync();
            foreach (var c in counts)
                entryCountDict[c.FInterId] = c.Count;
        }

        return new PagedResult<AuditLogListDto>
        {
            Items = items.Select(l => new AuditLogListDto
            {
                Uid = l.Uid,
                Fuid = l.Fuid,
                Fprgkey = l.Fprgkey,
                Fdescription = l.Fdescription,
                Fbillno = l.Fbillno,
                Fip = l.Fip,
                CUser = l.CUser,
                CYmd = l.CYmd,
                EntryCount = entryCountDict.GetValueOrDefault(l.Uid, 0)
            }).ToList(),
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
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
