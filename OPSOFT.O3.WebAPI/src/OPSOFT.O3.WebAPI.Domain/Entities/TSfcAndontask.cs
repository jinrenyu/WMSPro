using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯调度任务
/// </summary>
[SugarTable("T_SFC_ANDONTASK")]
public class TSfcAndontask : BaseEntity
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
    /// 时间单位
    /// </summary>
    [SugarColumn(ColumnName = "FTIMETYPE")]
    public string Ftimetype { get; set; } = string.Empty;

    /// <summary>
    /// 触发频率
    /// </summary>
    [SugarColumn(ColumnName = "FSYNRATE")]
    public int Fsynrate { get; set; }

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
}
