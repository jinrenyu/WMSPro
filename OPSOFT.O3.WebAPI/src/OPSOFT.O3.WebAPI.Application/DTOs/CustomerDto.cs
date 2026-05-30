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
    public string FGroupName { get; set; } = string.Empty;
}

public class CustomerDetailDto : CustomerListDto
{
    /// <summary>使用组织（FCompanyId 记录所选组织的上级/公司）</summary>
    public string FCompanyId { get; set; } = string.Empty;
    /// <summary>使用组织（公司）名称</summary>
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

public class CreateCustomerRequest
{
    [Required(ErrorMessage = "客户编码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "客户名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;   // 使用组织（新增时取当前切换组织）
    public string FGroupId { get; set; } = string.Empty;     // 客户分组
    public string FShortName { get; set; } = string.Empty;   // 简称
    public string FNameEn { get; set; } = string.Empty;      // 英文简称

    // ---- 基本 ----
    public string FProvincial { get; set; } = string.Empty;  // 省份
    public string FCity { get; set; } = string.Empty;        // 城市
    public string FZip { get; set; } = string.Empty;         // 邮政区号
    public string FAddress { get; set; } = string.Empty;     // 地址
    public string FAddressEn { get; set; } = string.Empty;   // 英文地址
    public string FContact { get; set; } = string.Empty;     // 联系人
    public string FPhone { get; set; } = string.Empty;       // 联系电话
    public string FFax { get; set; } = string.Empty;         // 传真
    public string FEmail { get; set; } = string.Empty;       // 邮箱
    public string FBank { get; set; } = string.Empty;        // 银行
    public string FAccount { get; set; } = string.Empty;     // 账户
    public string FEmpId { get; set; } = string.Empty;       // 业务员
    public string FLegalPerson { get; set; } = string.Empty; // 法人
    public string FWebSite { get; set; } = string.Empty;     // 公司网址
    public string FTaxRegisterCode { get; set; } = string.Empty; // 税号
    public string FTradingCurrId { get; set; } = string.Empty;   // 结算币别
}

public class UpdateCustomerRequest
{
    [Required(ErrorMessage = "客户名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FGroupId { get; set; } = string.Empty;
    public string FShortName { get; set; } = string.Empty;
    public string FNameEn { get; set; } = string.Empty;

    // ---- 基本 ----
    public string FProvincial { get; set; } = string.Empty;
    public string FCity { get; set; } = string.Empty;
    public string FZip { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FAddressEn { get; set; } = string.Empty;
    public string FContact { get; set; } = string.Empty;
    public string FPhone { get; set; } = string.Empty;
    public string FFax { get; set; } = string.Empty;
    public string FEmail { get; set; } = string.Empty;
    public string FBank { get; set; } = string.Empty;
    public string FAccount { get; set; } = string.Empty;
    public string FEmpId { get; set; } = string.Empty;
    public string FLegalPerson { get; set; } = string.Empty;
    public string FWebSite { get; set; } = string.Empty;
    public string FTaxRegisterCode { get; set; } = string.Empty;
    public string FTradingCurrId { get; set; } = string.Empty;
}
