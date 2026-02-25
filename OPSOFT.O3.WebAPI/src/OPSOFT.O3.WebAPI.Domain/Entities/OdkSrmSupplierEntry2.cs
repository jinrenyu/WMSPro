using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商银行信息
/// </summary>
[SugarTable("ODK_SRM_SupplierEntry2")]
public class OdkSrmSupplierEntry2 : BaseEntity
{
    /// <summary>
    /// 银行内码
    /// </summary>
    [SugarColumn(ColumnName = "FBANKID")]
    public string Fbankid { get; set; } = string.Empty;

    /// <summary>
    /// 银行账号
    /// </summary>
    [SugarColumn(ColumnName = "FBANKACCOUNT")]
    public string Fbankaccount { get; set; } = string.Empty;

    /// <summary>
    /// 户主
    /// </summary>
    [SugarColumn(ColumnName = "FHOUSEHOLDER")]
    public string Fhouseholder { get; set; } = string.Empty;

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
