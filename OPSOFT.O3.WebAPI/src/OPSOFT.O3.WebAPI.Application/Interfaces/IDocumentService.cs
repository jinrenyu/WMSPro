using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 通用单据服务接口（主表 + 明细表）
/// </summary>
public interface IDocumentService<THeader, TEntry, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where THeader : BaseEntity, new()
    where TEntry : BaseEntity, new()
{
    Task<PagedResult<TListDto>> GetPagedListAsync(PagedRequest request);
    Task<TDetailDto?> GetByIdAsync(string uid);
    Task<TDetailDto> CreateAsync(TCreateDto request);
    Task<bool> UpdateAsync(string uid, TUpdateDto request);
    Task<bool> DeleteAsync(string uid);
    Task<bool> ApproveAsync(string uid);
    Task<bool> RejectAsync(string uid, string? reason = null);
    Task<bool> CloseAsync(string uid);
}
