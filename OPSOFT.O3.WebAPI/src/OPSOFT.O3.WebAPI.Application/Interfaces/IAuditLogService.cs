using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 审计追踪日志服务
/// </summary>
public interface IAuditLogService
{
    /// <summary>
    /// 记录字段级变更
    /// </summary>
    Task LogChangeAsync(string prgKey, string targetUid, string? billNo,
        string description, List<AuditChangeItem> changes);

    /// <summary>
    /// 查询某条记录的变更历史
    /// </summary>
    Task<List<AuditLogDetailDto>> GetHistoryAsync(string targetUid);

    /// <summary>
    /// 分页查询审计日志
    /// </summary>
    Task<PagedResult<AuditLogListDto>> GetPagedListAsync(AuditLogQueryRequest request);
}
