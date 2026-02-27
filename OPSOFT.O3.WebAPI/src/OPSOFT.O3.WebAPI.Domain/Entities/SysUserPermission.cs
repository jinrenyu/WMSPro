using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 角色权限档案
/// </summary>
[SugarTable("SYS_USERPERMISSION")]
public class SysUserPermission : BaseEntity
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(ColumnName = "FROLEID")]
    public string Froleid { get; set; } = string.Empty;

    /// <summary>
    /// 循环还是程式( B=循环, C =模组  P=程式
    /// </summary>
    [SugarColumn(ColumnName = "PR_CODE")]
    public string PrCode { get; set; } = string.Empty;

    /// <summary>
    /// 程式代号
    /// </summary>
    [SugarColumn(ColumnName = "PRG_ID", IsNullable = true)]
    public string PRG_ID { get; set; } = string.Empty;

    /// <summary>
    /// 用户权限字段
    /// </summary>
    [SugarColumn(ColumnName = "PR_MAP", IsNullable = true)]
    public byte[] PR_MAP { get; set; }
}
