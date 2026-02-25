using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class EmployeeListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public int FSex { get; set; }
    public string FTel { get; set; } = string.Empty;
    public string FSalDeptId { get; set; } = string.Empty;
    public string FSalDeptName { get; set; } = string.Empty;
    public bool FIsDeparture { get; set; }
    public DateTime? CYmd { get; set; }
    public string FEducation { get; set; } = string.Empty;
    public DateTime FBirthDate { get; set; }
    public DateTime FEntryDate { get; set; }
    public DateTime FDepartureDate { get; set; }
    public string FMail { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FQq { get; set; } = string.Empty;
    public string FWechat { get; set; } = string.Empty;
    public string FEmergencyTel { get; set; } = string.Empty;
    public string Emergency { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class EmployeeDetailDto : EmployeeListDto
{
}

public class CreateEmployeeRequest
{
    [Required(ErrorMessage = "员工编码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "姓名不能为空")]
    public string FName { get; set; } = string.Empty;

    public int FSex { get; set; }
    public string FEducation { get; set; } = string.Empty;
    public DateTime? FBirthDate { get; set; }
    public DateTime? FEntryDate { get; set; }
    public string FTel { get; set; } = string.Empty;
    public string FSalDeptId { get; set; } = string.Empty;
    public string FMail { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FWechat { get; set; } = string.Empty;
    public string FEmergencyTel { get; set; } = string.Empty;
    public string Emergency { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateEmployeeRequest
{
    [Required(ErrorMessage = "姓名不能为空")]
    public string FName { get; set; } = string.Empty;

    public int FSex { get; set; }
    public string FEducation { get; set; } = string.Empty;
    public DateTime? FBirthDate { get; set; }
    public DateTime? FEntryDate { get; set; }
    public DateTime? FDepartureDate { get; set; }
    public bool FIsDeparture { get; set; }
    public string FTel { get; set; } = string.Empty;
    public string FSalDeptId { get; set; } = string.Empty;
    public string FMail { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FWechat { get; set; } = string.Empty;
    public string FEmergencyTel { get; set; } = string.Empty;
    public string Emergency { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}
