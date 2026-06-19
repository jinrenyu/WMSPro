using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 采购标签打印（收料通知单）服务：列表(已审非禁用收料通知单按行展开) + 条码生成 + 作废/反作废 + 条码明细查询。
/// 与 <see cref="IPurchaseOrderLabelService"/> 范式一致，区别在于源单为收料通知单（源单档写 FPUR* 并回填源采购订单 FPO*）。
/// </summary>
public interface IReceiveNoticeLabelService
{
    /// <summary>列表：已审核且非禁用的收料通知单按明细行展开</summary>
    Task<PagedResult<ReceiveNoticeLabelListDto>> GetPagedListAsync(PagedRequest request);

    /// <summary>条码生成页单据头（来源收料通知单 + 选中明细行）</summary>
    Task<ReceiveNoticeLabelHeadDto?> GetGenerateHeadAsync(string entryUid);

    /// <summary>查询某收料通知单明细已生成的条码明细</summary>
    Task<List<BarcodeLineDto>> GetBarcodesAsync(string receiveEntryUid);

    /// <summary>生成条码（写入 T_BD_BARCODERS 主档 + T_BD_BARCODERS1 源单档），返回本次生成的条码明细</summary>
    Task<List<BarcodeLineDto>> GenerateAsync(GenerateReceiveBarcodeRequest request);

    /// <summary>作废条码（初始 → 作废）</summary>
    Task<int> VoidAsync(List<string> barcodes);

    /// <summary>反作废条码（作废 → 初始）</summary>
    Task<int> UnvoidAsync(List<string> barcodes);

    /// <summary>打印：记录打印日期（FDATE）</summary>
    Task<int> MarkPrintedAsync(List<string> barcodes);
}
