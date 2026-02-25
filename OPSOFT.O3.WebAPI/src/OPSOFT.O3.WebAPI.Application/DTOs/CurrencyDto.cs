using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class CurrencyListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FCode { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public decimal FExchangeRate { get; set; }
    public int FPriceDigits { get; set; }
    public int FAmountDigits { get; set; }
    public DateTime? CYmd { get; set; }
    public string FDescription { get; set; } = string.Empty;
    public int FFixRate { get; set; }
    public string FUseOrgId { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class CurrencyDetailDto : CurrencyListDto
{
}

public class CreateCurrencyRequest
{
    [Required(ErrorMessage = "币别代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "货币代码不能为空")]
    public string FCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "币别名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public int FPriceDigits { get; set; } = 2;
    public int FAmountDigits { get; set; } = 2;
    public int FFixRate { get; set; }
    public decimal FExchangeRate { get; set; } = 1;
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateCurrencyRequest
{
    [Required(ErrorMessage = "币别名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCode { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public int FPriceDigits { get; set; } = 2;
    public int FAmountDigits { get; set; } = 2;
    public int FFixRate { get; set; }
    public decimal FExchangeRate { get; set; } = 1;
    public string FGroupId { get; set; } = string.Empty;
}
