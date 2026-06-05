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
    // ---- 使用组织 ----
    public string FCompanyId { get; set; } = string.Empty;
    public string FCompanyName { get; set; } = string.Empty;
    public string FGroupName { get; set; } = string.Empty;

    // ---- 物料带出（按 FMATERIALID = T_BD_MATERIAL.FINTERID 关联，只读）----
    public string FSpecification { get; set; } = string.Empty; // 规格型号
    public bool FFeedSn { get; set; }                          // 启用序列号

    // ---- 制单 / 修改（只读系统信息）----
    public string CUser { get; set; } = string.Empty;          // 制单人账号
    public string MUser { get; set; } = string.Empty;          // 修改人账号
    public DateTime? MYmd { get; set; }                        // 修改日期

    // ---- 关联解析的展示名（SYS_LOGINUSER / SYS_STATUS）----
    public string CUserName { get; set; } = string.Empty;      // 制单人姓名
    public string MUserName { get; set; } = string.Empty;      // 修改人姓名
    public string FCheckerName { get; set; } = string.Empty;   // 审核人姓名
    public string FDisableName { get; set; } = string.Empty;   // 禁用人姓名
    public string FStatusName { get; set; } = string.Empty;    // 单据状态名称
}

public class CreateMaterialBarTypeRequest
{
    [Required(ErrorMessage = "物料编码不能为空")]
    public string Fmaterialnumber { get; set; } = string.Empty;

    public string Fmaterialid { get; set; } = string.Empty;
    public int Fbartype { get; set; }
    public string Fmaterialname { get; set; } = string.Empty;
    public string FCompanyId { get; set; } = string.Empty;   // 使用组织（新增时取当前切换组织）
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateMaterialBarTypeRequest
{
    public int Fbartype { get; set; }                        // 条码类型（编辑时物料锁定，仅类型/分组可改）
    public string FGroupId { get; set; } = string.Empty;
}
