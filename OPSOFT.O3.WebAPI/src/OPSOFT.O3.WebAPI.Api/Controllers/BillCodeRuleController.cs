using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Authorization;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 编码规则配置（系统管理→编码规则）：按业务表单维护单据编号规则（SYS_LISTCODE/ENTRY）
/// 与条码编号规则（SYS_BARCODE/ENTRY）
/// </summary>
[ApiController]
[Route("api/billcoderule")]
[Authorize]
public class BillCodeRuleController : ControllerBase
{
    private readonly IBillCodeRuleService _service;

    public BillCodeRuleController(IBillCodeRuleService service)
    {
        _service = service;
    }

    /// <summary>已注册业务表单 + 两套规则配置概览</summary>
    [HttpGet]
    [RequirePermission("billcoderule:list")]
    public async Task<ActionResult<ApiResponse<List<BillCodeRuleListDto>>>> GetList()
    {
        var result = await _service.GetListAsync();
        return Ok(ApiResponse<List<BillCodeRuleListDto>>.Ok(result));
    }

    /// <summary>可登记单据花名册（系统已注册单据 + 登记状态）：供"登记新单据"下拉选择，自动带出 formKey/实体类名</summary>
    [HttpGet("document-types")]
    [RequirePermission("billcoderule:list")]
    public async Task<ActionResult<ApiResponse<List<BillCodeDocumentDto>>>> GetDocumentTypes()
    {
        var result = await _service.GetAvailableDocumentsAsync();
        return Ok(ApiResponse<List<BillCodeDocumentDto>>.Ok(result));
    }

    /// <summary>登记/更新一个可配置业务表单（数据驱动目录；登记后即出现在配置页）</summary>
    [HttpPost("forms")]
    [RequirePermission("billcoderule:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> RegisterForm([FromBody] BillCodeFormRegisterRequest request)
    {
        await _service.RegisterFormAsync(request);
        return Ok(ApiResponse<bool>.Ok(true, "业务表单已登记"));
    }

    /// <summary>注销业务表单（移出配置页目录；不动已配置规则与流水）</summary>
    [HttpDelete("forms/{formKey}")]
    [RequirePermission("billcoderule:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> UnregisterForm(string formKey)
    {
        await _service.UnregisterFormAsync(formKey);
        return Ok(ApiResponse<bool>.Ok(true, "业务表单已注销"));
    }

    /// <summary>规则详情（含可用动态字段白名单）</summary>
    [HttpGet("{formKey}")]
    [RequirePermission("billcoderule:list")]
    public async Task<ActionResult<ApiResponse<BillCodeRuleDetailDto>>> GetDetail(string formKey)
    {
        var result = await _service.GetDetailAsync(formKey);
        if (result == null)
            return Ok(ApiResponse<BillCodeRuleDetailDto>.Fail("未注册的业务表单", 404));
        return Ok(ApiResponse<BillCodeRuleDetailDto>.Ok(result));
    }

    /// <summary>取号预览（单据编号 + 条码编号，不占号）</summary>
    [HttpGet("{formKey}/preview")]
    [RequirePermission("billcoderule:list")]
    public async Task<ActionResult<ApiResponse<BillCodeRulePreviewDto>>> Preview(string formKey)
    {
        var result = await _service.PreviewAsync(formKey);
        return Ok(ApiResponse<BillCodeRulePreviewDto>.Ok(result));
    }

    /// <summary>保存编码规则（单据编号 + 条码编号）</summary>
    [HttpPut("{formKey}")]
    [RequirePermission("billcoderule:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> Save(string formKey, [FromBody] SaveBillCodeRuleRequest request)
    {
        var result = await _service.SaveAsync(formKey, request);
        return Ok(ApiResponse<bool>.Ok(result, "编码规则已保存"));
    }

    /// <summary>清除该表单的编码规则配置（软删规则头 + 分段，不动流水计数）</summary>
    [HttpDelete("{formKey}")]
    [RequirePermission("billcoderule:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(string formKey)
    {
        var result = await _service.DeleteAsync(formKey);
        return Ok(ApiResponse<bool>.Ok(result, "编码规则已清除"));
    }

    /// <summary>重置该表单的编码流水（下次取号从起始号重新计，仅建议测试期使用）</summary>
    [HttpPost("{formKey}/reset-seq")]
    [RequirePermission("billcoderule:edit")]
    public async Task<ActionResult<ApiResponse<bool>>> ResetSeq(string formKey)
    {
        var result = await _service.ResetSeqAsync(formKey);
        return Ok(ApiResponse<bool>.Ok(result, "编码流水已重置"));
    }
}
