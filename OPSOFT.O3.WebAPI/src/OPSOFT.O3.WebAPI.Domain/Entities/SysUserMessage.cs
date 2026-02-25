using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户间信息档
/// </summary>
[SugarTable("SYS_USERMESSAGE")]
public class SysUserMessage : BaseEntity
{
    /// <summary>
    /// 信息主题
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 信息内容
    /// </summary>
    [SugarColumn(ColumnName = "FINFO")]
    public string Finfo { get; set; } = string.Empty;

    /// <summary>
    /// 信息发送时间
    /// </summary>
    [SugarColumn(ColumnName = "FDATETIME")]
    public DateTime? Fdatetime { get; set; }

    /// <summary>
    /// 信息发送人
    /// </summary>
    [SugarColumn(ColumnName = "FFROMUSER")]
    public string Ffromuser { get; set; } = string.Empty;

    /// <summary>
    /// 信息接收人
    /// </summary>
    [SugarColumn(ColumnName = "FTOUSER")]
    public string Ftouser { get; set; } = string.Empty;

    /// <summary>
    /// 是否发送所有人
    /// </summary>
    [SugarColumn(ColumnName = "FISALL")]
    public bool Fisall { get; set; }

    /// <summary>
    /// 是否系统信息
    /// </summary>
    [SugarColumn(ColumnName = "FISSYS")]
    public bool Fissys { get; set; }
}
