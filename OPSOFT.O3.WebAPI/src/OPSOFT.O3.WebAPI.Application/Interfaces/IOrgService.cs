using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 组织服务
/// </summary>
public interface IOrgService
{
    /// <summary>获取当前用户可访问的组织列表（含名称、默认标记）</summary>
    Task<List<MyOrgDto>> GetMyOrgsAsync();
}
