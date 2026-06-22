using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 出入库流程配置（真实表 T_BOS_SELBILL / T_BOS_SELBILLENTRY，一主一从）。
/// 配置后续单据（采购入库单、采购退料单等）的「源单类型」可取值，以及单据可下推为哪些目标单据。
/// </summary>
[ApiController]
[Route("api/selbill")]
[Authorize]
public class SelBillController : ControllerBase
{
    private readonly ISelBillService _service;

    public SelBillController(ISelBillService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<SelBillListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<SelBillListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<SelBillDetailDto>>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<SelBillDetailDto>.Fail("单据不存在", 404));
        return Ok(ApiResponse<SelBillDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<SelBillDetailDto>>> Create([FromBody] CreateSelBillRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(ApiResponse<SelBillDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateSelBillRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }

    /// <summary>审核</summary>
    [HttpPost("{id}/approve")]
    public async Task<ActionResult<ApiResponse<bool>>> Approve(string id)
    {
        var result = await _service.ApproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "审核成功"));
    }

    /// <summary>反审核（回到草稿）</summary>
    [HttpPost("{id}/unapprove")]
    public async Task<ActionResult<ApiResponse<bool>>> Unapprove(string id, [FromBody] RejectRequest? request = null)
    {
        var result = await _service.RejectAsync(id, request?.Reason);
        return Ok(ApiResponse<bool>.Ok(result, "反审核成功"));
    }

    /// <summary>禁用</summary>
    [HttpPost("{id}/disable")]
    public async Task<ActionResult<ApiResponse<bool>>> Disable(string id)
    {
        var result = await _service.DisableAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "禁用成功"));
    }

    /// <summary>反禁用</summary>
    [HttpPost("{id}/enable")]
    public async Task<ActionResult<ApiResponse<bool>>> Enable(string id)
    {
        var result = await _service.EnableAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "反禁用成功"));
    }

    /// <summary>
    /// 按源单类型查可下推的目标单据类型（供采购订单/收料通知单/退料申请单等源单维护页「下推」按钮使用）。
    /// </summary>
    [HttpGet("push-targets")]
    public async Task<ActionResult<ApiResponse<List<PushDownTargetDto>>>> GetPushTargets([FromQuery] string sourceType)
    {
        var result = await _service.GetPushDownTargetsAsync(sourceType);
        return Ok(ApiResponse<List<PushDownTargetDto>>.Ok(result));
    }
}
