using System.Net;
using System.Text.Json;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Exceptions;

namespace OPSOFT.O3.WebAPI.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var correlationId = context.Items["CorrelationId"]?.ToString() ?? "unknown";
        var userId = context.User?.FindFirst("userId")?.Value ?? "anonymous";
        var requestPath = context.Request.Path.Value;

        var (statusCode, message, errorCode) = exception switch
        {
            ValidationException validationEx => ((int)validationEx.HttpStatusCode, validationEx.Message, validationEx.ErrorCode),
            BusinessException businessEx => ((int)businessEx.HttpStatusCode, businessEx.Message, businessEx.ErrorCode),
            UnauthorizedAccessException => (401, "未授权访问", "UNAUTHORIZED"),
            KeyNotFoundException => (404, "请求的资源不存在", "NOT_FOUND"),
            ArgumentException argEx => (400, argEx.Message, "BAD_REQUEST"),
            InvalidOperationException invEx => (400, invEx.Message, "BAD_REQUEST"),
            _ => (500, "服务器内部错误，请稍后重试", "INTERNAL_ERROR")
        };

        // Log based on exception type
        if (exception is BusinessException)
        {
            _logger.LogWarning(exception,
                "业务异常 [{ErrorCode}] CorrelationId: {CorrelationId}, UserId: {UserId}, Path: {RequestPath}, Message: {Message}",
                errorCode, correlationId, userId, requestPath, exception.Message);
        }
        else if (statusCode >= 500)
        {
            _logger.LogError(exception,
                "系统异常 [{ErrorCode}] CorrelationId: {CorrelationId}, UserId: {UserId}, Path: {RequestPath}, Message: {Message}",
                errorCode, correlationId, userId, requestPath, exception.Message);
        }
        else
        {
            _logger.LogWarning(exception,
                "请求异常 [{ErrorCode}] CorrelationId: {CorrelationId}, UserId: {UserId}, Path: {RequestPath}, Message: {Message}",
                errorCode, correlationId, userId, requestPath, exception.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            code = statusCode,
            message = statusCode >= 500 && !_environment.IsDevelopment() ? "服务器内部错误，请稍后重试" : message,
            errorCode,
            correlationId,
            errors = exception is ValidationException ve ? ve.Errors : null,
            stackTrace = _environment.IsDevelopment() ? exception.StackTrace : null
        };

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}
