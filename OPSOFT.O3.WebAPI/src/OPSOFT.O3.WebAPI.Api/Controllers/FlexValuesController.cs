using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Services;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 仓位（仓位集 T_BAS_FLEXVALUES）主数据维护
/// </summary>
[Route("api/[controller]")]
public class FlexValuesController : ApprovableDisableableMasterDataController<TBasFlexvalues, FlexValuesListDto, FlexValuesDetailDto, CreateFlexValuesRequest, UpdateFlexValuesRequest>
{
    private readonly FlexValuesService _flexValuesService;

    public FlexValuesController(IApprovableDisableableService<TBasFlexvalues, FlexValuesListDto, FlexValuesDetailDto, CreateFlexValuesRequest, UpdateFlexValuesRequest> service)
        : base(service)
    {
        _flexValuesService = (FlexValuesService)service;
    }

    // ---- 值列表（仓位集值）----

    [HttpGet("{id}/entries")]
    public async Task<ActionResult<ApiResponse<List<FlexValuesEntryDto>>>> GetEntries(string id)
        => Ok(ApiResponse<List<FlexValuesEntryDto>>.Ok(await _flexValuesService.GetEntriesAsync(id)));

    [HttpPut("{id}/entries")]
    public async Task<ActionResult<ApiResponse<bool>>> SaveEntries(string id, [FromBody] SaveFlexValuesEntriesRequest request)
    {
        await _flexValuesService.SaveEntriesAsync(id, request.Items);
        return Ok(ApiResponse<bool>.Ok(true, "保存成功"));
    }
}
