using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统主模块信息
/// </summary>
[SugarTable("SYS_ROTATEINFO")]
public class SysRotateInfo : BaseEntity
{
    /// <summary>
    /// 模块代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 模块名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 模块名称(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;
}
