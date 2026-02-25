using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 消息推送模板配置-涉及数据表
/// </summary>
[SugarTable("T_SPC_MESSAGESENDTEMPENTRY")]
public class TSpcMessagesendtempentry : BaseEntity
{
    /// <summary>
    /// 数据表
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID")]
    public string Ftableid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
