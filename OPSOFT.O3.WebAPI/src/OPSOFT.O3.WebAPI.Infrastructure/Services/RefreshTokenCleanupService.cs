using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 后台服务 — 定期清理过期的 Refresh Token
/// </summary>
public class RefreshTokenCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RefreshTokenCleanupService> _logger;
    private readonly IConfiguration _configuration;

    public RefreshTokenCleanupService(
        IServiceScopeFactory scopeFactory,
        ILogger<RefreshTokenCleanupService> logger,
        IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 每天凌晨 3 点执行清理
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddDays(1).AddHours(3);
            var delay = nextRun - now;

            _logger.LogInformation("Refresh Token 清理服务将在 {NextRun} 执行", nextRun);

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
                var repository = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();

                // 删除 30 天前过期的和已撤销的 token
                var cutoff = DateTime.Now.AddDays(-30);
                var deleted = await repository.DeleteExpiredAsync(cutoff);

                if (deleted > 0)
                {
                    _logger.LogInformation("清理了 {Count} 条过期的 Refresh Token", deleted);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Refresh Token 清理服务执行出错");
            }
        }
    }
}
