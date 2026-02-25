using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户登录信息表
/// </summary>
[SugarTable("SYS_LOGININFO")]
public class SysLoginInfo : BaseEntity
{
    /// <summary>
    /// 机器名
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPUTER")]
    public string Fcomputer { get; set; } = string.Empty;

    /// <summary>
    /// 登录用户
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// 登录时间
    /// </summary>
    [SugarColumn(ColumnName = "FLOGINTIME")]
    public DateTime? Flogintime { get; set; }

    /// <summary>
    /// 最后异动时间
    /// </summary>
    [SugarColumn(ColumnName = "FLASTTIME")]
    public DateTime? Flasttime { get; set; }
}
