using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 消息推送配置
/// </summary>
[SugarTable("T_SPC_MESSAGESEND")]
public class TSpcMessagesend : BaseEntity
{
    /// <summary>
    /// 消息推送名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 推送触发模式
    /// </summary>
    [SugarColumn(ColumnName = "FSENDTYPE")]
    public string Fsendtype { get; set; } = string.Empty;

    /// <summary>
    /// 消息推送模板ID
    /// </summary>
    [SugarColumn(ColumnName = "FSENDTEMPID")]
    public string Fsendtempid { get; set; } = string.Empty;

    /// <summary>
    /// 等级
    /// </summary>
    [SugarColumn(ColumnName = "FLEVEL")]
    public string Flevel { get; set; } = string.Empty;

    /// <summary>
    /// 消息推送途经
    /// </summary>
    [SugarColumn(ColumnName = "FSENDWAY")]
    public string Fsendway { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTATRTIME")]
    public int Fstatrtime { get; set; }

    /// <summary>
    /// 频率,每
    /// </summary>
    [SugarColumn(ColumnName = "FNUMRATE")]
    public int Fnumrate { get; set; }

    /// <summary>
    /// 周期
    /// </summary>
    [SugarColumn(ColumnName = "FCYCLE")]
    public string Fcycle { get; set; } = string.Empty;

    /// <summary>
    /// 执行节点
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTENODE")]
    public string Fexecutenode { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
