using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface IUserService
{
    Task<PagedResult<UserListDto>> GetPagedListAsync(PagedRequest request);
    Task<UserDetailDto?> GetByIdAsync(string uid);
    Task<UserDetailDto?> GetCurrentUserAsync(string userId);
    Task<UserDetailDto> CreateAsync(CreateUserRequest request);
    Task<bool> UpdateAsync(string uid, UpdateUserRequest request);
    Task<bool> DeleteAsync(string uid);
    Task<bool> ChangePasswordAsync(string uid, ChangePasswordRequest request);
    Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    Task<bool> AssignRolesAsync(string uid, AssignRolesRequest request);
    Task<bool> UnlockAsync(string uid);
    Task<bool> ToggleStatusAsync(string uid);
}
