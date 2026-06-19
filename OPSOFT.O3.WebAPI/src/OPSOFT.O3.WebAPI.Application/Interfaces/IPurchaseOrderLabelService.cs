using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 采购标签打印（采购订单）服务：列表(已审非禁用采购订单按行展开) + 条码生成 + 作废/反作废 + 条码明细查询
/// </summary>
public interface IPurchaseOrderLabelService
{
    /// <summary>列表：已审核且非禁用的采购订单按明细行展开</summary>
    Task<PagedResult<PurchaseOrderLabelListDto>> GetPagedListAsync(PagedRequest request);

    /// <summary>条码生成页单据头（来源采购订单 + 选中明细行）</summary>
    Task<PurchaseOrderLabelHeadDto?> GetGenerateHeadAsync(string entryUid);

    /// <summary>查询某采购订单明细已生成的条码明细</summary>
    Task<List<BarcodeLineDto>> GetBarcodesAsync(string poEntryUid);

    /// <summary>生成条码（写入 T_BD_BARCODERS 主档 + T_BD_BARCODERS1 源单档），返回本次生成的条码明细</summary>
    Task<List<BarcodeLineDto>> GenerateAsync(GenerateBarcodeRequest request);

    /// <summary>作废条码（初始 → 作废）</summary>
    Task<int> VoidAsync(List<string> barcodes);

    /// <summary>反作废条码（作废 → 初始）</summary>
    Task<int> UnvoidAsync(List<string> barcodes);

    /// <summary>打印：记录打印日期（FDATE）</summary>
    Task<int> MarkPrintedAsync(List<string> barcodes);
}
