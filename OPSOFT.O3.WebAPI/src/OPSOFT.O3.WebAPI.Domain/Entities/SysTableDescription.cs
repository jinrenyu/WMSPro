using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统表结构定义
/// </summary>
[SugarTable("SYS_TABLEDESCRIPTION")]
public class SysTableDescription : BaseEntity
{
    /// <summary>
    /// 实体表名
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 详细描述(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;
}
