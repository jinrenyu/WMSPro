using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 退料申请单（真实表 T_PUR_MRAPP / T_PUR_MRAPPENTRY，一主一从）
/// </summary>
[ApiController]
[Route("api/materialreturnapply")]
[Authorize]
public class MaterialReturnApplyController : ControllerBase
{
    private readonly IDocumentService<TPurMrApp, TPurMrAppEntry,
        MaterialReturnApplyListDto, MaterialReturnApplyDetailDto, CreateMaterialReturnApplyRequest, UpdateMaterialReturnApplyRequest> _service;

    public MaterialReturnApplyController(
        IDocumentService<TPurMrApp, TPurMrAppEntry,
            MaterialReturnApplyListDto, MaterialReturnApplyDetailDto, CreateMaterialReturnApplyRequest, UpdateMaterialReturnApplyRequest> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<MaterialReturnApplyListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<MaterialReturnApplyListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<MaterialReturnApplyDetailDto>>> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<MaterialReturnApplyDetailDto>.Fail("单据不存在", 404));
        return Ok(ApiResponse<MaterialReturnApplyDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<MaterialReturnApplyDetailDto>>> Create([FromBody] CreateMaterialReturnApplyRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(ApiResponse<MaterialReturnApplyDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateMaterialReturnApplyRequest request)
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
