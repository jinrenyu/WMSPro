using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Authorization;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUser;

    public UserController(IUserService userService, ICurrentUserService currentUser)
    {
        _userService = userService;
        _currentUser = currentUser;
    }

    [HttpGet]
    [RequirePermission("user:list")]
    public async Task<ActionResult<ApiResponse<PagedResult<UserListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _userService.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<UserListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    [RequirePermission("user:list")]
    public async Task<ActionResult<ApiResponse<UserDetailDto>>> GetById(string id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<UserDetailDto>.Fail("用户不存在", 404));
        return Ok(ApiResponse<UserDetailDto>.Ok(result));
    }

    [HttpGet("me")]
    public async Task<ActionResult<ApiResponse<UserDetailDto>>> GetCurrentUser()
    {
        var userId = _currentUser.UserId;
        if (string.IsNullOrEmpty(userId))
            return Ok(ApiResponse<UserDetailDto>.Fail("未登录", 401));

        var result = await _userService.GetCurrentUserAsync(userId);
        if (result == null)
            return Ok(ApiResponse<UserDetailDto>.Fail("用户不存在", 404));
        return Ok(ApiResponse<UserDetailDto>.Ok(result));
    }

    [HttpPost]
    [RequirePermission("user:add")]
    public async Task<ActionResult<ApiResponse<UserDetailDto>>> Create([FromBody] CreateUserRequest request)
    {
        var result = await _userService.CreateAsync(request);
        return Ok(ApiResponse<UserDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    [RequirePermission("user:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] UpdateUserRequest request)
    {
        var result = await _userService.UpdateAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    [RequirePermission("user:delete")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
    {
        var result = await _userService.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }

    [HttpPost("{id}/change-password")]
    public async Task<ActionResult<ApiResponse<bool>>> ChangePassword(string id, [FromBody] ChangePasswordRequest request)
    {
        // 只允许本人修改密码
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return Ok(ApiResponse<bool>.Fail("用户不存在", 404));
        if (user.UserId != _currentUser.UserId)
            return Ok(ApiResponse<bool>.Fail("只能修改自己的密码", 403));

        var result = await _userService.ChangePasswordAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "密码修改成功"));
    }

    [HttpPost("reset-password")]
    [RequirePermission("user:reset-pwd")]
    public async Task<ActionResult<ApiResponse<bool>>> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _userService.ResetPasswordAsync(request);
        return Ok(ApiResponse<bool>.Ok(result, "密码重置成功"));
    }

    [HttpPost("{id}/assign-roles")]
    [RequirePermission("user:assign")]
    public async Task<ActionResult<ApiResponse<bool>>> AssignRoles(string id, [FromBody] AssignRolesRequest request)
    {
        var result = await _userService.AssignRolesAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "角色分配成功"));
    }

    [HttpPost("{id}/unlock")]
    [RequirePermission("user:unlock")]
    public async Task<ActionResult<ApiResponse<bool>>> Unlock(string id)
    {
        var result = await _userService.UnlockAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "解锁成功"));
    }

    [HttpPost("{id}/toggle-status")]
    [RequirePermission("user:toggle-status")]
    public async Task<ActionResult<ApiResponse<bool>>> ToggleStatus(string id)
    {
        var result = await _userService.ToggleStatusAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "状态切换成功"));
    }
}
