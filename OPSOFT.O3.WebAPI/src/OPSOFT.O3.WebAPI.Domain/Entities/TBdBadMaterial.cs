using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料不良
/// </summary>
[SugarTable("T_BD_BADMATERIAL")]
public class TBdBadmaterial : BaseEntity
{
    /// <summary>
    /// 不良项目
    /// </summary>
    [SugarColumn(ColumnName = "FBADMATID")]
    public string Fbadmatid { get; set; } = string.Empty;

    /// <summary>
    /// 不良个数
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public string Fcount { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
    /// 不良原因ID
    /// </summary>
    [SugarColumn(ColumnName = "FRESAONID")]
    public string Fresaonid { get; set; } = string.Empty;

    /// <summary>
    /// 责任归属
    /// </summary>
    [SugarColumn(ColumnName = "FDUTY")]
    public string Fduty { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

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
