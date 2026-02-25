using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备维护方案-设备备件
/// </summary>
[SugarTable("T_ENG_EQMTTASKPROJECTENTRY4")]
public class TEngEqmttaskprojectentry4 : BaseEntity
{
    /// <summary>
    /// 备件内码
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTID")]
    public string Fsparepartid { get; set; } = string.Empty;

    /// <summary>
    /// 备件数量
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTCOUNT")]
    public decimal Fsparepartcount { get; set; }

    /// <summary>
    /// 设备维护内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEMAINTID")]
    public string Fmachinemaintid { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

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
