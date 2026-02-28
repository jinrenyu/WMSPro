using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// Refresh Token 实体
/// </summary>
[SugarTable("SYS_REFRESHTOKEN")]
public class SysRefreshToken : BaseEntity
{
    /// <summary>
    /// 用户UID（外键）
    /// </summary>
    [SugarColumn(ColumnName = "USER_UID")]
    public string UserUid { get; set; } = string.Empty;

    /// <summary>
    /// 用户ID（冗余，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Token 哈希值（SHA256）
    /// </summary>
    [SugarColumn(ColumnName = "TOKEN_HASH", Length = 64)]
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>
    /// 过期时间
    /// </summary>
    [SugarColumn(ColumnName = "EXPIRES_AT")]
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// 创建时的IP地址
    /// </summary>
    [SugarColumn(ColumnName = "CREATED_IP", Length = 45)]
    public string CreatedIp { get; set; } = string.Empty;

    /// <summary>
    /// 创建时的 User-Agent
    /// </summary>
    [SugarColumn(ColumnName = "CREATED_USER_AGENT", Length = 500)]
    public string CreatedUserAgent { get; set; } = string.Empty;

    /// <summary>
    /// 最后使用时间
    /// </summary>
    [SugarColumn(ColumnName = "LAST_USED_AT", IsNullable = true)]
    public DateTime? LastUsedAt { get; set; }

    /// <summary>
    /// 是否已撤销
    /// </summary>
    [SugarColumn(ColumnName = "IS_REVOKED")]
    public bool IsRevoked { get; set; }

    /// <summary>
    /// 撤销时间
    /// </summary>
    [SugarColumn(ColumnName = "REVOKED_AT", IsNullable = true)]
    public DateTime? RevokedAt { get; set; }

    /// <summary>
    /// 撤销原因
    /// </summary>
    [SugarColumn(ColumnName = "REVOKED_REASON", Length = 200, IsNullable = true)]
    public string RevokedReason { get; set; } = string.Empty;
}
