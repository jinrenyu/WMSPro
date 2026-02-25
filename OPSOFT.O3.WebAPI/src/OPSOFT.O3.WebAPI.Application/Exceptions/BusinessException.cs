using System.Net;

namespace OPSOFT.O3.WebAPI.Application.Exceptions;

/// <summary>
/// 业务异常基类
/// </summary>
public class BusinessException : Exception
{
    public string ErrorCode { get; }
    public HttpStatusCode HttpStatusCode { get; }

    public BusinessException(string message, string errorCode = "BUSINESS_ERROR", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        ErrorCode = errorCode;
        HttpStatusCode = httpStatusCode;
    }

    public BusinessException(string message, Exception innerException, string errorCode = "BUSINESS_ERROR", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        HttpStatusCode = httpStatusCode;
    }
}

/// <summary>
/// 资源未找到异常 (404)
/// </summary>
public class NotFoundException : BusinessException
{
    public NotFoundException(string message)
        : base(message, "NOT_FOUND", HttpStatusCode.NotFound) { }

    public NotFoundException(string entityName, object key)
        : base($"实体 '{entityName}' (键: {key}) 未找到", "NOT_FOUND", HttpStatusCode.NotFound) { }
}

/// <summary>
/// 禁止访问异常 (403)
/// </summary>
public class ForbiddenException : BusinessException
{
    public ForbiddenException(string message = "没有权限执行此操作")
        : base(message, "FORBIDDEN", HttpStatusCode.Forbidden) { }
}

/// <summary>
/// 验证异常 (422)
/// </summary>
public class ValidationException : BusinessException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("一个或多个验证错误发生", "VALIDATION_ERROR", HttpStatusCode.UnprocessableEntity)
    {
        Errors = errors;
    }

    public ValidationException(string field, string error)
        : base("验证错误", "VALIDATION_ERROR", HttpStatusCode.UnprocessableEntity)
    {
        Errors = new Dictionary<string, string[]> { { field, new[] { error } } };
    }
}
