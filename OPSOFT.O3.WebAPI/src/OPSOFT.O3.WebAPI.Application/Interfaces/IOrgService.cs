using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 组织服务
/// </summary>
public interface IOrgService
{
    /// <summary>获取当前用户可访问的组织列表（含名称、默认标记）</summary>
    Task<List<MyOrgDto>> GetMyOrgsAsync();

    /// <summary>组织下拉（供单据/明细选择货主、保管者等场景，按编码/名称模糊）</summary>
    Task<List<LookupDto>> GetLookupAsync(LookupRequest request);
}
