using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 短信验证码 / MFA 会话表。
/// 单表同时承载「登录中间态票据(TicketHash)」与「短信验证码(CodeHash)」：
/// 登录密码校验通过后创建一行(仅 TicketHash)，发码时写入 CodeHash，校验成功后置 IsConsumed。
/// </summary>
[SugarTable("SYS_SMSCODE")]
public class SysSmsCode : BaseEntity
{
    /// <summary>用户 UID</summary>
    [SugarColumn(ColumnName = "USER_UID", Length = 50)]
    public string UserUid { get; set; } = string.Empty;

    /// <summary>用户账号(冗余，便于查询)</summary>
    [SugarColumn(ColumnName = "USER_ID", Length = 20)]
    public string UserId { get; set; } = string.Empty;

    /// <summary>接收验证码的手机号</summary>
    [SugarColumn(ColumnName = "MOBILE", Length = 30)]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>一次性登录票据哈希(SHA256)</summary>
    [SugarColumn(ColumnName = "TICKET_HASH", Length = 64)]
    public string TicketHash { get; set; } = string.Empty;

    /// <summary>验证码哈希(SHA256)，未发码时为空</summary>
    [SugarColumn(ColumnName = "CODE_HASH", Length = 64)]
    public string CodeHash { get; set; } = string.Empty;

    /// <summary>用途(login=登录MFA)</summary>
    [SugarColumn(ColumnName = "PURPOSE", Length = 20)]
    public string Purpose { get; set; } = "login";

    /// <summary>会话过期时间</summary>
    [SugarColumn(ColumnName = "EXPIRES_AT")]
    public DateTime ExpiresAt { get; set; }

    /// <summary>最后一次发码时间(用于发送频控)</summary>
    [SugarColumn(ColumnName = "CODE_SENT_AT", IsNullable = true)]
    public DateTime? CodeSentAt { get; set; }

    /// <summary>累计发送次数</summary>
    [SugarColumn(ColumnName = "SEND_COUNT")]
    public int SendCount { get; set; }

    /// <summary>当前验证码累计校验尝试次数</summary>
    [SugarColumn(ColumnName = "ATTEMPT_COUNT")]
    public int AttemptCount { get; set; }

    /// <summary>是否已消费(验证成功或作废)</summary>
    [SugarColumn(ColumnName = "IS_CONSUMED")]
    public bool IsConsumed { get; set; }

    /// <summary>消费时间</summary>
    [SugarColumn(ColumnName = "CONSUMED_AT", IsNullable = true)]
    public DateTime? ConsumedAt { get; set; }

    /// <summary>创建时的 IP</summary>
    [SugarColumn(ColumnName = "CREATED_IP", Length = 45)]
    public string CreatedIp { get; set; } = string.Empty;
}
