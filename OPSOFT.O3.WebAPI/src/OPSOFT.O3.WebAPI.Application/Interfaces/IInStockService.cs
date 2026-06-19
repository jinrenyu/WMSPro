using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 采购入库单服务接口：在通用单据契约（一主+物料汇总明细）之上，
/// 追加"扫码解析"与"源单类型（数据驱动）"两个入库单专有能力。
/// </summary>
public interface IInStockService : IDocumentService<TStkInstock, TStkInstockentry,
    InStockListDto, InStockDetailDto, CreateInStockRequest, UpdateInStockRequest>
{
    /// <summary>扫码解析：按条码查条码主档；箱码经装箱清单展开子条码，返回录入明细行(ENTRY1)+底阶条码(ENTRY2)</summary>
    Task<ScanBarcodeResultDto> ScanBarcodeAsync(ScanBarcodeRequest request);

    /// <summary>源单类型（数据驱动，T_BOS_SELBILL × SYS_BILLTEMPLATE，按目标单据 STK_InStock 过滤启用项）</summary>
    Task<List<SourceBillTypeDto>> GetSourceBillTypesAsync();
}
