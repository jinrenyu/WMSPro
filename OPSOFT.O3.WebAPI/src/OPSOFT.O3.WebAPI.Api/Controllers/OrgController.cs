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

    /// <summary>组织下拉（货主/保管者等选择场景，与基础资料 lookup 同形：{uid,fNumber,fName}）</summary>
    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
        => Ok(ApiResponse<List<LookupDto>>.Ok(await _orgService.GetLookupAsync(request)));
}
