using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 编码规则配置维护（系统管理→编码规则）：按业务表单一行，
/// 单据编号规则存 SYS_LISTCODE/ENTRY，条码编号规则存 SYS_BARCODE/ENTRY。
/// </summary>
public interface IBillCodeRuleService
{
    /// <summary>已注册业务表单清单 + 两套规则配置概览</summary>
    Task<List<BillCodeRuleListDto>> GetListAsync();

    /// <summary>可登记单据花名册（反射系统已注册单据 + 登记状态），供"登记新单据"下拉选择</summary>
    Task<List<BillCodeDocumentDto>> GetAvailableDocumentsAsync();

    /// <summary>登记/更新一个可配置业务表单（数据驱动目录；按 FormKey 幂等）</summary>
    Task RegisterFormAsync(BillCodeFormRegisterRequest request);

    /// <summary>注销业务表单（移出配置页目录；不动已配置规则与流水）</summary>
    Task UnregisterFormAsync(string formKey);

    /// <summary>规则详情（含可用动态字段白名单）；未注册的表单返回 null</summary>
    Task<BillCodeRuleDetailDto?> GetDetailAsync(string formKey);

    /// <summary>保存（规则头按表单 upsert，分段整删整插；Segments 为空 = 清空该规则）</summary>
    Task<bool> SaveAsync(string formKey, SaveBillCodeRuleRequest request);

    /// <summary>清除配置（软删单据/条码两套规则头 + 分段；不动流水计数）</summary>
    Task<bool> DeleteAsync(string formKey);

    /// <summary>重置流水（删除该表单的 SYS_LISTCODENO/SYS_BARCODENO 计数行，下次取号从起始号重新计）</summary>
    Task<bool> ResetSeqAsync(string formKey);

    /// <summary>取号预览（单据编号 + 条码编号，不占号）</summary>
    Task<BillCodeRulePreviewDto> PreviewAsync(string formKey);
}
