using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class BaseDataGroupListDto
{
    public string Uid { get; set; } = string.Empty;
    public string FPrgKey { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FCName { get; set; } = string.Empty;
    public string FTName { get; set; } = string.Empty;
    public string FEName { get; set; } = string.Empty;
    public string FParentId { get; set; } = string.Empty;
    public string FFullNumber { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
}

public class BaseDataGroupDetailDto : BaseDataGroupListDto
{
}

public class CreateBaseDataGroupRequest
{
    [Required(ErrorMessage = "分组代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "分组名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCName { get; set; } = string.Empty;
    public string FTName { get; set; } = string.Empty;
    public string FEName { get; set; } = string.Empty;
    public string FParentId { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
}

public class UpdateBaseDataGroupRequest
{
    [Required(ErrorMessage = "分组名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCName { get; set; } = string.Empty;
    public string FTName { get; set; } = string.Empty;
    public string FEName { get; set; } = string.Empty;
    public string FParentId { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
}
