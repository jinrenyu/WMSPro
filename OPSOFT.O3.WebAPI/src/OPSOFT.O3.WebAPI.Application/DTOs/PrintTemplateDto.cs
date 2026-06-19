using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class PrintTemplateListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FBillSource { get; set; } = string.Empty;       // 适用单据来源（表单键）
    public string FBillSourceName { get; set; } = string.Empty;   // 适用单据中文名（服务端解析）
    public decimal FPaperWidth { get; set; }
    public decimal FPaperHeight { get; set; }
    public bool FIsDefault { get; set; }
    public string FDescription { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
}

public class PrintTemplateDetailDto : PrintTemplateListDto
{
    public string FTemplate { get; set; } = string.Empty;         // 模板 JSON（hiprint）

    // ---- 系统信息（只读）----
    public string FCompanyId { get; set; } = string.Empty;
    public string CUser { get; set; } = string.Empty;
    public string MUser { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
}

public class CreatePrintTemplateRequest
{
    [Required(ErrorMessage = "模板编码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "模板名称不能为空")]
    public string FName { get; set; } = string.Empty;

    [Required(ErrorMessage = "适用单据不能为空")]
    public string FBillSource { get; set; } = string.Empty;       // PUR_PurchaseOrder / PUR_ReceiveBill

    public string FTemplate { get; set; } = string.Empty;         // 模板 JSON
    public decimal FPaperWidth { get; set; }
    public decimal FPaperHeight { get; set; }
    public bool FIsDefault { get; set; }
    public string FDescription { get; set; } = string.Empty;
}

public class UpdatePrintTemplateRequest
{
    [Required(ErrorMessage = "模板名称不能为空")]
    public string FName { get; set; } = string.Empty;

    [Required(ErrorMessage = "适用单据不能为空")]
    public string FBillSource { get; set; } = string.Empty;

    public string FTemplate { get; set; } = string.Empty;
    public decimal FPaperWidth { get; set; }
    public decimal FPaperHeight { get; set; }
    public bool FIsDefault { get; set; }
    public string FDescription { get; set; } = string.Empty;
}
