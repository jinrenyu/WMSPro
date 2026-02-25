using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 信息是否已读标记档
/// </summary>
[SugarTable("SYS_USERMESSAGEENTRY")]
public class SysUserMessageEntry : BaseEntity
{
    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FDATETIME")]
    public DateTime? Fdatetime { get; set; }

    /// <summary>
    /// 信息读取人
    /// </summary>
    [SugarColumn(ColumnName = "FREADUSER")]
    public string Freaduser { get; set; } = string.Empty;

    /// <summary>
    /// 是否已读
    /// </summary>
    [SugarColumn(ColumnName = "FISREAD")]
    public bool Fisread { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    [SugarColumn(ColumnName = "FISDEL")]
    public bool Fisdel { get; set; }
}
