using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 点检调度平台-点检计划
/// </summary>
[SugarTable("T_ENG_SPOTCHECKPLANTASKENTRY")]
public class TEngSpotcheckplantaskentry : BaseEntity
{
    /// <summary>
    /// 点检计划内码
    /// </summary>
    [SugarColumn(ColumnName = "FPLANID")]
    public string Fplanid { get; set; } = string.Empty;

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
