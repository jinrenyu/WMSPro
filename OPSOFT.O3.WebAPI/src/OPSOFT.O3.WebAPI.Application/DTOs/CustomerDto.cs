using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class CustomerListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FShortName { get; set; } = string.Empty;
    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FSeller { get; set; } = string.Empty;
    public string FSalDeptId { get; set; } = string.Empty;
    public string FTradingCurrId { get; set; } = string.Empty;
    public string FCountry { get; set; } = string.Empty;
    public string FProvincial { get; set; } = string.Empty;
    public string FCity { get; set; } = string.Empty;
    public string FZip { get; set; } = string.Empty;
    public string FWebSite { get; set; } = string.Empty;
    public string FTel { get; set; } = string.Empty;
    public string FFax { get; set; } = string.Empty;
    public string FEmail { get; set; } = string.Empty;
    public string FBank { get; set; } = string.Empty;
    public string FAccount { get; set; } = string.Empty;
    public string FLegalPerson { get; set; } = string.Empty;
    public string FTaxRegisterCode { get; set; } = string.Empty;
    public string FNameEn { get; set; } = string.Empty;
    public string FAddressEn { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FEmpId { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class CustomerDetailDto : CustomerListDto
{
}

public class CreateCustomerRequest
{
    [Required(ErrorMessage = "客户编码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "客户名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FShortName { get; set; } = string.Empty;
    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FSeller { get; set; } = string.Empty;
    public string FTradingCurrId { get; set; } = string.Empty;
    public string FCountry { get; set; } = string.Empty;
    public string FTel { get; set; } = string.Empty;
    public string FEmail { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateCustomerRequest
{
    [Required(ErrorMessage = "客户名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FShortName { get; set; } = string.Empty;
    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FSeller { get; set; } = string.Empty;
    public string FTradingCurrId { get; set; } = string.Empty;
    public string FCountry { get; set; } = string.Empty;
    public string FTel { get; set; } = string.Empty;
    public string FEmail { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}
