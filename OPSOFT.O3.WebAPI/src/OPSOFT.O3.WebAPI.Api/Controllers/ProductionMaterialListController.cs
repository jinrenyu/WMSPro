using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 生产用料清单 / 生产投料单（真实表 T_PRD_PPBOM / T_PRD_PPBOMENTRY）
/// </summary>
[ApiController]
[Route("api/prodmateriallist")]
[Authorize]
public class ProductionMaterialListController : ControllerBase
{
    private readonly IDocumentService<TPrdPpbom, TPrdPpbomentry,
        ProductionMaterialListListDto, ProductionMaterialListDetailDto, CreateProductionMaterialListRequest, UpdateProductionMaterialListRequest> _service;

    public ProductionMaterialListController(
        IDocumentService<TPrdPpbom, TPrdPpbomentry,
            ProductionMaterialListListDto, ProductionMaterialListDetailDto, CreateProductionMaterialListRequest, UpdateProductionMaterialListRequest> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<ProductionMaterialListListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<ProductionMaterialListListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductionMaterialListDetailDto>>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<ProductionMaterialListDetailDto>.Fail("单据不存在", 404));
        return Ok(ApiResponse<ProductionMaterialListDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductionMaterialListDetailDto>>> Create([FromBody] CreateProductionMaterialListRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(ApiResponse<ProductionMaterialListDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateProductionMaterialListRequest request)
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
}
