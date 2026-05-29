using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class MaterialListDto
{
    public string Uid { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    public string FErpClsId { get; set; } = string.Empty;
    public string FBaseUnitId { get; set; } = string.Empty;
    public string FBaseUnitName { get; set; } = string.Empty;
    public string FTypeId { get; set; } = string.Empty;
    public string FTypeName { get; set; } = string.Empty;
    public bool FIsBatchManage { get; set; }
    public DateTime? CYmd { get; set; }
    public string FMasterId { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public string FBarcode { get; set; } = string.Empty;
    public string FAbc { get; set; } = string.Empty;
    public decimal FMaxQty { get; set; }
    public decimal FSafeQty { get; set; }
    public decimal FNetWeight { get; set; }
    public decimal FGrossWeight { get; set; }
    public string FStoreUnitId { get; set; } = string.Empty;
    public string FStoreUnitName { get; set; } = string.Empty;
    public string FSaleUnitId { get; set; } = string.Empty;
    public string FSaleUnitName { get; set; } = string.Empty;
    public string FPurchaseUnitId { get; set; } = string.Empty;
    public string FPurchaseUnitName { get; set; } = string.Empty;
    public bool FIsKfPeriod { get; set; }
    public int FKfUnit { get; set; }
    public int FKfPeriod { get; set; }
    public decimal FMinPoQty { get; set; }
    public decimal FIncreaseQty { get; set; }
    public bool FCheckIncoming { get; set; }
    public string FOldNumber { get; set; } = string.Empty;
    public string FDeStockId { get; set; } = string.Empty;
    public string FDeStockName { get; set; } = string.Empty;
    public string FDeSpId { get; set; } = string.Empty;
    public string FDeSpName { get; set; } = string.Empty;
    public bool FVPart { get; set; }
    public string FGroupId { get; set; } = string.Empty;
    public string FGroupName { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FCheckerId { get; set; } = string.Empty;
    public DateTime FCheckDate { get; set; }
}

public class MaterialDetailDto : MaterialListDto
{
    // ---- 基本信息 ----
    public string FChartNumber { get; set; } = string.Empty;   // 图号
    public decimal? FTaxRate { get; set; }                      // 税率
    public string FCompanyId { get; set; } = string.Empty;      // 使用组织

    // ---- 包装与单位信息 ----
    public int? FBarType { get; set; }                          // 条码类型
    public string FSubConUnitId { get; set; } = string.Empty;   // 委外单位
    public string FProduceUnitId { get; set; } = string.Empty;  // 生产单位
    public string FAuxUnitId { get; set; } = string.Empty;      // 辅助单位

    // ---- 尺寸信息 ----
    public decimal? FLength { get; set; }
    public decimal? FWidth { get; set; }
    public decimal? FHeight { get; set; }
    public decimal? FVolume { get; set; }

    // ---- 库存控制信息 ----
    public decimal? FLowLimit { get; set; }                     // 最低库存量
    public bool? FFeedSn { get; set; }                          // 启用序列号
    public bool? FOtherSn { get; set; }                         // 第三方序列号
    public bool? FIsSecUnit { get; set; }                       // 启用辅助单位
    public bool? FIsFeed { get; set; }                          // 不投料

    // ---- 其他信息 ----
    public bool? FIsPinal { get; set; }                         // 终检
    public bool? FSuite { get; set; }                           // 套件
    public decimal? FPrice { get; set; }                        // 单价

    // ---- 审核 / 禁用 / 制单等系统信息（只读） ----
    public string CUser { get; set; } = string.Empty;           // 制单人
    public string MUser { get; set; } = string.Empty;           // 修改人
    public DateTime? MYmd { get; set; }                         // 修改日期
    public string Fdisableid { get; set; } = string.Empty;      // 禁用人
    public DateTime? Fdisabledate { get; set; }                 // 禁用日期

    // ---- 使用组织 ----
    public string FCompanyName { get; set; } = string.Empty;

    // ---- 图片（Base64）----
    public string? FPictureBase64 { get; set; }
}

public class CreateMaterialRequest
{
    [Required(ErrorMessage = "物料编码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "物料名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;   // 使用组织（新增时取当前切换组织）

    public string FSpecification { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public string FBarcode { get; set; } = string.Empty;
    public string FErpClsId { get; set; } = string.Empty;
    public string FAbc { get; set; } = string.Empty;
    public decimal FMaxQty { get; set; }
    public decimal FSafeQty { get; set; }
    public decimal FNetWeight { get; set; }
    public decimal FGrossWeight { get; set; }
    public string FBaseUnitId { get; set; } = string.Empty;
    public string FStoreUnitId { get; set; } = string.Empty;
    public string FSaleUnitId { get; set; } = string.Empty;
    public string FPurchaseUnitId { get; set; } = string.Empty;
    public bool FIsKfPeriod { get; set; }
    public int FKfUnit { get; set; }
    public int FKfPeriod { get; set; }
    public bool FIsBatchManage { get; set; }
    public decimal FMinPoQty { get; set; }
    public decimal FIncreaseQty { get; set; }
    public bool FCheckIncoming { get; set; }
    public string FOldNumber { get; set; } = string.Empty;
    public string FDeStockId { get; set; } = string.Empty;
    public string FDeSpId { get; set; } = string.Empty;
    public bool FVPart { get; set; }
    public string FTypeId { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;

    // ---- 扩展字段 ----
    public string FChartNumber { get; set; } = string.Empty;
    public decimal FTaxRate { get; set; }
    public int FBarType { get; set; }
    public string FSubConUnitId { get; set; } = string.Empty;
    public string FProduceUnitId { get; set; } = string.Empty;
    public string FAuxUnitId { get; set; } = string.Empty;
    public decimal FLength { get; set; }
    public decimal FWidth { get; set; }
    public decimal FHeight { get; set; }
    public decimal FVolume { get; set; }
    public decimal FLowLimit { get; set; }
    public bool FFeedSn { get; set; }
    public bool FOtherSn { get; set; }
    public bool FIsSecUnit { get; set; }
    public bool FIsFeed { get; set; }
    public bool FIsPinal { get; set; }
    public bool FSuite { get; set; }
    public decimal FPrice { get; set; }
    public string? FPictureBase64 { get; set; }
}

public class UpdateMaterialRequest
{
    [Required(ErrorMessage = "物料名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FSpecification { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public string FBarcode { get; set; } = string.Empty;
    public string FErpClsId { get; set; } = string.Empty;
    public string FAbc { get; set; } = string.Empty;
    public decimal FMaxQty { get; set; }
    public decimal FSafeQty { get; set; }
    public decimal FNetWeight { get; set; }
    public decimal FGrossWeight { get; set; }
    public string FBaseUnitId { get; set; } = string.Empty;
    public string FStoreUnitId { get; set; } = string.Empty;
    public string FSaleUnitId { get; set; } = string.Empty;
    public string FPurchaseUnitId { get; set; } = string.Empty;
    public bool FIsKfPeriod { get; set; }
    public int FKfUnit { get; set; }
    public int FKfPeriod { get; set; }
    public bool FIsBatchManage { get; set; }
    public decimal FMinPoQty { get; set; }
    public decimal FIncreaseQty { get; set; }
    public bool FCheckIncoming { get; set; }
    public string FOldNumber { get; set; } = string.Empty;
    public string FDeStockId { get; set; } = string.Empty;
    public string FDeSpId { get; set; } = string.Empty;
    public bool FVPart { get; set; }
    public string FTypeId { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;

    // ---- 扩展字段 ----
    public string FChartNumber { get; set; } = string.Empty;
    public decimal FTaxRate { get; set; }
    public int FBarType { get; set; }
    public string FSubConUnitId { get; set; } = string.Empty;
    public string FProduceUnitId { get; set; } = string.Empty;
    public string FAuxUnitId { get; set; } = string.Empty;
    public decimal FLength { get; set; }
    public decimal FWidth { get; set; }
    public decimal FHeight { get; set; }
    public decimal FVolume { get; set; }
    public decimal FLowLimit { get; set; }
    public bool FFeedSn { get; set; }
    public bool FOtherSn { get; set; }
    public bool FIsSecUnit { get; set; }
    public bool FIsFeed { get; set; }
    public bool FIsPinal { get; set; }
    public bool FSuite { get; set; }
    public decimal FPrice { get; set; }
    public string? FPictureBase64 { get; set; }
}
