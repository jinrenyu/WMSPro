using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OperationLogController : ControllerBase
{
    private readonly IOperationLogService _service;
    private readonly ILogExportService _exportService;

    public OperationLogController(IOperationLogService service, ILogExportService exportService)
    {
        _service = service;
        _exportService = exportService;
    }

    /// <summary>
    /// 查询操作日志列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<OperationLogDto>>>> GetList(
        [FromQuery] OperationLogQueryRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<OperationLogDto>>.Ok(result));
    }

    /// <summary>
    /// 导出操作日志CSV
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] OperationLogQueryRequest request)
    {
        var bytes = await _exportService.ExportOperationLogsCsvAsync(request);
        return File(bytes, "text/csv", $"operation-logs-{DateTime.Now:yyyyMMddHHmmss}.csv");
    }

    /// <summary>
    /// 获取操作日志统计
    /// </summary>
    [HttpGet("statistics")]
    public async Task<ActionResult<ApiResponse<OperationLogSummaryDto>>> GetStatistics(
        [FromQuery] OperationLogStatisticsRequest request)
    {
        var result = await _service.GetStatisticsAsync(request);
        return Ok(ApiResponse<OperationLogSummaryDto>.Ok(result));
    }
}
