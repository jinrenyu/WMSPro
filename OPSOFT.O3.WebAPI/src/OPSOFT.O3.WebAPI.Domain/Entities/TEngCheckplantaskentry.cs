using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维护调度平台－设备维护方案
/// </summary>
[SugarTable("T_ENG_CHECKPLANTASKENTRY")]
public class TEngCheckplantaskentry : BaseEntity
{
    /// <summary>
    /// 设备维护方案内码
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
