using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class SupplierListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FNote { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class SupplierDetailDto : SupplierListDto
{
}

public class CreateSupplierRequest
{
    [Required(ErrorMessage = "供应商代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "供应商名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateSupplierRequest
{
    [Required(ErrorMessage = "供应商名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}
