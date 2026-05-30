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
    public string FGroupName { get; set; } = string.Empty;
}

public class CurrencyDetailDto : CurrencyListDto
{
    // ---- 使用组织 ----
    public string FCompanyId { get; set; } = string.Empty;
    public string FCompanyName { get; set; } = string.Empty;

    // ---- 审核 / 禁用 / 制单等系统信息（只读）----
    public string CUser { get; set; } = string.Empty;          // 制单人
    public string MUser { get; set; } = string.Empty;          // 修改人
    public DateTime? MYmd { get; set; }                        // 修改日期
    public string FCheckerId { get; set; } = string.Empty;     // 审核人
    public DateTime? FCheckDate { get; set; }                  // 审核日期
    public string Fdisableid { get; set; } = string.Empty;     // 禁用人
    public DateTime? Fdisabledate { get; set; }                // 禁用日期
}

public class CreateCurrencyRequest
{
    [Required(ErrorMessage = "币别代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "币别名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;   // 使用组织（新增时取当前切换组织）
    public string FCode { get; set; } = string.Empty;        // 货币代码
    public string FDescription { get; set; } = string.Empty; // 币别描述
    public int FPriceDigits { get; set; }                    // 单价精度
    public int FAmountDigits { get; set; }                   // 金额精度
    public int FFixRate { get; set; } = 1;                   // 折算方式 1=原币×汇率 2=原币÷汇率
    public decimal FExchangeRate { get; set; } = 1;          // 记账汇率
    public string FGroupId { get; set; } = string.Empty;     // 分组
}

public class UpdateCurrencyRequest
{
    [Required(ErrorMessage = "币别名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCode { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public int FPriceDigits { get; set; }
    public int FAmountDigits { get; set; }
    public int FFixRate { get; set; } = 1;
    public decimal FExchangeRate { get; set; } = 1;
    public string FGroupId { get; set; } = string.Empty;
}
