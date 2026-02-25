using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 消息推送配置-涉及字段限制条件
/// </summary>
[SugarTable("T_SPC_MESSAGESENDENTRY1")]
public class TSpcMessagesendentry1 : BaseEntity
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
    /// 条件控制
    /// </summary>
    [SugarColumn(ColumnName = "FCONDITCONTROL")]
    public string Fconditcontrol { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

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
