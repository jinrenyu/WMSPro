namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 操作日志统计 — 按操作类型
/// </summary>
public class OperationCountByTypeDto
{
    public string OperationType { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// 操作日志统计 — 按用户
/// </summary>
public class OperationCountByUserDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// 操作日志统计 — 按模块
/// </summary>
public class OperationCountByModuleDto
{
    public string PrgKey { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// 操作日志统计 — 按日期趋势
/// </summary>
public class OperationCountByDateDto
{
    public string Date { get; set; } = string.Empty;
    public int Count { get; set; }
}

/// <summary>
/// 操作日志统计汇总
/// </summary>
public class OperationLogSummaryDto
{
    public int TotalCount { get; set; }
    public int SuccessCount { get; set; }
    public int FailureCount { get; set; }
    public int TodayCount { get; set; }
    public List<OperationCountByTypeDto> ByType { get; set; } = new();
    public List<OperationCountByUserDto> TopUsers { get; set; } = new();
    public List<OperationCountByModuleDto> ByModule { get; set; } = new();
    public List<OperationCountByDateDto> DailyTrend { get; set; } = new();
}

/// <summary>
/// 统计查询请求
/// </summary>
public class OperationLogStatisticsRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? PrgKey { get; set; }
    public int TopUserCount { get; set; } = 10;
    public int TrendDays { get; set; } = 30;
}
