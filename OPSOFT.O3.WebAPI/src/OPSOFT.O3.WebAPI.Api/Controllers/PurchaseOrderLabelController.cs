using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 采购标签打印 - 采购订单（按已审非禁用采购订单行生成条码：T_BD_BARCODERS / T_BD_BARCODERS1）
/// </summary>
[ApiController]
[Route("api/purchaseorderlabel")]
[Authorize]
public class PurchaseOrderLabelController : ControllerBase
{
    private readonly IPurchaseOrderLabelService _service;

    public PurchaseOrderLabelController(IPurchaseOrderLabelService service)
    {
        _service = service;
    }

    /// <summary>列表：已审核且非禁用的采购订单按明细行展开</summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<PurchaseOrderLabelListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await _service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<PurchaseOrderLabelListDto>>.Ok(result));
    }

    /// <summary>条码生成页单据头（来源采购订单 + 选中明细行）</summary>
    [HttpGet("head/{entryUid}")]
    public async Task<ActionResult<ApiResponse<PurchaseOrderLabelHeadDto>>> GetHead(string entryUid)
    {
        var result = await _service.GetGenerateHeadAsync(entryUid);
        if (result == null)
            return Ok(ApiResponse<PurchaseOrderLabelHeadDto>.Fail("采购订单明细不存在", 404));
        return Ok(ApiResponse<PurchaseOrderLabelHeadDto>.Ok(result));
    }

    /// <summary>某采购订单明细已生成的条码明细</summary>
    [HttpGet("barcodes/{poEntryUid}")]
    public async Task<ActionResult<ApiResponse<List<BarcodeLineDto>>>> GetBarcodes(string poEntryUid)
    {
        var result = await _service.GetBarcodesAsync(poEntryUid);
        return Ok(ApiResponse<List<BarcodeLineDto>>.Ok(result));
    }

    /// <summary>生成条码</summary>
    [HttpPost("generate")]
    public async Task<ActionResult<ApiResponse<List<BarcodeLineDto>>>> Generate([FromBody] GenerateBarcodeRequest request)
    {
        var result = await _service.GenerateAsync(request);
        return Ok(ApiResponse<List<BarcodeLineDto>>.Ok(result, $"已生成 {result.Count} 个条码"));
    }

    /// <summary>作废条码</summary>
    [HttpPost("void")]
    public async Task<ActionResult<ApiResponse<int>>> Void([FromBody] BarcodeActionRequest request)
    {
        var n = await _service.VoidAsync(request?.Barcodes ?? new());
        return Ok(ApiResponse<int>.Ok(n, $"已作废 {n} 个条码"));
    }

    /// <summary>反作废条码</summary>
    [HttpPost("unvoid")]
    public async Task<ActionResult<ApiResponse<int>>> Unvoid([FromBody] BarcodeActionRequest request)
    {
        var n = await _service.UnvoidAsync(request?.Barcodes ?? new());
        return Ok(ApiResponse<int>.Ok(n, $"已反作废 {n} 个条码"));
    }

    /// <summary>打印：记录打印日期</summary>
    [HttpPost("print")]
    public async Task<ActionResult<ApiResponse<int>>> Print([FromBody] BarcodeActionRequest request)
    {
        var n = await _service.MarkPrintedAsync(request?.Barcodes ?? new());
        return Ok(ApiResponse<int>.Ok(n, $"已打印 {n} 个条码"));
    }
}
