using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Authorization;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// ERP 集成配置（字段自定义）—— 真实表 T_SYN_INFO / T_SYN_INFOENTRY / T_SYN_INFOENTRY1。
/// 提供配置单据的 CRUD + 审核，手动同步，以及目标目录/功能代码下拉。
/// </summary>
[ApiController]
[Route("api/erpsyncconfig")]
[Authorize]
public class ErpSyncConfigController : ControllerBase
{
    private readonly IErpSyncConfigService _service;

    public ErpSyncConfigController(IErpSyncConfigService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ErpSyncConfigListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<ErpSyncConfigListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ErpSyncConfigDetailDto>>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<ErpSyncConfigDetailDto>.Fail("配置不存在", 404));
        return Ok(ApiResponse<ErpSyncConfigDetailDto>.Ok(result));
    }

    [HttpPost]
    [RequirePermission("erpsync:add")]
    public async Task<ActionResult<ApiResponse<ErpSyncConfigDetailDto>>> Create([FromBody] CreateErpSyncConfigRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(ApiResponse<ErpSyncConfigDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    [RequirePermission("erpsync:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateErpSyncConfigRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    [RequirePermission("erpsync:delete")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }

    [HttpPost("{id}/approve")]
    [RequirePermission("erpsync:approve")]
    public async Task<ActionResult<ApiResponse<bool>>> Approve(string id)
    {
        var result = await _service.ApproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "审核成功"));
    }

    [HttpPost("{id}/unapprove")]
    [RequirePermission("erpsync:approve")]
    public async Task<ActionResult<ApiResponse<bool>>> Unapprove(string id, [FromBody] RejectRequest? request = null)
    {
        var result = await _service.RejectAsync(id, request?.Reason);
        return Ok(ApiResponse<bool>.Ok(result, "反审核成功"));
    }

    /// <summary>手动执行一次同步</summary>
    [HttpPost("{id}/sync")]
    [RequirePermission("erpsync:sync")]
    public async Task<ActionResult<ApiResponse<ErpSyncRunResultDto>>> Sync(string id)
    {
        var result = await _service.RunSyncAsync(id);
        return Ok(ApiResponse<ErpSyncRunResultDto>.Ok(result, result.Message));
    }

    /// <summary>目标实体目录（反射）</summary>
    [HttpGet("target-tables")]
    public async Task<ActionResult<ApiResponse<List<ErpTargetTableDto>>>> GetTargetTables([FromQuery] string? keyword)
    {
        var result = await _service.GetTargetTablesAsync(keyword);
        return Ok(ApiResponse<List<ErpTargetTableDto>>.Ok(result));
    }

    /// <summary>目标字段目录（反射）</summary>
    [HttpGet("target-fields")]
    public async Task<ActionResult<ApiResponse<List<ErpTargetFieldDto>>>> GetTargetFields([FromQuery] string table)
    {
        var result = await _service.GetTargetFieldsAsync(table);
        return Ok(ApiResponse<List<ErpTargetFieldDto>>.Ok(result));
    }

    /// <summary>功能代码 / ERP表单ID 候选（SYS_BILLTEMPLATE）</summary>
    [HttpGet("bill-templates")]
    public async Task<ActionResult<ApiResponse<List<ErpBillTemplateDto>>>> GetBillTemplates([FromQuery] string? keyword)
    {
        var result = await _service.GetBillTemplatesAsync(keyword);
        return Ok(ApiResponse<List<ErpBillTemplateDto>>.Ok(result));
    }
}
