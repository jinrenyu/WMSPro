using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Infrastructure.ErpIntegration;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// ERP 自动定时同步后台服务：按轮询间隔扫描「已启用 + 已审核」的同步任务，
/// 依据 最后同步时间 + 同步频率×时间单位 判断是否到点，到点则调用通用同步引擎执行一次。
/// </summary>
public class ErpSyncBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ErpSyncBackgroundService> _logger;
    private readonly ErpIntegrationOptions _options;

    public ErpSyncBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<ErpSyncBackgroundService> logger,
        IOptions<ErpIntegrationOptions> options)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_options.SchedulerEnabled)
        {
            _logger.LogInformation("ERP 定时同步服务已禁用（ErpIntegration:SchedulerEnabled=false）");
            return;
        }

        var pollSeconds = Math.Max(5, _options.SchedulerPollSeconds);
        _logger.LogInformation("ERP 定时同步服务已启动，轮询间隔 {Poll}s", pollSeconds);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await TickAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERP 定时同步轮询异常");
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(pollSeconds), stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    private async Task TickAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();

        // 已启用 + 已审核(40) 的任务
        var tasks = await db.Queryable<TSynInfo>()
            .Where(t => t.Fisenable && t.FStatus == 40 && !t.FDeleted)
            .ToListAsync();

        var now = DateTime.Now;
        foreach (var task in tasks)
        {
            stoppingToken.ThrowIfCancellationRequested();
            if (!IsDue(task, now)) continue;

            _logger.LogInformation("ERP 定时同步触发：{Number} - {Name}", task.Fnumber, task.Fname);
            try
            {
                var engine = scope.ServiceProvider.GetRequiredService<IErpSyncEngine>();
                var result = await engine.RunAsync(task.Uid);
                _logger.LogInformation("ERP 定时同步完成：{Number} 成功{Ok}/失败{Fail}", task.Fnumber, result.SuccessCount, result.FailCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERP 定时同步执行失败：{Number}", task.Fnumber);
            }
        }
    }

    /// <summary>是否到点：从未同步过则立即执行；否则 最后同步时间 + 频率×单位秒 &lt;= now</summary>
    private static bool IsDue(TSynInfo task, DateTime now)
    {
        if (task.Fsynrate <= 0) return false;               // 频率非法不触发，避免空转刷库
        if (task.Ftimestamp is not DateTime last) return true;
        var intervalSeconds = task.Fsynrate * UnitSeconds(task.Ftimetype);
        return last.AddSeconds(intervalSeconds) <= now;
    }

    private static long UnitSeconds(string timetype) => timetype switch
    {
        "1" => 1,
        "2" => 60,
        "3" => 3600,
        "4" => 86400,
        _ => 1
    };
}
