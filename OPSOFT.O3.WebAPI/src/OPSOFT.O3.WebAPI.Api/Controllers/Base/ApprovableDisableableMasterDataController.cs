using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers.Base;

/// <summary>
/// 可审核+可禁用基础资料通用 Controller 基类 — 审核与禁用为独立能力
/// </summary>
public abstract class ApprovableDisableableMasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : MasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, IApprovable, IDisableable, new()
{
    private readonly IApprovableDisableableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> _service;

    protected ApprovableDisableableMasterDataController(
        IApprovableDisableableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> service)
        : base(service)
    {
        _service = service;
    }

    /// <summary>
    /// 审核
    /// </summary>
    [HttpPut("{id}/approve")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Approve(string id)
    {
        var result = await _service.ApproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "审核成功"));
    }

    /// <summary>
    /// 反审核
    /// </summary>
    [HttpPut("{id}/unapprove")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Unapprove(string id)
    {
        var result = await _service.UnapproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "反审核成功"));
    }

    /// <summary>
    /// 禁用
    /// </summary>
    [HttpPut("{id}/disable")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Disable(string id)
    {
        var result = await _service.DisableAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "禁用成功"));
    }

    /// <summary>
    /// 反禁用
    /// </summary>
    [HttpPut("{id}/enable")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Enable(string id)
    {
        var result = await _service.EnableAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "反禁用成功"));
    }
}
