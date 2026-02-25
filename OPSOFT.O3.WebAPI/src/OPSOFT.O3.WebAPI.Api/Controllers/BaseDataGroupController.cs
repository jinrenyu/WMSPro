using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 基础资料通用分组
/// </summary>
[ApiController]
[Authorize]
[Route("api/base-data-group/{prgKey}")]
public class BaseDataGroupController : ControllerBase
{
    private readonly IBaseDataGroupService _service;

    public BaseDataGroupController(IBaseDataGroupService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<BaseDataGroupListDto>>>> GetList(
        string prgKey, [FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(prgKey, request);
        return Ok(ApiResponse<PagedResult<BaseDataGroupListDto>>.Ok(result));
    }

    [HttpGet("tree")]
    public async Task<ActionResult<ApiResponse<List<BaseDataGroupListDto>>>> GetTree(string prgKey)
    {
        var result = await _service.GetTreeAsync(prgKey);
        return Ok(ApiResponse<List<BaseDataGroupListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<BaseDataGroupDetailDto>>> GetById(string prgKey, string id)
    {
        var result = await _service.GetByIdAsync(prgKey, id);
        if (result == null)
            return Ok(ApiResponse<BaseDataGroupDetailDto>.Fail("记录不存在", 404));
        return Ok(ApiResponse<BaseDataGroupDetailDto>.Ok(result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<BaseDataGroupDetailDto>>> Create(
        string prgKey, [FromBody] CreateBaseDataGroupRequest request)
    {
        var result = await _service.CreateAsync(prgKey, request);
        return Ok(ApiResponse<BaseDataGroupDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(
        string prgKey, string id, [FromBody] UpdateBaseDataGroupRequest request)
    {
        var result = await _service.UpdateAsync(prgKey, id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(string prgKey, string id)
    {
        var result = await _service.DeleteAsync(prgKey, id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup(
        string prgKey, [FromQuery] LookupRequest request)
    {
        var result = await _service.GetLookupAsync(prgKey, request);
        return Ok(ApiResponse<List<LookupDto>>.Ok(result));
    }
}
