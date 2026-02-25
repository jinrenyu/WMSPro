using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 报价单表体3_价格修改履历
/// </summary>
[SugarTable("ODK_SRM_QuoteEntry2")]
public class OdkSrmQuoteEntry2 : BaseEntity
{
    /// <summary>
    /// 关联内码
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONID")]
    public string Frelationid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 修改价格描述
    /// </summary>
    [SugarColumn(ColumnName = "FDETAIL")]
    public string Fdetail { get; set; } = string.Empty;

    /// <summary>
    /// 修改后价格
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
