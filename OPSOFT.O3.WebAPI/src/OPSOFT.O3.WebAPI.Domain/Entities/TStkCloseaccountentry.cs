using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 关账设置表体
/// </summary>
[SugarTable("T_STK_CLOSEACCOUNTENTRY")]
public class TStkCloseaccountentry : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPE")]
    public string Fbilltype { get; set; } = string.Empty;

    /// <summary>
    /// 关账日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

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
