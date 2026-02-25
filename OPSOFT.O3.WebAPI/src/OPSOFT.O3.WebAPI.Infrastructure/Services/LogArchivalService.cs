using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 日志归档清理后台服务 — 定期清理过期的操作日志和审计日志
/// </summary>
public class LogArchivalService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<LogArchivalService> _logger;
    private readonly IConfiguration _configuration;

    public LogArchivalService(
        IServiceScopeFactory scopeFactory,
        ILogger<LogArchivalService> logger,
        IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var enabled = _configuration.GetValue<bool>("LogArchival:Enabled", true);
        if (!enabled)
        {
            _logger.LogInformation("日志归档服务已禁用");
            return;
        }

        var runAtHour = _configuration.GetValue<int>("LogArchival:RunAtHour", 2);

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddHours(runAtHour);
            if (nextRun <= now) nextRun = nextRun.AddDays(1);

            var delay = nextRun - now;
            _logger.LogInformation("日志归档服务将在 {NextRun} 执行，等待 {Delay}", nextRun, delay);

            try
            {
                await Task.Delay(delay, stoppingToken);
                await CleanupLogsAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "日志归档执行失败");
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }

    private async Task CleanupLogsAsync(CancellationToken stoppingToken)
    {
        var retentionDays = _configuration.GetValue<int>("LogArchival:RetentionDays", 90);
        var batchSize = _configuration.GetValue<int>("LogArchival:BatchSize", 1000);
        var cutoffDate = DateTime.Now.AddDays(-retentionDays);

        _logger.LogInformation("开始清理 {CutoffDate} 之前的日志记录，批次大小: {BatchSize}", cutoffDate, batchSize);

        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();

        // Clean operation logs in batches
        var totalOperationLogs = 0;
        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var ids = await db.Queryable<SysOperationLog>()
                .Where(l => l.CYmd < cutoffDate)
                .Select(l => l.Uid)
                .Take(batchSize)
                .ToListAsync();
            if (ids.Count == 0) break;
            await db.Deleteable<SysOperationLog>().In(ids).ExecuteCommandAsync();
            totalOperationLogs += ids.Count;
            if (ids.Count < batchSize) break;
        }

        // Clean request logs in batches (shorter retention)
        var requestLogRetentionDays = _configuration.GetValue<int>("LogArchival:RequestLogRetentionDays", 30);
        var requestLogCutoffDate = DateTime.Now.AddDays(-requestLogRetentionDays);
        var totalRequestLogs = 0;
        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var ids = await db.Queryable<SysRequestLog>()
                .Where(l => l.Frequesttime < requestLogCutoffDate)
                .Select(l => l.Uid)
                .Take(batchSize)
                .ToListAsync();
            if (ids.Count == 0) break;
            await db.Deleteable<SysRequestLog>().In(ids).ExecuteCommandAsync();
            totalRequestLogs += ids.Count;
            if (ids.Count < batchSize) break;
        }

        // Clean audit log entries — delete entries whose parent header is old
        var totalAuditEntries = 0;
        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var ids = await db.Queryable<SysAuditLogEntry>()
                .Where(e => SqlFunc.Subqueryable<SysAuditLog>()
                    .Where(h => h.Uid == e.FInterId && h.CYmd < cutoffDate)
                    .Any())
                .Select(e => e.Uid)
                .Take(batchSize)
                .ToListAsync();
            if (ids.Count == 0) break;
            await db.Deleteable<SysAuditLogEntry>().In(ids).ExecuteCommandAsync();
            totalAuditEntries += ids.Count;
            if (ids.Count < batchSize) break;
        }

        // Clean audit log headers
        var totalAuditLogs = 0;
        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var ids = await db.Queryable<SysAuditLog>()
                .Where(l => l.CYmd < cutoffDate)
                .Select(l => l.Uid)
                .Take(batchSize)
                .ToListAsync();
            if (ids.Count == 0) break;
            await db.Deleteable<SysAuditLog>().In(ids).ExecuteCommandAsync();
            totalAuditLogs += ids.Count;
            if (ids.Count < batchSize) break;
        }

        _logger.LogInformation("日志归档完成: 操作日志 {OpCount} 条, 请求日志 {ReqCount} 条, 审计日志 {AuditCount} 条, 审计明细 {EntryCount} 条",
            totalOperationLogs, totalRequestLogs, totalAuditLogs, totalAuditEntries);
    }
}
