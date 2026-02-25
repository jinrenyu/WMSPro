using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 个人信息公告
/// </summary>
[SugarTable("SYS_PRESONINFO")]
public class SysPresonInfo : BaseEntity
{
    /// <summary>
    /// 消息编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 消息推送名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 消息内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 消息状态
    /// </summary>
    [SugarColumn(ColumnName = "FINFOSTATUS")]
    public string Finfostatus { get; set; } = string.Empty;

    /// <summary>
    /// 收件人
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVER")]
    public string Freceiver { get; set; } = string.Empty;

    /// <summary>
    /// 消息类型
    /// </summary>
    [SugarColumn(ColumnName = "FMESSAGETYPE")]
    public string Fmessagetype { get; set; } = string.Empty;
}
