using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统同步信息表
/// </summary>
[SugarTable("T_SYN_INFO")]
public class TSynInfo : BaseEntity
{
    /// <summary>
    /// 功能代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 功能名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 系统预设
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// 最后同步时间
    /// </summary>
    [SugarColumn(ColumnName = "FTIMESTAMP")]
    public DateTime? Ftimestamp { get; set; }

    /// <summary>
    /// 时间单位
    /// </summary>
    [SugarColumn(ColumnName = "FTIMETYPE")]
    public string Ftimetype { get; set; } = string.Empty;

    /// <summary>
    /// 同步频率
    /// </summary>
    [SugarColumn(ColumnName = "FSYNRATE")]
    public int Fsynrate { get; set; }

    /// <summary>
    /// 是否启用动步
    /// </summary>
    [SugarColumn(ColumnName = "FISENABLE")]
    public bool Fisenable { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间点
    /// </summary>
    [SugarColumn(ColumnName = "FSTART")]
    public int Fstart { get; set; }

    /// <summary>
    /// 同步过滤规则
    /// </summary>
    [SugarColumn(ColumnName = "FRULEID")]
    public string Fruleid { get; set; } = string.Empty;

    /// <summary>
    /// ERP表单ID
    /// </summary>
    [SugarColumn(ColumnName = "FERPBILLID")]
    public string Ferpbillid { get; set; } = string.Empty;
}
