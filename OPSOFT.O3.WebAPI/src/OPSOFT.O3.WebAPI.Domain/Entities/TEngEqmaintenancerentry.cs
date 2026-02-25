using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备维修商表体-维修设备信息
/// </summary>
[SugarTable("T_ENG_EQMAINTENANCERENTRY")]
public class TEngEqmaintenancerentry : BaseEntity
{
    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPID")]
    public string Fequipid { get; set; } = string.Empty;

    /// <summary>
    /// 维修范围
    /// </summary>
    [SugarColumn(ColumnName = "FMACONTEXT")]
    public string Fmacontext { get; set; } = string.Empty;

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
