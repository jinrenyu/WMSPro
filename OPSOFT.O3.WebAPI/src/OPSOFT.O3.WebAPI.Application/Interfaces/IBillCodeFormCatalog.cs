using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 编码规则业务表单目录（数据驱动）：登记表 SYS_BILLCODEFORM 的读写。
/// 取代写死的 BillCodeFieldRegistry.Forms——新单据在此登记即出现在配置页、可被基类反射取号匹配。
/// 字段段白名单仍由代码侧 BillCodeFieldRegistry 提供（字段值须由单据 Service 建单时喂入上下文）。
/// </summary>
public interface IBillCodeFormCatalog
{
    /// <summary>全部已登记的可配置业务表单（按友好名排序）</summary>
    Task<List<BillCodeFormDto>> GetFormsAsync();

    /// <summary>可登记单据花名册（反射系统已注册的 DocumentService 子单据 + 各自登记状态），供"登记新单据"下拉选择</summary>
    Task<List<BillCodeDocumentDto>> GetAvailableDocumentsAsync();

    /// <summary>单个表单（未登记返回 null）</summary>
    Task<BillCodeFormDto?> GetFormAsync(string formKey);

    /// <summary>表单友好名（未登记返回 null）</summary>
    Task<string?> GetFormNameAsync(string formKey);

    /// <summary>由单据表头实体类名解析 formKey（基类反射取号用；未登记/未填实体名返回 null）</summary>
    Task<string?> ResolveFormKeyByEntityAsync(string entityName);

    /// <summary>登记或更新一个业务表单（按 FormKey 幂等 Upsert）</summary>
    Task RegisterAsync(BillCodeFormRegisterRequest request);

    /// <summary>注销登记（移出目录；不动已配置的规则与流水数据）</summary>
    Task UnregisterAsync(string formKey);
}
