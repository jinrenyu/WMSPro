using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 招标评审文件费
/// </summary>
[SugarTable("ODK_SRM_TenderAEFee")]
public class OdkSrmTenderAEFee : BaseEntity
{
    /// <summary>
    /// 评分单号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;

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
    /// 投标单号
    /// </summary>
    [SugarColumn(ColumnName = "FBIDBILLNO")]
    public string Fbidbillno { get; set; } = string.Empty;

    /// <summary>
    /// 投标单内码
    /// </summary>
    [SugarColumn(ColumnName = "FBIDINTERID")]
    public string Fbidinterid { get; set; } = string.Empty;

    /// <summary>
    /// 投标方内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 文件单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 发放数量
    /// </summary>
    [SugarColumn(ColumnName = "FEXTENDQTY")]
    public decimal Fextendqty { get; set; }

    /// <summary>
    /// 应收金额
    /// </summary>
    [SugarColumn(ColumnName = "FRAMT")]
    public decimal Framt { get; set; }

    /// <summary>
    /// 实收金额
    /// </summary>
    [SugarColumn(ColumnName = "FARAMT")]
    public decimal Faramt { get; set; }

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
