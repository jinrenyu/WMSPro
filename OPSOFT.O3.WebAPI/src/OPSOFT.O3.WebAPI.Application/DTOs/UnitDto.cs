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
    // 所属分组（单位组 T_BD_UNITGROUP，JOIN by FUnitGroupId == FInterId）
    public string FUnitGroupNumber { get; set; } = string.Empty;
    public string FUnitGroupName { get; set; } = string.Empty;
    public string FBaseUnitNumber { get; set; } = string.Empty;
    public string FBaseUnitName { get; set; } = string.Empty;
    // 换算关系（T_BD_UNITCONVERTRATE，JOIN by FUnitId == 本单位 FInterId；无记录时回落 numerator=FCoefficient/denominator=1）
    public decimal FConvertNumerator { get; set; }
    public decimal FConvertDenominator { get; set; }
    // 使用组织 / 审计（只读）
    public string FCompanyId { get; set; } = string.Empty;
    public string FCompanyName { get; set; } = string.Empty;
    public string CUser { get; set; } = string.Empty;
    public string MUser { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string FCheckerId { get; set; } = string.Empty;
    public DateTime? FCheckDate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
}

/// <summary>单位组下拉选项（含基准单位，用于"所属分组"选择与"基准单位"显示）</summary>
public class UnitGroupOptionDto
{
    public string Uid { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FBaseUnitNumber { get; set; } = string.Empty;
    public string FBaseUnitName { get; set; } = string.Empty;
}

public class CreateUnitRequest
{
    [Required(ErrorMessage = "单位代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "单位名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public string FUnitGroupId { get; set; } = string.Empty;
    public bool FIsBaseUnit { get; set; }
    public int FPrecision { get; set; }
    public string FRoundType { get; set; } = string.Empty;
    public string FConvertType { get; set; } = string.Empty;
    public decimal FConvertNumerator { get; set; } = 1;
    public decimal FConvertDenominator { get; set; } = 1;
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
    public decimal FConvertNumerator { get; set; } = 1;
    public decimal FConvertDenominator { get; set; } = 1;
    public string FGroupId { get; set; } = string.Empty;
}
