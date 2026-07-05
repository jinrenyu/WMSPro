using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 生产订单「下达 / 反下达」服务。
/// 下达：按选中明细行的 BOM 生成对应的「生产用料清单」(T_PRD_PPBOM/ENTRY，直接审核态)，并把明细业务状态置为下达(3)。
/// 反下达：删除该明细对应的用料清单，把明细业务状态退回计划确认(2)。
/// 对照旧系统 ProductiveTask.CreatePPBom / ReCommit 实现。
/// </summary>
public interface IProductionReleaseService
{
    /// <summary>下达：生成用料清单。moUid=生产订单主表 Uid，entryUids=明细行 Uid 集合。</summary>
    Task<ProductionReleaseResultDto> ReleaseAsync(string moUid, List<string> entryUids);

    /// <summary>反下达：删除用料清单并退回明细业务状态。</summary>
    Task<ProductionReleaseResultDto> UnreleaseAsync(string moUid, List<string> entryUids);
}
