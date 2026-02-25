using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface IRoleService
{
    Task<PagedResult<RoleDto>> GetPagedListAsync(PagedRequest request);
    Task<RoleDetailDto?> GetByIdAsync(string uid);
    Task<RoleDto> CreateAsync(CreateRoleRequest request);
    Task<bool> UpdateAsync(string uid, UpdateRoleRequest request);
    Task<bool> DeleteAsync(string uid);
    Task<List<string>> GetPermissionsAsync(string roleId);
    Task<bool> AssignPermissionsAsync(string roleId, AssignPermissionsRequest request);
}
