using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 通用 CRUD 服务接口
/// </summary>
public interface ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, new()
{
    Task<PagedResult<TListDto>> GetPagedListAsync(PagedRequest request);
    Task<TDetailDto?> GetByIdAsync(string uid);
    Task<TDetailDto> CreateAsync(TCreateDto request);
    Task<bool> UpdateAsync(string uid, TUpdateDto request);
    Task<bool> DeleteAsync(string uid);
    Task<List<LookupDto>> GetLookupAsync(LookupRequest request);
}
