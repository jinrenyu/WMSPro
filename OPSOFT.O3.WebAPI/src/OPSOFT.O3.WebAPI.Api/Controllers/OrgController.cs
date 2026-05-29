using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrgController : ControllerBase
{
    private readonly IOrgService _orgService;

    public OrgController(IOrgService orgService)
    {
        _orgService = orgService;
    }

    /// <summary>获取当前用户可访问的组织列表</summary>
    [HttpGet("my")]
    public async Task<ActionResult<ApiResponse<List<MyOrgDto>>>> My()
        => Ok(ApiResponse<List<MyOrgDto>>.Ok(await _orgService.GetMyOrgsAsync()));
}
