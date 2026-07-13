using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 后台服务 — 定期清理过期 / 已消费的短信验证码会话(SYS_SMSCODE)。
/// 短信码为临时数据，采用物理删除。
/// </summary>
public class SmsCodeCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SmsCodeCleanupService> _logger;

    public SmsCodeCleanupService(IServiceScopeFactory scopeFactory, ILogger<SmsCodeCleanupService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            // 每天凌晨 3:30 执行
            var nextRun = now.Date.AddDays(1).AddHours(3).AddMinutes(30);
            var delay = nextRun - now;

            _logger.LogInformation("短信验证码清理服务将在 {NextRun} 执行", nextRun);

            try
            {
                await Task.Delay(delay, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();

                // 删除已消费的，以及过期超过 1 天的
                var cutoff = DateTime.Now.AddDays(-1);
                var deleted = await db.Deleteable<SysSmsCode>()
                    .Where(x => x.IsConsumed || x.ExpiresAt < cutoff)
                    .ExecuteCommandAsync();

                if (deleted > 0)
                {
                    _logger.LogInformation("清理了 {Count} 条过期/已消费的短信验证码", deleted);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "短信验证码清理服务执行出错");
            }
        }
    }
}
