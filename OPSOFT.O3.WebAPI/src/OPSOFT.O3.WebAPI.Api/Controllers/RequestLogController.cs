using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RequestLogController : ControllerBase
{
    private readonly IRequestLogService _service;
    private readonly ILogExportService _exportService;

    public RequestLogController(IRequestLogService service, ILogExportService exportService)
    {
        _service = service;
        _exportService = exportService;
    }

    /// <summary>
    /// 查询请求日志列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<RequestLogDto>>>> GetList(
        [FromQuery] RequestLogQueryRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<RequestLogDto>>.Ok(result));
    }

    /// <summary>
    /// 导出请求日志CSV
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] RequestLogQueryRequest request)
    {
        var bytes = await _exportService.ExportRequestLogsCsvAsync(request);
        return File(bytes, "text/csv", $"request-logs-{DateTime.Now:yyyyMMddHHmmss}.csv");
    }

    /// <summary>
    /// 获取请求日志统计
    /// </summary>
    [HttpGet("statistics")]
    public async Task<ActionResult<ApiResponse<RequestLogSummaryDto>>> GetStatistics(
        [FromQuery] RequestLogStatisticsRequest request)
    {
        var result = await _service.GetStatisticsAsync(request);
        return Ok(ApiResponse<RequestLogSummaryDto>.Ok(result));
    }
}
