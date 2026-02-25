using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户密码历史记录
/// </summary>
[SugarTable("SYS_USERPWHISTORY")]
public class SysUserPwHistory : BaseEntity
{
    /// <summary>
    /// 用户帐号
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// 修改日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "FPASSWORD")]
    public string Fpassword { get; set; } = string.Empty;
}
