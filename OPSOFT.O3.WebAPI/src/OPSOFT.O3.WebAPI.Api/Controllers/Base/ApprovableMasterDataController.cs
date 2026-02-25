using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers.Base;

/// <summary>
/// 可审核基础资料通用 Controller 基类 — 自动添加审核/反审核端点
/// </summary>
public abstract class ApprovableMasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : MasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, IApprovable, new()
{
    private readonly IApprovableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> _approvableService;

    protected ApprovableMasterDataController(
        IApprovableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> service)
        : base(service)
    {
        _approvableService = service;
    }

    /// <summary>
    /// 审核
    /// </summary>
    [HttpPut("{id}/approve")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Approve(string id)
    {
        var result = await _approvableService.ApproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "审核成功"));
    }

    /// <summary>
    /// 反审核
    /// </summary>
    [HttpPut("{id}/unapprove")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Unapprove(string id)
    {
        var result = await _approvableService.UnapproveAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "反审核成功"));
    }
}
