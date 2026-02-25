using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 清场项目
/// </summary>
[SugarTable("T_MI_SITECLEAR")]
public class TMiSiteclear : BaseEntity
{
    /// <summary>
    /// 清场项目编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 清场项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FMISTATUS")]
    public int Fmistatus { get; set; }

    /// <summary>
    /// 执行要求
    /// </summary>
    [SugarColumn(ColumnName = "FEXEREQ")]
    public string Fexereq { get; set; } = string.Empty;

    /// <summary>
    /// 执行工具
    /// </summary>
    [SugarColumn(ColumnName = "FEXETOOL")]
    public string Fexetool { get; set; } = string.Empty;

    /// <summary>
    /// 是否是重点项目
    /// </summary>
    [SugarColumn(ColumnName = "FISIMP")]
    public bool Fisimp { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }
}
