namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 请求日志列表DTO
/// </summary>
public class RequestLogDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fmethod { get; set; } = string.Empty;
    public string Fpath { get; set; } = string.Empty;
    public string Fquerystring { get; set; } = string.Empty;
    public int Fstatuscode { get; set; }
    public long Felapsedms { get; set; }
    public string Fip { get; set; } = string.Empty;
    public string Fuserid { get; set; } = string.Empty;
    public string Fuseragent { get; set; } = string.Empty;
    public string Fcorrelationid { get; set; } = string.Empty;
    public DateTime? Frequesttime { get; set; }
    public string Fresponsebody { get; set; } = string.Empty;
}

/// <summary>
/// 请求日志查询参数
/// </summary>
public class RequestLogQueryRequest : PagedRequest
{
    public string? Method { get; set; }
    public string? Path { get; set; }
    public int? StatusCode { get; set; }
    public string? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? MinElapsedMs { get; set; }
}

/// <summary>
/// 请求日志统计参数
/// </summary>
public class RequestLogStatisticsRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int TrendDays { get; set; } = 30;
}

/// <summary>
/// 按HTTP方法统计
/// </summary>
public class RequestCountByMethodDto
{
    public string Method { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// 按状态码统计
/// </summary>
public class RequestCountByStatusCodeDto
{
    public int StatusCode { get; set; }
    public int Count { get; set; }
}

/// <summary>
/// 按路径统计
/// </summary>
public class RequestCountByPathDto
{
    public string Path { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// 按日期统计
/// </summary>
public class RequestCountByDateDto
{
    public string Date { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// 请求日志统计汇总
/// </summary>
public class RequestLogSummaryDto
{
    public int TotalCount { get; set; }
    public int TodayCount { get; set; }
    public double AvgResponseMs { get; set; }
    public int ErrorCount { get; set; }
    public List<RequestCountByMethodDto> ByMethod { get; set; } = new();
    public List<RequestCountByStatusCodeDto> ByStatusCode { get; set; } = new();
    public List<RequestCountByPathDto> TopPaths { get; set; } = new();
    public List<RequestCountByDateDto> DailyTrend { get; set; } = new();
}
