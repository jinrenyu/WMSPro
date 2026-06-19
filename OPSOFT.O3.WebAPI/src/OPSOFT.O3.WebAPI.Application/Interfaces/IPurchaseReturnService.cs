using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 采购退料单（外购退料）服务接口：在通用单据契约（一主+物料汇总明细）之上，
/// 追加"扫码解析"与"源单类型（数据驱动）"两个专有能力。审核=退料出库过账、反审核=冲回。
/// </summary>
public interface IPurchaseReturnService : IDocumentService<TPurMrb, TPurMrbEntry,
    PurchaseReturnListDto, PurchaseReturnDetailDto, CreatePurchaseReturnRequest, UpdatePurchaseReturnRequest>
{
    /// <summary>扫码解析：按条码查条码主档（仅"已入库"可退料）；箱码经装箱清单展开子条码，返回录入明细行(ENTRY1)+底阶条码(ENTRY2)</summary>
    Task<PurchaseReturnScanResultDto> ScanBarcodeAsync(ScanBarcodeRequest request);

    /// <summary>源单类型（数据驱动，T_BOS_SELBILL × SYS_BILLTEMPLATE，按目标单据 PUR_MRB 过滤启用项）</summary>
    Task<List<SourceBillTypeDto>> GetSourceBillTypesAsync();
}
