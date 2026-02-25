using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class StockPlaceListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FStockId { get; set; } = string.Empty;
    public string FStockName { get; set; } = string.Empty;
    public string FSpProperty { get; set; } = string.Empty;
    public bool FAllowMix { get; set; }
    public DateTime? CYmd { get; set; }
    public string FDescription { get; set; } = string.Empty;
    public decimal FMaxCapacity { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class StockPlaceDetailDto : StockPlaceListDto
{
}

public class CreateStockPlaceRequest
{
    [Required(ErrorMessage = "仓位代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "仓位名称不能为空")]
    public string FName { get; set; } = string.Empty;

    [Required(ErrorMessage = "所属仓库不能为空")]
    public string FStockId { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public string FSpProperty { get; set; } = string.Empty;
    public bool FAllowMix { get; set; }
    public decimal FMaxCapacity { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateStockPlaceRequest
{
    [Required(ErrorMessage = "仓位名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FStockId { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public string FSpProperty { get; set; } = string.Empty;
    public bool FAllowMix { get; set; }
    public decimal FMaxCapacity { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}
