using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工作中心日历-表体
/// </summary>
[SugarTable("T_WCCALENDARENTRY")]
public class TWccalendarentry : BaseEntity
{
    /// <summary>
    /// 日历日期
    /// </summary>
    [SugarColumn(ColumnName = "FCALENDARDATE")]
    public DateTime? Fcalendardate { get; set; }

    /// <summary>
    /// 累计工作天数
    /// </summary>
    [SugarColumn(ColumnName = "FWORKINGDAYS")]
    public decimal Fworkingdays { get; set; }

    /// <summary>
    /// 工作日起始日期
    /// </summary>
    [SugarColumn(ColumnName = "FCORWORKDATE")]
    public DateTime? Fcorworkdate { get; set; }

    /// <summary>
    /// 是否为假期
    /// </summary>
    [SugarColumn(ColumnName = "FISHOLIDAY")]
    public bool Fisholiday { get; set; }

    /// <summary>
    /// 时间模型内码
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTMODEID")]
    public string Fshiftmodeid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
