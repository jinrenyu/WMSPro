using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers.Base;

/// <summary>
/// 基础资料通用 Controller 基类
/// </summary>
[ApiController]
[Authorize]
public abstract class MasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> : ControllerBase
    where TEntity : BaseEntity, new()
{
    protected readonly ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> Service;

    protected MasterDataController(ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> service)
    {
        Service = service;
    }

    [HttpGet]
    public virtual async Task<ActionResult<ApiResponse<PagedResult<TListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await Service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<TListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<ApiResponse<TDetailDto>>> GetById(string id)
    {
        var result = await Service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<TDetailDto>.Fail("记录不存在", 404));
        return Ok(ApiResponse<TDetailDto>.Ok(result));
    }

    [HttpPost]
    public virtual async Task<ActionResult<ApiResponse<TDetailDto>>> Create([FromBody] TCreateDto request)
    {
        var result = await Service.CreateAsync(request);
        return Ok(ApiResponse<TDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] TUpdateDto request)
    {
        var result = await Service.UpdateAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
    {
        var result = await Service.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }

    [HttpGet("lookup")]
    public virtual async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var result = await Service.GetLookupAsync(request);
        return Ok(ApiResponse<List<LookupDto>>.Ok(result));
    }
}
