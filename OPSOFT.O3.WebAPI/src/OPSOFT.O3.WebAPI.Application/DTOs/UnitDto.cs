using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class UnitListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FUnitGroupId { get; set; } = string.Empty;
    public bool FIsBaseUnit { get; set; }
    public int FPrecision { get; set; }
    public decimal FCoefficient { get; set; }
    public DateTime? CYmd { get; set; }
    public string FDescription { get; set; } = string.Empty;
    public string FRoundType { get; set; } = string.Empty;
    public string FConvertType { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class UnitDetailDto : UnitListDto
{
}

public class CreateUnitRequest
{
    [Required(ErrorMessage = "单位代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "单位名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public string FUnitGroupId { get; set; } = string.Empty;
    public bool FIsBaseUnit { get; set; }
    public int FPrecision { get; set; }
    public string FRoundType { get; set; } = string.Empty;
    public string FConvertType { get; set; } = string.Empty;
    public decimal FCoefficient { get; set; } = 1;
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateUnitRequest
{
    [Required(ErrorMessage = "单位名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public string FUnitGroupId { get; set; } = string.Empty;
    public bool FIsBaseUnit { get; set; }
    public int FPrecision { get; set; }
    public string FRoundType { get; set; } = string.Empty;
    public string FConvertType { get; set; } = string.Empty;
    public decimal FCoefficient { get; set; } = 1;
    public string FGroupId { get; set; } = string.Empty;
}
