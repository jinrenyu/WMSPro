using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 即时库存（只读：真实表 T_STK_INVENTORY 分页查询）
/// </summary>
[ApiController]
[Route("api/inventory")]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<InventoryListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<InventoryListDto>>.Ok(result));
    }
}
