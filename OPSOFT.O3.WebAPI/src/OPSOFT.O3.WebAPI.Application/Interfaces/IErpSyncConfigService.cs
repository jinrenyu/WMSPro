using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// ERP 集成配置服务（一主两从：T_SYN_INFO / T_SYN_INFOENTRY / T_SYN_INFOENTRY1）。
/// 继承通用单据契约（CRUD + 审核），并追加：手动同步、目标目录（反射）、功能代码候选。
/// </summary>
public interface IErpSyncConfigService : IDocumentService<TSynInfo, TSynInfoentry,
    ErpSyncConfigListDto, ErpSyncConfigDetailDto, CreateErpSyncConfigRequest, UpdateErpSyncConfigRequest>
{
    /// <summary>手动执行一次同步（按配置从 ERP 取数并 upsert 到目标表）</summary>
    Task<ErpSyncRunResultDto> RunSyncAsync(string uid);

    /// <summary>目标实体目录（反射 Domain 所有 [SugarTable] 实体）</summary>
    Task<List<ErpTargetTableDto>> GetTargetTablesAsync(string? keyword = null);

    /// <summary>指定目标表的字段目录（反射实体 [SugarColumn]）</summary>
    Task<List<ErpTargetFieldDto>> GetTargetFieldsAsync(string tableName);

    /// <summary>功能代码 / ERP表单ID 候选（SYS_BILLTEMPLATE）</summary>
    Task<List<ErpBillTemplateDto>> GetBillTemplatesAsync(string? keyword = null);
}
