using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Authorization;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [RequirePermission("role:list")]
    public async Task<ActionResult<ApiResponse<PagedResult<RoleDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _roleService.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<RoleDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    [RequirePermission("role:list")]
    public async Task<ActionResult<ApiResponse<RoleDetailDto>>> GetById(string id)
    {
        var result = await _roleService.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<RoleDetailDto>.Fail("角色不存在", 404));
        return Ok(ApiResponse<RoleDetailDto>.Ok(result));
    }

    [HttpPost]
    [RequirePermission("role:add")]
    public async Task<ActionResult<ApiResponse<RoleDto>>> Create([FromBody] CreateRoleRequest request)
    {
        var result = await _roleService.CreateAsync(request);
        return Ok(ApiResponse<RoleDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    [RequirePermission("role:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateRoleRequest request)
    {
        var result = await _roleService.UpdateAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    [RequirePermission("role:delete")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
    {
        var result = await _roleService.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }

    [HttpGet("{id}/permissions")]
    [RequirePermission("role:list")]
    public async Task<ActionResult<ApiResponse<List<string>>>> GetPermissions(string id)
    {
        var result = await _roleService.GetPermissionsAsync(id);
        return Ok(ApiResponse<List<string>>.Ok(result));
    }

    [HttpPost("{id}/permissions")]
    [RequirePermission("role:assign")]
    public async Task<ActionResult<ApiResponse<bool>>> AssignPermissions(string id, [FromBody] AssignPermissionsRequest request)
    {
        var result = await _roleService.AssignPermissionsAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "权限分配成功"));
    }
}
