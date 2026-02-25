using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/purchase-order")]
[Authorize]
public class PurchaseOrderController : ControllerBase
{
    private readonly IDocumentService<OdkSrmPOOrder, OdkSrmPOOrderEntry,
        PurchaseOrderListDto, PurchaseOrderDetailDto, CreatePurchaseOrderRequest, UpdatePurchaseOrderRequest> _service;

    public PurchaseOrderController(
        IDocumentService<OdkSrmPOOrder, OdkSrmPOOrderEntry,
            PurchaseOrderListDto, PurchaseOrderDetailDto, CreatePurchaseOrderRequest, UpdatePurchaseOrderRequest> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<PurchaseOrderListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<PurchaseOrderListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PurchaseOrderDetailDto>>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<PurchaseOrderDetailDto>.Fail("单据不存在", 404));
        return Ok(ApiResponse<PurchaseOrderDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PurchaseOrderDetailDto>>> Create([FromBody] CreatePurchaseOrderRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(ApiResponse<PurchaseOrderDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdatePurchaseOrderRequest request)
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

    [HttpPost("{id}/approve")]
    public async Task<ActionResult<ApiResponse<bool>>> Approve(string id)
    {
        var result = await _service.ApproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "审核成功"));
    }

    [HttpPost("{id}/reject")]
    public async Task<ActionResult<ApiResponse<bool>>> Reject(string id, [FromBody] RejectRequest? request = null)
    {
        var result = await _service.RejectAsync(id, request?.Reason);
        return Ok(ApiResponse<bool>.Ok(result, "驳回成功"));
    }

    [HttpPost("{id}/close")]
    public async Task<ActionResult<ApiResponse<bool>>> Close(string id)
    {
        var result = await _service.CloseAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "关闭成功"));
    }
}

/// <summary>
/// 驳回请求（所有单据共用）
/// </summary>
public class RejectRequest
{
    public string? Reason { get; set; }
}
