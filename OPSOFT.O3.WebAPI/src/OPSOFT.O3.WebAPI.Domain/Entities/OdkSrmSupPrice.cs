using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购价格管理
/// </summary>
[SugarTable("ODK_SRM_SupPrice")]
public class OdkSrmSupPrice : BaseEntity
{
    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 币别内码
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 订货量(从
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTQTY")]
    public decimal Fstartqty { get; set; }
}
