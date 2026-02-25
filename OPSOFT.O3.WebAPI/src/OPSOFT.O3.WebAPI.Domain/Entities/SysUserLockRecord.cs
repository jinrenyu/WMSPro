using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户锁定记录
/// </summary>
[SugarTable("SYS_USERLOCKRECORD")]
public class SysUserLockRecord : BaseEntity
{
    /// <summary>
    /// 锁定用户ID
    /// </summary>
    [SugarColumn(ColumnName = "USERID")]
    public string Userid { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnName = "USERNAME")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 解锁激活时间
    /// </summary>
    [SugarColumn(ColumnName = "UNLOCKTIME")]
    public DateTime? Unlocktime { get; set; }

    /// <summary>
    /// 锁定状态
    /// </summary>
    [SugarColumn(ColumnName = "LOCKSTATUS")]
    public int Lockstatus { get; set; }

    /// <summary>
    /// 解锁人
    /// </summary>
    [SugarColumn(ColumnName = "UNLOCKUSER")]
    public string Unlockuser { get; set; } = string.Empty;

    /// <summary>
    /// 锁定时操作的机器IP
    /// </summary>
    [SugarColumn(ColumnName = "LOCKIP")]
    public string Lockip { get; set; } = string.Empty;

    /// <summary>
    /// 锁定时操作的机器名称
    /// </summary>
    [SugarColumn(ColumnName = "MACHINENAME")]
    public string Machinename { get; set; } = string.Empty;
}
