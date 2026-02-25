using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 专家费用
/// </summary>
[SugarTable("ODK_SRM_ExpertFee")]
public class OdkSrmExpertFee : BaseEntity
{
    /// <summary>
    /// 招标单编号
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERBILLNO")]
    public string Ftenderbillno { get; set; } = string.Empty;

    /// <summary>
    /// 招标单内码
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERINTERID")]
    public string Ftenderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 专家内码
    /// </summary>
    [SugarColumn(ColumnName = "FEXPERTID")]
    public string Fexpertid { get; set; } = string.Empty;

    /// <summary>
    /// 应收金额
    /// </summary>
    [SugarColumn(ColumnName = "FPAMT")]
    public decimal Fpamt { get; set; }

    /// <summary>
    /// 实收金额
    /// </summary>
    [SugarColumn(ColumnName = "FAPAMT")]
    public decimal Fapamt { get; set; }

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 凭证号
    /// </summary>
    [SugarColumn(ColumnName = "FVOUCHERNO")]
    public string Fvoucherno { get; set; } = string.Empty;

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
