using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 采购退料单 / 外购退料（真实表 T_PUR_MRB + ENTRY/ENTRY1/ENTRY2）。
/// 标准单据 CRUD/审核外，另含"扫码解析"与"源单类型（数据驱动）"两个专有端点。
/// </summary>
[ApiController]
[Route("api/purchasereturn")]
[Authorize]
public class PurchaseReturnController : ControllerBase
{
    private readonly IPurchaseReturnService _service;

    public PurchaseReturnController(IPurchaseReturnService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<PurchaseReturnListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<PurchaseReturnListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PurchaseReturnDetailDto>>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<PurchaseReturnDetailDto>.Fail("单据不存在", 404));
        return Ok(ApiResponse<PurchaseReturnDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PurchaseReturnDetailDto>>> Create([FromBody] CreatePurchaseReturnRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(ApiResponse<PurchaseReturnDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdatePurchaseReturnRequest request)
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

    /// <summary>审核（退料出库过账）</summary>
    [HttpPost("{id}/approve")]
    public async Task<ActionResult<ApiResponse<bool>>> Approve(string id)
    {
        var result = await _service.ApproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "审核成功"));
    }

    /// <summary>反审核（冲回，回到草稿）</summary>
    [HttpPost("{id}/unapprove")]
    public async Task<ActionResult<ApiResponse<bool>>> Unapprove(string id, [FromBody] RejectRequest? request = null)
    {
        var result = await _service.RejectAsync(id, request?.Reason);
        return Ok(ApiResponse<bool>.Ok(result, "反审核成功"));
    }

    /// <summary>关闭</summary>
    [HttpPost("{id}/close")]
    public async Task<ActionResult<ApiResponse<bool>>> Close(string id)
    {
        var result = await _service.CloseAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "关闭成功"));
    }

    /// <summary>扫码解析（按条码查条码主档，仅已入库可退料；箱码展开装箱清单）</summary>
    [HttpPost("scan")]
    public async Task<ActionResult<ApiResponse<PurchaseReturnScanResultDto>>> Scan([FromBody] ScanBarcodeRequest request)
    {
        var result = await _service.ScanBarcodeAsync(request);
        return Ok(ApiResponse<PurchaseReturnScanResultDto>.Ok(result));
    }

    /// <summary>源单类型（数据驱动，按目标单据 PUR_MRB 取启用项）</summary>
    [HttpGet("source-bill-types")]
    public async Task<ActionResult<ApiResponse<List<SourceBillTypeDto>>>> GetSourceBillTypes()
    {
        var result = await _service.GetSourceBillTypesAsync();
        return Ok(ApiResponse<List<SourceBillTypeDto>>.Ok(result));
    }
}
