using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Authorization;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 生产订单 / 生产任务单（真实表 T_PRD_MO / T_PRD_MOENTRY）
/// </summary>
[ApiController]
[Route("api/productionorder")]
[Authorize]
public class ProductionOrderController : ControllerBase
{
    private readonly IDocumentService<TPrdMo, TPrdMoentry,
        ProductionOrderListDto, ProductionOrderDetailDto, CreateProductionOrderRequest, UpdateProductionOrderRequest> _service;
    private readonly IProductionReleaseService _release;

    public ProductionOrderController(
        IDocumentService<TPrdMo, TPrdMoentry,
            ProductionOrderListDto, ProductionOrderDetailDto, CreateProductionOrderRequest, UpdateProductionOrderRequest> service,
        IProductionReleaseService release)
    {
        _service = service;
        _release = release;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ProductionOrderListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<ProductionOrderListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductionOrderDetailDto>>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<ProductionOrderDetailDto>.Fail("单据不存在", 404));
        return Ok(ApiResponse<ProductionOrderDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductionOrderDetailDto>>> Create([FromBody] CreateProductionOrderRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(ApiResponse<ProductionOrderDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateProductionOrderRequest request)
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

    /// <summary>关闭</summary>
    [HttpPost("{id}/close")]
    public async Task<ActionResult<ApiResponse<bool>>> Close(string id)
    {
        var result = await _service.CloseAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "关闭成功"));
    }

    /// <summary>下达：按选中明细行的 BOM 生成生产用料清单（id=生产订单主表 Uid）</summary>
    [HttpPost("{id}/release")]
    [RequirePermission("productionorder:release")]
    public async Task<ActionResult<ApiResponse<ProductionReleaseResultDto>>> Release(string id, [FromBody] ProductionReleaseRequest request)
    {
        var result = await _release.ReleaseAsync(id, request?.EntryUids ?? new List<string>());
        var msg = result.SuccessCount > 0 ? $"下达成功 {result.SuccessCount} 行" : "未下达任何明细";
        return Ok(ApiResponse<ProductionReleaseResultDto>.Ok(result, msg));
    }

    /// <summary>反下达：删除选中明细行对应的生产用料清单</summary>
    [HttpPost("{id}/unrelease")]
    [RequirePermission("productionorder:unrelease")]
    public async Task<ActionResult<ApiResponse<ProductionReleaseResultDto>>> Unrelease(string id, [FromBody] ProductionReleaseRequest request)
    {
        var result = await _release.UnreleaseAsync(id, request?.EntryUids ?? new List<string>());
        var msg = result.SuccessCount > 0 ? $"反下达成功 {result.SuccessCount} 行" : "未反下达任何明细";
        return Ok(ApiResponse<ProductionReleaseResultDto>.Ok(result, msg));
    }
}
