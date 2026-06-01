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
    // 成本中心（T_CB_COSTCENTER，JOIN by FCostCenterId == FInterId）
    public string FCostCenterId { get; set; } = string.Empty;
    public string FCostCenterNumber { get; set; } = string.Empty;
    public string FCostCenterName { get; set; } = string.Empty;
    // 职务（T_BAS_ASSISTANTDATAENTRY 类别 FNUMBER='D'，JOIN by FDutyId == FInterId）
    public string FDutyId { get; set; } = string.Empty;
    public string FDutyNumber { get; set; } = string.Empty;
    public string FDutyName { get; set; } = string.Empty;
    // 其它新增字段
    public DateTime FTransferDate { get; set; }
    public decimal FPersonnelCoefficient { get; set; }
    public decimal FDutyLevel { get; set; }
    public string FDingTalkUserId { get; set; } = string.Empty;
    public string FPictureBase64 { get; set; } = string.Empty;
    // 使用组织 / 审计（只读）
    public string FCompanyId { get; set; } = string.Empty;
    public string FCompanyName { get; set; } = string.Empty;
    public string CUser { get; set; } = string.Empty;
    public string MUser { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string FCheckerId { get; set; } = string.Empty;
    public DateTime? FCheckDate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
}

/// <summary>成本中心 / 职务 下拉项</summary>
public class EmployeeLookupOptionDto
{
    public string Uid { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
}

public class CreateEmployeeRequest
{
    [Required(ErrorMessage = "员工编码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "姓名不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;
    public int FSex { get; set; }
    public string FEducation { get; set; } = string.Empty;
    public DateTime? FBirthDate { get; set; }
    public DateTime? FEntryDate { get; set; }
    public DateTime? FTransferDate { get; set; }
    public string FTel { get; set; } = string.Empty;
    public string FSalDeptId { get; set; } = string.Empty;
    public string FCostCenterId { get; set; } = string.Empty;
    public string FDutyId { get; set; } = string.Empty;
    public decimal FDutyLevel { get; set; }
    public decimal FPersonnelCoefficient { get; set; }
    public string FMail { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FQq { get; set; } = string.Empty;
    public string FWechat { get; set; } = string.Empty;
    public string FEmergencyTel { get; set; } = string.Empty;
    public string Emergency { get; set; } = string.Empty;
    public string FDingTalkUserId { get; set; } = string.Empty;
    public string FPictureBase64 { get; set; } = string.Empty;
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
    public DateTime? FTransferDate { get; set; }
    public DateTime? FDepartureDate { get; set; }
    public bool FIsDeparture { get; set; }
    public string FTel { get; set; } = string.Empty;
    public string FSalDeptId { get; set; } = string.Empty;
    public string FCostCenterId { get; set; } = string.Empty;
    public string FDutyId { get; set; } = string.Empty;
    public decimal FDutyLevel { get; set; }
    public decimal FPersonnelCoefficient { get; set; }
    public string FMail { get; set; } = string.Empty;
    public string FNote { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FQq { get; set; } = string.Empty;
    public string FWechat { get; set; } = string.Empty;
    public string FEmergencyTel { get; set; } = string.Empty;
    public string Emergency { get; set; } = string.Empty;
    public string FDingTalkUserId { get; set; } = string.Empty;
    public string FPictureBase64 { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}
