using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Services;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class UnitController : ApprovableDisableableMasterDataController<TBdUnit, UnitListDto, UnitDetailDto, CreateUnitRequest, UpdateUnitRequest>
{
    private readonly UnitService _unitService;

    public UnitController(IApprovableDisableableService<TBdUnit, UnitListDto, UnitDetailDto, CreateUnitRequest, UpdateUnitRequest> service)
        : base(service)
    {
        _unitService = (UnitService)service;
    }

    /// <summary>所属分组（单位组）下拉，含基准单位</summary>
    [HttpGet("unit-groups")]
    public async Task<ActionResult<ApiResponse<List<UnitGroupOptionDto>>>> UnitGroups([FromQuery] string? keyword)
        => Ok(ApiResponse<List<UnitGroupOptionDto>>.Ok(await _unitService.GetUnitGroupsAsync(keyword)));
}
