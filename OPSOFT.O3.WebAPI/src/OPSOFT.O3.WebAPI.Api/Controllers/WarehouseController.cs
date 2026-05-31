using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Services;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class WarehouseController : ApprovableDisableableMasterDataController<TBdStock, WarehouseListDto, WarehouseDetailDto, CreateWarehouseRequest, UpdateWarehouseRequest>
{
    private readonly WarehouseService _warehouseService;

    public WarehouseController(IApprovableDisableableService<TBdStock, WarehouseListDto, WarehouseDetailDto, CreateWarehouseRequest, UpdateWarehouseRequest> service)
        : base(service)
    {
        _warehouseService = (WarehouseService)service;
    }

    // ---- 仓位信息（仓位集 + 仓位集值，启用仓位管理时维护）----

    [HttpGet("{id}/flex")]
    public async Task<ActionResult<ApiResponse<List<WarehouseFlexItemDto>>>> GetFlex(string id)
        => Ok(ApiResponse<List<WarehouseFlexItemDto>>.Ok(await _warehouseService.GetFlexAsync(id)));

    [HttpPut("{id}/flex")]
    public async Task<ActionResult<ApiResponse<bool>>> SaveFlex(string id, [FromBody] SaveWarehouseFlexRequest request)
    {
        await _warehouseService.SaveFlexAsync(id, request.Items);
        return Ok(ApiResponse<bool>.Ok(true, "保存成功"));
    }

    // ---- 仓位集 / 仓位集值 下拉 ----

    [HttpGet("flex-sets/lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> FlexSetLookup([FromQuery] string? keyword)
        => Ok(ApiResponse<List<LookupDto>>.Ok(await _warehouseService.GetFlexSetLookupAsync(keyword)));

    [HttpGet("flex-set-values/lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> FlexSetValueLookup([FromQuery] string? parentId, [FromQuery] string? keyword)
        => Ok(ApiResponse<List<LookupDto>>.Ok(await _warehouseService.GetFlexSetValueLookupAsync(parentId ?? string.Empty, keyword)));
}
