using Microsoft.Extensions.Diagnostics.HealthChecks;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ISqlSugarClient _db;

    public DatabaseHealthCheck(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _db.Ado.GetScalarAsync("SELECT 1", cancellationToken);
            return HealthCheckResult.Healthy("数据库连接正常");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("数据库连接失败", ex);
        }
    }
}
