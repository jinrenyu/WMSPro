using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly int _slowThresholdMs;
    private readonly int _criticalThresholdMs;
    private readonly HashSet<string> _excludePaths;
    private readonly List<string> _sensitiveFields;
    private readonly bool _logRequestBody;
    private readonly int _maxBodyLogLength;
    private readonly bool _persistToDatabase;
    private readonly int _maxResponseBodyLogLength;
    private readonly bool _maskSensitiveData;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger,
        IConfiguration configuration, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _logger = logger;
        _scopeFactory = scopeFactory;

        var section = configuration.GetSection("RequestLogging");
        _slowThresholdMs = section.GetValue<int>("SlowRequestThresholdMs", 500);
        _criticalThresholdMs = section.GetValue<int>("CriticalRequestThresholdMs", 2000);
        _excludePaths = section.GetSection("ExcludePaths").Get<List<string>>()?.ToHashSet(StringComparer.OrdinalIgnoreCase)
                        ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        _sensitiveFields = section.GetSection("SensitiveFields").Get<List<string>>() ?? new List<string>();
        _logRequestBody = section.GetValue<bool>("LogRequestBody", false);
        _maxBodyLogLength = section.GetValue<int>("MaxRequestBodyLogLength", 4096);
        _persistToDatabase = section.GetValue<bool>("PersistToDatabase", false);
        _maxResponseBodyLogLength = section.GetValue<int>("MaxResponseBodyLogLength", 4096);
        _maskSensitiveData = section.GetValue<bool>("MaskSensitiveData", true);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? "/";

        if (_excludePaths.Any(ep => path.StartsWith(ep, StringComparison.OrdinalIgnoreCase))
            || context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        if (_logRequestBody)
        {
            context.Request.EnableBuffering();
        }

        var sw = Stopwatch.StartNew();

        string? requestBody = null;
        if (_logRequestBody && context.Request.ContentLength > 0)
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            requestBody = await reader.ReadToEndAsync();
            if (requestBody.Length > _maxBodyLogLength)
                requestBody = requestBody[.._maxBodyLogLength] + "...(truncated)";
            if (_maskSensitiveData)
                requestBody = MaskSensitiveFields(requestBody);
            context.Request.Body.Position = 0;
        }

        // Capture response body if persisting to database
        string? responseBody = null;
        Stream? originalBodyStream = null;
        MemoryStream? responseBodyStream = null;

        if (_persistToDatabase)
        {
            originalBodyStream = context.Response.Body;
            responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;
        }

        try
        {
            await _next(context);
        }
        finally
        {
            if (_persistToDatabase && originalBodyStream != null && responseBodyStream != null)
            {
                responseBodyStream.Position = 0;
                using var sr = new StreamReader(responseBodyStream, Encoding.UTF8, leaveOpen: true);
                responseBody = await sr.ReadToEndAsync();
                if (responseBody.Length > _maxResponseBodyLogLength)
                    responseBody = responseBody[.._maxResponseBodyLogLength] + "...(truncated)";
                if (_maskSensitiveData)
                    responseBody = MaskSensitiveFields(responseBody);

                responseBodyStream.Position = 0;
                await responseBodyStream.CopyToAsync(originalBodyStream);
                context.Response.Body = originalBodyStream;
                responseBodyStream.Dispose();
            }
        }

        sw.Stop();
        var elapsedMs = sw.ElapsedMilliseconds;
        var method = context.Request.Method;
        var statusCode = context.Response.StatusCode;
        var remoteIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var userId = context.Items["CorrelationId"] != null
            ? (context.User?.FindFirst("userId")?.Value ?? "anonymous")
            : "anonymous";

        var message = "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms [IP: {RemoteIp}] [User: {UserId}]";

        if (elapsedMs > _criticalThresholdMs)
        {
            _logger.LogError(message, method, path, statusCode, elapsedMs, remoteIp, userId);
        }
        else if (elapsedMs > _slowThresholdMs)
        {
            _logger.LogWarning(message, method, path, statusCode, elapsedMs, remoteIp, userId);
        }
        else
        {
            _logger.LogInformation(message, method, path, statusCode, elapsedMs, remoteIp, userId);
        }

        if (requestBody != null)
        {
            _logger.LogDebug("Request body for {Method} {Path}: {RequestBody}", method, path, requestBody);
        }

        // Fire-and-forget: persist to database
        if (_persistToDatabase)
        {
            var queryString = context.Request.QueryString.Value ?? string.Empty;
            var userAgent = context.Request.Headers.UserAgent.ToString();
            var correlationId = context.Items["CorrelationId"]?.ToString() ?? string.Empty;
            var capturedResponseBody = responseBody ?? string.Empty;
            var capturedRequestBody = requestBody ?? string.Empty;

            _ = Task.Run(async () =>
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IRequestLogService>();
                    await service.LogAsync(method, path, queryString, statusCode, elapsedMs,
                        remoteIp, userId, userAgent, correlationId, capturedResponseBody, capturedRequestBody);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "请求日志落库失败: {Method} {Path}", method, path);
                }
            });
        }
    }

    private string MaskSensitiveFields(string body)
    {
        foreach (var field in _sensitiveFields)
        {
            var pattern = $@"(""{Regex.Escape(field)}""\s*:\s*)""[^""]*""";
            body = Regex.Replace(body, pattern, @$"$1""***""", RegexOptions.IgnoreCase);
        }
        return body;
    }
}
