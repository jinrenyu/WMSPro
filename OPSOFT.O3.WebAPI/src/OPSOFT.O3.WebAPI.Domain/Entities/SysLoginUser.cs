using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 登录用户表
/// </summary>
[SugarTable("SYS_LOGINUSER")]
public class SysLoginUser : BaseEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnName = "USER_NAME")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "PR_PASSWORD")]
    public string PrPassword { get; set; } = string.Empty;

    /// <summary>
    /// 是否PDA用户
    /// </summary>
    [SugarColumn(ColumnName = "IS_PDA")]
    public bool IsPda { get; set; }

    /// <summary>
    /// 关联类别(1=职员,2=厂商,3=客户)
    /// </summary>
    [SugarColumn(ColumnName = "PATYPE")]
    public string PaType { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "EMAIL")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 职员/厂商/客户内码
    /// </summary>
    [SugarColumn(ColumnName = "PA_ID")]
    public string PaId { get; set; } = string.Empty;

    /// <summary>
    /// ERP ID
    /// </summary>
    [SugarColumn(ColumnName = "ERPID")]
    public string ErpId { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool IsDefault { get; set; }

    /// <summary>
    /// 卡号
    /// </summary>
    [SugarColumn(ColumnName = "CARDID")]
    public string CardId { get; set; } = string.Empty;

    /// <summary>
    /// 域用户
    /// </summary>
    [SugarColumn(ColumnName = "DOMAINUSER")]
    public string DomainUser { get; set; } = string.Empty;

    /// <summary>
    /// 最后登录时间
    /// </summary>
    [SugarColumn(ColumnName = "LASTLOGINTIME")]
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 最后修改密码时间
    /// </summary>
    [SugarColumn(ColumnName = "LASTCPTIME")]
    public DateTime? LastCpTime { get; set; }

    /// <summary>
    /// 照片
    /// </summary>
    [SugarColumn(ColumnName = "PHOTO", IsIgnore = true)]
    public byte[]? Photo { get; set; }

    /// <summary>
    /// 密码错误次数
    /// </summary>
    [SugarColumn(ColumnName = "PWDERRTIMES")]
    public int PwdErrTimes { get; set; }

    /// <summary>
    /// 锁定状态
    /// </summary>
    [SugarColumn(ColumnName = "LOCKSTATUS")]
    public int LockStatus { get; set; }

    /// <summary>
    /// 最后锁定时间
    /// </summary>
    [SugarColumn(ColumnName = "LASTLOCKTIME")]
    public DateTime? LastLockTime { get; set; }
}
