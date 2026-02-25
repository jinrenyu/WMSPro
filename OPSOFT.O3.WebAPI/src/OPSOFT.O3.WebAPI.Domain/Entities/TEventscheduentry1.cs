using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 事件调度-通知人
/// </summary>
[SugarTable("T_EVENTSCHEDUENTRY1")]
public class TEventscheduentry1 : BaseEntity
{
    /// <summary>
    /// 通知人内码
    /// </summary>
    [SugarColumn(ColumnName = "FEVTID")]
    public string Fevtid { get; set; } = string.Empty;

    /// <summary>
    /// 是否存在邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FISEM")]
    public bool Fisem { get; set; }

    /// <summary>
    /// 邮箱号
    /// </summary>
    [SugarColumn(ColumnName = "FEMAILNUM")]
    public string Femailnum { get; set; } = string.Empty;

    /// <summary>
    /// 是否发生邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEISSEND")]
    public bool Feissend { get; set; }

    /// <summary>
    /// 微信号
    /// </summary>
    [SugarColumn(ColumnName = "FWECHAT")]
    public string Fwechat { get; set; } = string.Empty;

    /// <summary>
    /// 是否发送微信
    /// </summary>
    [SugarColumn(ColumnName = "FWISSEND")]
    public bool Fwissend { get; set; }

    /// <summary>
    /// 通知人姓名
    /// </summary>
    [SugarColumn(ColumnName = "FEVTTNAME")]
    public string Fevttname { get; set; } = string.Empty;

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
