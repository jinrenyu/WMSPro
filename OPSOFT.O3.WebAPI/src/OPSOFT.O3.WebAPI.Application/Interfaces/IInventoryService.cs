using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 即时库存服务（只读：真实表 T_STK_INVENTORY 分页查询 + 关联名称解析）。
/// 库存行由采购入库等过账写入，本服务仅提供列表查询，无新增/修改/删除。
/// </summary>
public interface IInventoryService
{
    /// <summary>分页查询即时库存（关键字按物料代码/名称/批次过滤；动态筛选作用于库存表真实列）</summary>
    Task<PagedResult<InventoryListDto>> GetPagedListAsync(PagedRequest request);
}
