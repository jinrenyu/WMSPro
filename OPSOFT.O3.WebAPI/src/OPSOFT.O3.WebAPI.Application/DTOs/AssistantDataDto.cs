using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ═══════════════════════════════════════════════════════
// 辅助资料类别 (T_BAS_ASSISTANTDATA)
// ═══════════════════════════════════════════════════════

public class AssistantDataListDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fnumber { get; set; } = string.Empty;
    public string Fname { get; set; } = string.Empty;
    public string Fdescription { get; set; } = string.Empty;
    public string Fparentid { get; set; } = string.Empty;
    public string Ftopclassid { get; set; } = string.Empty;
    public bool Isdefault { get; set; }
    public bool Fiso3use { get; set; }
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public DateTime? CYmd { get; set; }
}

public class AssistantDataDetailDto : AssistantDataListDto
{
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class CreateAssistantDataRequest
{
    [Required(ErrorMessage = "辅助资料类别代码不能为空")]
    public string Fnumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "辅助资料类别名称不能为空")]
    public string Fname { get; set; } = string.Empty;

    public string Fdescription { get; set; } = string.Empty;
    public string Fparentid { get; set; } = string.Empty;
    public string Ftopclassid { get; set; } = string.Empty;
    public bool Isdefault { get; set; }
    public bool Fiso3use { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateAssistantDataRequest
{
    [Required(ErrorMessage = "辅助资料类别名称不能为空")]
    public string Fname { get; set; } = string.Empty;

    public string Fdescription { get; set; } = string.Empty;
    public string Fparentid { get; set; } = string.Empty;
    public string Ftopclassid { get; set; } = string.Empty;
    public bool Isdefault { get; set; }
    public bool Fiso3use { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

// ═══════════════════════════════════════════════════════
// 辅助资料明细 (T_BAS_ASSISTANTDATAENTRY)
// ═══════════════════════════════════════════════════════

public class AssistantDataEntryListDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fnumber { get; set; } = string.Empty;
    public string Fdatavalue { get; set; } = string.Empty;
    public string Fid { get; set; } = string.Empty;
    public string Fparentid { get; set; } = string.Empty;
    public string Fdescription { get; set; } = string.Empty;
    public int Fseq { get; set; }
    public bool Isdefault { get; set; }
    public string Fuseorgid { get; set; } = string.Empty;
    public bool Fisbuildself { get; set; }
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public DateTime? CYmd { get; set; }
}

public class AssistantDataEntryDetailDto : AssistantDataEntryListDto
{
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class CreateAssistantDataEntryRequest
{
    [Required(ErrorMessage = "辅助资料代码不能为空")]
    public string Fnumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "辅助资料名称不能为空")]
    public string Fdatavalue { get; set; } = string.Empty;

    [Required(ErrorMessage = "辅助资料类别不能为空")]
    public string Fid { get; set; } = string.Empty;

    public string Fparentid { get; set; } = string.Empty;
    public string Fdescription { get; set; } = string.Empty;
    public int Fseq { get; set; }
    public bool Isdefault { get; set; }
    public string Fuseorgid { get; set; } = string.Empty;
    public bool Fisbuildself { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateAssistantDataEntryRequest
{
    [Required(ErrorMessage = "辅助资料名称不能为空")]
    public string Fdatavalue { get; set; } = string.Empty;

    public string Fparentid { get; set; } = string.Empty;
    public string Fdescription { get; set; } = string.Empty;
    public int Fseq { get; set; }
    public bool Isdefault { get; set; }
    public string Fuseorgid { get; set; } = string.Empty;
    public bool Fisbuildself { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}
