using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class MaterialController : ApprovableDisableableMasterDataController<TBdMaterial, MaterialListDto, MaterialDetailDto, CreateMaterialRequest, UpdateMaterialRequest>
{
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService service)
        : base(service)
    {
        _materialService = service;
    }

    // ---- 物料维度（辅助属性配置）----

    [HttpGet("{id}/dimensions")]
    public async Task<ActionResult<ApiResponse<List<MaterialDimensionDto>>>> GetDimensions(string id)
        => Ok(ApiResponse<List<MaterialDimensionDto>>.Ok(await _materialService.GetDimensionsAsync(id)));

    [HttpPut("{id}/dimensions")]
    public async Task<ActionResult<ApiResponse<bool>>> SaveDimensions(string id, [FromBody] SaveMaterialDimensionsRequest request)
    {
        await _materialService.SaveDimensionsAsync(id, request.Items);
        return Ok(ApiResponse<bool>.Ok(true, "保存成功"));
    }

    // ---- 辅助单位换算 ----

    [HttpGet("{id}/sec-units")]
    public async Task<ActionResult<ApiResponse<List<MaterialSecUnitDto>>>> GetSecUnits(string id)
        => Ok(ApiResponse<List<MaterialSecUnitDto>>.Ok(await _materialService.GetSecUnitsAsync(id)));

    [HttpPut("{id}/sec-units")]
    public async Task<ActionResult<ApiResponse<bool>>> SaveSecUnits(string id, [FromBody] SaveMaterialSecUnitsRequest request)
    {
        await _materialService.SaveSecUnitsAsync(id, request.Items);
        return Ok(ApiResponse<bool>.Ok(true, "保存成功"));
    }

    // ---- 辅助属性下拉 ----

    [HttpGet("aux-properties/lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> AuxPropertyLookup([FromQuery] string? keyword)
        => Ok(ApiResponse<List<LookupDto>>.Ok(await _materialService.GetAuxPropertyLookupAsync(keyword)));

    // ---- 物料是否启用辅助属性（订单明细据此决定“辅助属性”是否可选/只读）----
    [HttpGet("{id}/aux-enabled")]
    public async Task<ActionResult<ApiResponse<bool>>> AuxEnabled(string id)
        => Ok(ApiResponse<bool>.Ok(await _materialService.IsAuxEnabledAsync(id)));

    [HttpGet("material-types/lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> MaterialTypeLookup([FromQuery] string? keyword)
        => Ok(ApiResponse<List<LookupDto>>.Ok(await _materialService.GetMaterialTypeLookupAsync(keyword)));
}
