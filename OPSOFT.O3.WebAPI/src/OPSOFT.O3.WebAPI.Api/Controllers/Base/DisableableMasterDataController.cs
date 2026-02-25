using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers.Base;

/// <summary>
/// 纯禁用基础资料通用 Controller 基类 — 只有禁用/反禁用端点，不包含审核
/// </summary>
public abstract class DisableableMasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : MasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, IDisableable, new()
{
    private readonly IDisableableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> _disableableService;

    protected DisableableMasterDataController(
        IDisableableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> service)
        : base(service)
    {
        _disableableService = service;
    }

    /// <summary>
    /// 禁用
    /// </summary>
    [HttpPut("{id}/disable")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Disable(string id)
    {
        var result = await _disableableService.DisableAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "禁用成功"));
    }

    /// <summary>
    /// 反禁用
    /// </summary>
    [HttpPut("{id}/enable")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Enable(string id)
    {
        var result = await _disableableService.EnableAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "反禁用成功"));
    }
}
