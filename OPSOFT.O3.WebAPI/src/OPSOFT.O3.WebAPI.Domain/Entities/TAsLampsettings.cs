using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯三色灯配置
/// </summary>
[SugarTable("T_AS_LAMPSETTINGS")]
public class TAsLampsettings : BaseEntity
{
    /// <summary>
    /// 配置代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 配置名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 多色灯ID
    /// </summary>
    [SugarColumn(ColumnName = "FLAMPID")]
    public string Flampid { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    [SugarColumn(ColumnName = "FPORT")]
    public int Fport { get; set; }

    /// <summary>
    /// ip
    /// </summary>
    [SugarColumn(ColumnName = "FIP")]
    public string Fip { get; set; } = string.Empty;

    /// <summary>
    /// 输出控制
    /// </summary>
    [SugarColumn(ColumnName = "FOUTPUT")]
    public string Foutput { get; set; } = string.Empty;

    /// <summary>
    /// 安灯颜色
    /// </summary>
    [SugarColumn(ColumnName = "FCOLOR")]
    public int Fcolor { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// API路由路径
    /// </summary>
    [SugarColumn(ColumnName = "FROUTE")]
    public string Froute { get; set; } = string.Empty;
}
