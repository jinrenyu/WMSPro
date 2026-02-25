using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统安全策略设定表
/// </summary>
[SugarTable("SYS_SECURITYSTRATEGY")]
public class SysSecurityStrategy : BaseEntity
{
    /// <summary>
    /// 是否启用安全策略
    /// </summary>
    [SugarColumn(ColumnName = "FENABLED")]
    public bool Fenabled { get; set; }

    /// <summary>
    /// 登录密码最小长度 <=0 为不限止
    /// </summary>
    [SugarColumn(ColumnName = "FPLENGHT")]
    public int Fplenght { get; set; }

    /// <summary>
    /// 密码组合
    /// </summary>
    [SugarColumn(ColumnName = "FPCOMBINATION")]
    public string Fpcombination { get; set; } = string.Empty;

    /// <summary>
    /// 密码多少次不可以重复 0 为不控制
    /// </summary>
    [SugarColumn(ColumnName = "FPREPETITIONTIME")]
    public int Fprepetitiontime { get; set; }

    /// <summary>
    /// 超过多少天必须修改密码
    /// </summary>
    [SugarColumn(ColumnName = "FCPDAYS")]
    public int Fcpdays { get; set; }

    /// <summary>
    /// 超过分钟未使用系统退出
    /// </summary>
    [SugarColumn(ColumnName = "FLOGOUTMINUTES")]
    public int Flogoutminutes { get; set; }

    /// <summary>
    /// 管理员可查询业务单据
    /// </summary>
    [SugarColumn(ColumnName = "FENABLEADMIN")]
    public bool Fenableadmin { get; set; }

    /// <summary>
    /// 提前提醒天数
    /// </summary>
    [SugarColumn(ColumnName = "FREMINDERDAYS")]
    public int Freminderdays { get; set; }

    /// <summary>
    /// 密码锁定允许输入密码错误次数
    /// </summary>
    [SugarColumn(ColumnName = "FERRORLIMIT")]
    public int Ferrorlimit { get; set; }

    /// <summary>
    /// 自动解锁时间，单位分钟
    /// </summary>
    [SugarColumn(ColumnName = "FAUTOUNLOCKMINS")]
    public int Fautounlockmins { get; set; }
}
