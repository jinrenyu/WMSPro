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
}

public class CreateMaterialRequest
{
    [Required(ErrorMessage = "物料编码不能为空")]
    public string FNumber { get; set; } = string.Empty;

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
}
