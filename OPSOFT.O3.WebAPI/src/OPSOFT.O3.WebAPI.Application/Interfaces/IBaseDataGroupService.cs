using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 基础资料分组服务接口
/// </summary>
public interface IBaseDataGroupService
{
    Task<PagedResult<BaseDataGroupListDto>> GetPagedListAsync(string prgKey, PagedRequest request);
    Task<BaseDataGroupDetailDto?> GetByIdAsync(string prgKey, string uid);
    Task<BaseDataGroupDetailDto> CreateAsync(string prgKey, CreateBaseDataGroupRequest request);
    Task<bool> UpdateAsync(string prgKey, string uid, UpdateBaseDataGroupRequest request);
    Task<bool> DeleteAsync(string prgKey, string uid);
    Task<List<LookupDto>> GetLookupAsync(string prgKey, LookupRequest request);
    Task<List<BaseDataGroupListDto>> GetTreeAsync(string prgKey);
}
