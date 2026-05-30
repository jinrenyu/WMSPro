using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class SupplierListDto
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
    public string FNote { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
    public string FGroupName { get; set; } = string.Empty;
}

public class SupplierDetailDto : SupplierListDto
{
    // ---- 基本 ----
    public decimal? FTaxRate { get; set; }                     // 税率
    public string FCountry { get; set; } = string.Empty;       // 国家
    public string FProvincial { get; set; } = string.Empty;    // 地区
    public string FFax { get; set; } = string.Empty;           // 传真
    public string FEmail { get; set; } = string.Empty;         // 电子邮件
    public string FBank { get; set; } = string.Empty;          // 银行
    public string FAccount { get; set; } = string.Empty;       // 账户
    public string FEmpId { get; set; } = string.Empty;         // 业务员

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

public class CreateSupplierRequest
{
    [Required(ErrorMessage = "供应商代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "供应商名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;   // 使用组织（新增时取当前切换组织）
    public string FGroupId { get; set; } = string.Empty;     // 供应商分组
    public string FShortName { get; set; } = string.Empty;   // 供应商简称

    // ---- 基本 ----
    public decimal? FTaxRate { get; set; }                   // 税率
    public string FCountry { get; set; } = string.Empty;     // 国家
    public string FProvincial { get; set; } = string.Empty;  // 地区
    public string FAddress { get; set; } = string.Empty;     // 通讯地址
    public string FContact { get; set; } = string.Empty;     // 联系人
    public string FPhone { get; set; } = string.Empty;       // 联系电话
    public string FFax { get; set; } = string.Empty;         // 传真
    public string FEmail { get; set; } = string.Empty;       // 电子邮件
    public string FBank { get; set; } = string.Empty;        // 银行
    public string FAccount { get; set; } = string.Empty;     // 账户
    public string FEmpId { get; set; } = string.Empty;       // 业务员
}

public class UpdateSupplierRequest
{
    [Required(ErrorMessage = "供应商名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FGroupId { get; set; } = string.Empty;
    public string FShortName { get; set; } = string.Empty;

    // ---- 基本 ----
    public decimal? FTaxRate { get; set; }
    public string FCountry { get; set; } = string.Empty;
    public string FProvincial { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FFax { get; set; } = string.Empty;
    public string FEmail { get; set; } = string.Empty;
    public string FBank { get; set; } = string.Empty;
    public string FAccount { get; set; } = string.Empty;
    public string FEmpId { get; set; } = string.Empty;
}
