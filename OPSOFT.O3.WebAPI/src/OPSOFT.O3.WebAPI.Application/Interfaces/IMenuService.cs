using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface IMenuService
{
    Task<List<MenuTreeDto>> GetTreeAsync(bool activeOnly = false);
    Task<MenuTreeDto?> GetByIdAsync(string uid);
    Task<MenuTreeDto> CreateAsync(CreateMenuRequest request);
    Task<bool> UpdateAsync(string uid, UpdateMenuRequest request);
    Task<bool> DeleteAsync(string uid);
}
