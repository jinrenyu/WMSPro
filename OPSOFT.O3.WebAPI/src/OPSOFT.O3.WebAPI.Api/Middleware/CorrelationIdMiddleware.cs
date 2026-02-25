using System.Security.Claims;
using Serilog.Context;

namespace OPSOFT.O3.WebAPI.Api.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string CorrelationIdHeader = "X-Correlation-Id";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault()
                            ?? Guid.NewGuid().ToString("D");

        context.Items["CorrelationId"] = correlationId;

        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationIdHeader] = correlationId;
            return Task.CompletedTask;
        });

        var userId = context.User?.FindFirst("userId")?.Value
                     ?? context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? "anonymous";

        var companyId = context.User?.FindFirst("companyId")?.Value ?? "unknown";

        using (LogContext.PushProperty("CorrelationId", correlationId))
        using (LogContext.PushProperty("UserId", userId))
        using (LogContext.PushProperty("CompanyId", companyId))
        {
            await _next(context);
        }
    }
}
