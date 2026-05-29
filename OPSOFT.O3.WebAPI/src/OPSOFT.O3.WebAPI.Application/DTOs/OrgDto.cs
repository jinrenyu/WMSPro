namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 当前用户可访问的组织
/// </summary>
public class MyOrgDto
{
    public string OrgId { get; set; } = string.Empty;       // 组织内码（SysUserOrg.Forgid = SysOrgStructure.Uid）
    public string OrgNumber { get; set; } = string.Empty;   // 组织代号
    public string OrgName { get; set; } = string.Empty;     // 组织名称
    public string ParentOrgId { get; set; } = string.Empty; // 上级/所属组织（SysOrgStructure.Fparaid，无则取自身）—— 物料 FCompanyId 取此值
    public string ParentOrgName { get; set; } = string.Empty; // 上级组织名称
    public bool IsDefault { get; set; }                     // 是否默认组织
}
