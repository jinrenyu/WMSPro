using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class MaterialBarTypeListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string Fmaterialid { get; set; } = string.Empty;
    public int Fbartype { get; set; }
    public string Fmaterialnumber { get; set; } = string.Empty;
    public string Fmaterialname { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class MaterialBarTypeDetailDto : MaterialBarTypeListDto
{
}

public class CreateMaterialBarTypeRequest
{
    [Required(ErrorMessage = "物料编码不能为空")]
    public string Fmaterialnumber { get; set; } = string.Empty;

    public string Fmaterialid { get; set; } = string.Empty;
    public int Fbartype { get; set; }
    public string Fmaterialname { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateMaterialBarTypeRequest
{
    public string Fmaterialid { get; set; } = string.Empty;
    public int Fbartype { get; set; }
    public string Fmaterialnumber { get; set; } = string.Empty;
    public string Fmaterialname { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string Fcheckerid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}
