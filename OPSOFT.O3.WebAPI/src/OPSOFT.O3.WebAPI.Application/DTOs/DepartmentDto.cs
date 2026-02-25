using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class DepartmentListDto
{
    public string Uid { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FFullName { get; set; } = string.Empty;
    public string FParentId { get; set; } = string.Empty;
    public string FManager { get; set; } = string.Empty;
    public bool FIsLine { get; set; }
    public DateTime? CYmd { get; set; }
}

public class DepartmentDetailDto : DepartmentListDto
{
    public string FDescription { get; set; } = string.Empty;
    public string FHelpCode { get; set; } = string.Empty;
    public string FCreateOrgId { get; set; } = string.Empty;
    public string FDeptProperty { get; set; } = string.Empty;
    public bool FCostAccountType { get; set; }
    public DateTime? FEffectDate { get; set; }
    public DateTime? FLapseDate { get; set; }
    public string FErpNumber { get; set; } = string.Empty;
}

public class DepartmentTreeDto : DepartmentListDto
{
    public List<DepartmentTreeDto> Children { get; set; } = new();
}

public class CreateDepartmentRequest
{
    [Required(ErrorMessage = "部门代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "部门名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public string FFullName { get; set; } = string.Empty;
    public string FHelpCode { get; set; } = string.Empty;
    public string FParentId { get; set; } = string.Empty;
    public string FManager { get; set; } = string.Empty;
    public bool FIsLine { get; set; }
    public string FDeptProperty { get; set; } = string.Empty;
    public bool FCostAccountType { get; set; }
    public DateTime? FEffectDate { get; set; }
    public DateTime? FLapseDate { get; set; }
}

public class UpdateDepartmentRequest
{
    [Required(ErrorMessage = "部门名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public string FFullName { get; set; } = string.Empty;
    public string FHelpCode { get; set; } = string.Empty;
    public string FParentId { get; set; } = string.Empty;
    public string FManager { get; set; } = string.Empty;
    public bool FIsLine { get; set; }
    public string FDeptProperty { get; set; } = string.Empty;
    public bool FCostAccountType { get; set; }
    public DateTime? FEffectDate { get; set; }
    public DateTime? FLapseDate { get; set; }
}
