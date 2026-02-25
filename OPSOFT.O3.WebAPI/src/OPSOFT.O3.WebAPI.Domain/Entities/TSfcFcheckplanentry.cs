using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 巡检方案表体
/// </summary>
[SugarTable("T_SFC_FCHECKPLANENTRY")]
public class TSfcFcheckplanentry : BaseEntity
{
    /// <summary>
    /// 检验内容
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKCONTENTID")]
    public string Fcheckcontentid { get; set; } = string.Empty;

    /// <summary>
    /// 序号
    /// </summary>
    [SugarColumn(ColumnName = "FSQUENCE")]
    public int Fsquence { get; set; }

    /// <summary>
    /// 检测方式 0=目检;1=机检
    /// </summary>
    [SugarColumn(ColumnName = "FTESTTYPE")]
    public int Ftesttype { get; set; }

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
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

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
