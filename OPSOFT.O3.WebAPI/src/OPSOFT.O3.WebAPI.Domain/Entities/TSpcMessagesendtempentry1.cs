using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 消息推送模板配置-涉及字段
/// </summary>
[SugarTable("T_SPC_MESSAGESENDTEMPENTRY1")]
public class TSpcMessagesendtempentry1 : BaseEntity
{
    /// <summary>
    /// 数据表
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID")]
    public string Ftableid { get; set; } = string.Empty;

    /// <summary>
    /// 字段ID
    /// </summary>
    [SugarColumn(ColumnName = "FIELDID")]
    public string Fieldid { get; set; } = string.Empty;

    /// <summary>
    /// 是否必选
    /// </summary>
    [SugarColumn(ColumnName = "FISSELECT")]
    public bool Fisselect { get; set; }

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
