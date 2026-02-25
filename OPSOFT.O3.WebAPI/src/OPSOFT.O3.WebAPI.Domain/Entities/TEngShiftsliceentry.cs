using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 班次表体
/// </summary>
[SugarTable("T_ENG_SHIFTSLICEENTRY")]
public class TEngShiftsliceentry : BaseEntity
{
    /// <summary>
    /// 时段代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 时段类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FBEGTIME")]
    public string Fbegtime { get; set; } = string.Empty;

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public string Fendtime { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
