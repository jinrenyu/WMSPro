using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Authorization;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    /// <summary>
    /// 获取菜单树（菜单管理页面用，包含所有菜单）
    /// </summary>
    [HttpGet("tree")]
    [RequirePermission("menu:list")]
    public async Task<ActionResult<ApiResponse<List<MenuTreeDto>>>> GetTree()
    {
        var result = await _menuService.GetTreeAsync(activeOnly: false);
        return Ok(ApiResponse<List<MenuTreeDto>>.Ok(result));
    }

    /// <summary>
    /// 获取侧边栏菜单树（仅启用的菜单）
    /// </summary>
    [HttpGet("sidebar")]
    public async Task<ActionResult<ApiResponse<List<MenuTreeDto>>>> GetSidebar()
    {
        var result = await _menuService.GetTreeAsync(activeOnly: true);
        return Ok(ApiResponse<List<MenuTreeDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    [RequirePermission("menu:list")]
    public async Task<ActionResult<ApiResponse<MenuTreeDto>>> GetById(string id)
    {
        var result = await _menuService.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<MenuTreeDto>.Fail("菜单不存在", 404));
        return Ok(ApiResponse<MenuTreeDto>.Ok(result));
    }

    [HttpPost]
    [RequirePermission("menu:add")]
    public async Task<ActionResult<ApiResponse<MenuTreeDto>>> Create([FromBody] CreateMenuRequest request)
    {
        var result = await _menuService.CreateAsync(request);
        return Ok(ApiResponse<MenuTreeDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    [RequirePermission("menu:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateMenuRequest request)
    {
        var result = await _menuService.UpdateAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    [RequirePermission("menu:delete")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
    {
        var result = await _menuService.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }
}
