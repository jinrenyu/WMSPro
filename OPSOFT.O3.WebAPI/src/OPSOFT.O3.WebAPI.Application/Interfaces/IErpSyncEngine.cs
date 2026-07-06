using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 配置驱动的通用同步引擎：按一个同步任务（T_SYN_INFO + 实体信息 + 字段映射）
/// 从 ERP 取数并动态 upsert 到目标表。手动同步与定时同步共用。
/// </summary>
public interface IErpSyncEngine
{
    /// <summary>执行一个同步任务（按配置 uid）</summary>
    Task<ErpSyncRunResultDto> RunAsync(string configUid);
}
