using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 事件调度-邮箱内容
/// </summary>
[SugarTable("T_EVENTSCHEDUENTRY2")]
public class TEventscheduentry2 : BaseEntity
{
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 附件上传路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 附件本地路径
    /// </summary>
    [SugarColumn(ColumnName = "FFILEPATH")]
    public string Ffilepath { get; set; } = string.Empty;

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
