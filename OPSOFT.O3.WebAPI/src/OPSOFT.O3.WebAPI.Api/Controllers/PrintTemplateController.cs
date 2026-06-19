using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Services;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class PrintTemplateController : MasterDataController<TBdPrintTemplate, PrintTemplateListDto, PrintTemplateDetailDto, CreatePrintTemplateRequest, UpdatePrintTemplateRequest>
{
    private readonly PrintTemplateService _printTemplateService;

    public PrintTemplateController(ICrudService<TBdPrintTemplate, PrintTemplateListDto, PrintTemplateDetailDto, CreatePrintTemplateRequest, UpdatePrintTemplateRequest> service)
        : base(service)
    {
        _printTemplateService = (PrintTemplateService)service;
    }

    /// <summary>
    /// 按适用单据来源取启用模板（含模板 JSON），默认模板在最前，供打印页选择
    /// </summary>
    [HttpGet("by-source/{billSource}")]
    public async Task<ActionResult<ApiResponse<List<PrintTemplateDetailDto>>>> GetBySource(string billSource)
    {
        var result = await _printTemplateService.GetBySourceAsync(billSource);
        return Ok(ApiResponse<List<PrintTemplateDetailDto>>.Ok(result));
    }
}
