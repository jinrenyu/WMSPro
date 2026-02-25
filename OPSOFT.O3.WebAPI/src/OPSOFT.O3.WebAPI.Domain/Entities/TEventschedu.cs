using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 事件调度
/// </summary>
[SugarTable("T_EVENTSCHEDU")]
public class TEventschedu : BaseEntity
{
    /// <summary>
    /// 单据名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 最近执行时间
    /// </summary>
    [SugarColumn(ColumnName = "FLATELYTIME")]
    public DateTime? Flatelytime { get; set; }

    /// <summary>
    /// 事件类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 选择菜单
    /// </summary>
    [SugarColumn(ColumnName = "FMENUE")]
    public string Fmenue { get; set; } = string.Empty;

    /// <summary>
    /// 文本内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 附件路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FFNAME")]
    public string Ffname { get; set; } = string.Empty;

    /// <summary>
    /// 系统文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FFILENAME")]
    public string Ffilename { get; set; } = string.Empty;

    /// <summary>
    /// 表单代码
    /// </summary>
    [SugarColumn(ColumnName = "FBDNUMBER")]
    public string Fbdnumber { get; set; } = string.Empty;

    /// <summary>
    /// 数据源sql
    /// </summary>
    [SugarColumn(ColumnName = "FSQL")]
    public string Fsql { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 是否发送微信公众号
    /// </summary>
    [SugarColumn(ColumnName = "FISWEIXINGZH")]
    public bool Fisweixingzh { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
