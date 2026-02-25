using System.Text;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 日志导出服务实现
/// </summary>
public class LogExportService : ILogExportService
{
    private readonly ISqlSugarClient _db;
    private readonly ILogger<LogExportService> _logger;
    private const int MaxExportRows = 10000;

    public LogExportService(ISqlSugarClient db, ILogger<LogExportService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<byte[]> ExportOperationLogsCsvAsync(OperationLogQueryRequest request)
    {
        var items = await _db.Queryable<SysOperationLog>()
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
                l.Fprgkey.Contains(request.Keyword!))
            .OrderBy(l => l.Fdate, OrderByType.Desc)
            .Take(MaxExportRows)
            .ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("日期,用户ID,用户名,模块,操作类型,目标ID,单据号,IP地址,是否成功,错误信息,描述");

        foreach (var item in items)
        {
            sb.AppendLine($"{item.Fdate:yyyy-MM-dd HH:mm:ss},{EscapeCsv(item.Fuserid)},{EscapeCsv(item.Fusername)},{EscapeCsv(item.Fprgkey)},{EscapeCsv(item.Foperationtype)},{EscapeCsv(item.Ftargetuid)},{EscapeCsv(item.Fbillno)},{EscapeCsv(item.Fip)},{(item.Fsuccess ? "是" : "否")},{EscapeCsv(item.Ferrormsg)},{EscapeCsv(item.Fstatement)}");
        }

        return PrependBom(sb);
    }

    public async Task<byte[]> ExportAuditLogsCsvAsync(AuditLogQueryRequest request)
    {
        var logs = await _db.Queryable<SysAuditLog>()
            .Where(l => !l.FDeleted)
            .WhereIF(!string.IsNullOrEmpty(request.PrgKey), l => l.Fprgkey == request.PrgKey)
            .WhereIF(!string.IsNullOrEmpty(request.TargetUid), l => l.Fuid == request.TargetUid)
            .WhereIF(request.StartDate.HasValue, l => l.CYmd >= request.StartDate)
            .WhereIF(request.EndDate.HasValue, l => l.CYmd <= request.EndDate)
            .WhereIF(!string.IsNullOrEmpty(request.Keyword), l =>
                l.Fdescription.Contains(request.Keyword!) ||
                l.Fbillno.Contains(request.Keyword!))
            .OrderBy(l => l.CYmd, OrderByType.Desc)
            .Take(MaxExportRows)
            .ToListAsync();

        var logUids = logs.Select(l => l.Uid).ToList();
        var entries = logUids.Any()
            ? await _db.Queryable<SysAuditLogEntry>()
                .Where(e => logUids.Contains(e.FInterId) && !e.FDeleted)
                .ToListAsync()
            : new List<SysAuditLogEntry>();

        var sb = new StringBuilder();
        sb.AppendLine("日期,用户,模块,单据号,描述,变更类型,表名,字段名,变更前,变更后");

        foreach (var log in logs)
        {
            var logEntries = entries.Where(e => e.FInterId == log.Uid).ToList();
            if (logEntries.Any())
            {
                foreach (var entry in logEntries)
                {
                    var typeName = entry.Ftype switch
                    {
                        1 => "新增",
                        2 => "修改",
                        3 => "删除",
                        _ => entry.Ftype.ToString()
                    };
                    sb.AppendLine($"{log.CYmd:yyyy-MM-dd HH:mm:ss},{EscapeCsv(log.CUser)},{EscapeCsv(log.Fprgkey)},{EscapeCsv(log.Fbillno)},{EscapeCsv(log.Fdescription)},{typeName},{EscapeCsv(entry.Ftablename)},{EscapeCsv(entry.Fcolumnname)},{EscapeCsv(entry.Fbeforedata)},{EscapeCsv(entry.Fafterdata)}");
                }
            }
            else
            {
                sb.AppendLine($"{log.CYmd:yyyy-MM-dd HH:mm:ss},{EscapeCsv(log.CUser)},{EscapeCsv(log.Fprgkey)},{EscapeCsv(log.Fbillno)},{EscapeCsv(log.Fdescription)},,,,,");
            }
        }

        return PrependBom(sb);
    }

    public async Task<byte[]> ExportRequestLogsCsvAsync(RequestLogQueryRequest request)
    {
        var items = await _db.Queryable<SysRequestLog>()
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
                l.Fip.Contains(request.Keyword!))
            .OrderBy(l => l.Frequesttime, OrderByType.Desc)
            .Take(MaxExportRows)
            .ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("请求时间,HTTP方法,路径,查询字符串,状态码,响应时间(ms),IP,用户ID,UA,关联ID,响应体");

        foreach (var item in items)
        {
            sb.AppendLine($"{item.Frequesttime:yyyy-MM-dd HH:mm:ss},{EscapeCsv(item.Fmethod)},{EscapeCsv(item.Fpath)},{EscapeCsv(item.Fquerystring)},{item.Fstatuscode},{item.Felapsedms},{EscapeCsv(item.Fip)},{EscapeCsv(item.Fuserid)},{EscapeCsv(item.Fuseragent)},{EscapeCsv(item.Fcorrelationid)},{EscapeCsv(item.Fresponsebody)}");
        }

        return PrependBom(sb);
    }

    private static byte[] PrependBom(StringBuilder sb)
    {
        var bom = Encoding.UTF8.GetPreamble();
        var content = Encoding.UTF8.GetBytes(sb.ToString());
        var result = new byte[bom.Length + content.Length];
        bom.CopyTo(result, 0);
        content.CopyTo(result, bom.Length);
        return result;
    }

    private static string EscapeCsv(string? value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }
}
