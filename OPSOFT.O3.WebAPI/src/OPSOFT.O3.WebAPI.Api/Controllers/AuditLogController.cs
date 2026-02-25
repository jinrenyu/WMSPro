using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogService _service;
    private readonly ILogExportService _exportService;

    public AuditLogController(IAuditLogService service, ILogExportService exportService)
    {
        _service = service;
        _exportService = exportService;
    }

    /// <summary>
    /// 查询某条记录的变更历史
    /// </summary>
    [HttpGet("{targetUid}/history")]
    public async Task<ActionResult<ApiResponse<List<AuditLogDetailDto>>>> GetHistory(string targetUid)
    {
        var result = await _service.GetHistoryAsync(targetUid);
        return Ok(ApiResponse<List<AuditLogDetailDto>>.Ok(result));
    }

    /// <summary>
    /// 查询审计日志列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<AuditLogListDto>>>> GetList(
        [FromQuery] AuditLogQueryRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<AuditLogListDto>>.Ok(result));
    }

    /// <summary>
    /// 导出审计日志CSV
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] AuditLogQueryRequest request)
    {
        var bytes = await _exportService.ExportAuditLogsCsvAsync(request);
        return File(bytes, "text/csv", $"audit-logs-{DateTime.Now:yyyyMMddHHmmss}.csv");
    }
}
