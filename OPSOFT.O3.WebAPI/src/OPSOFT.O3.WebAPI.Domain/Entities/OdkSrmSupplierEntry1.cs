using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商证书资质信息
/// </summary>
[SugarTable("ODK_SRM_SupplierEntry1")]
public class OdkSrmSupplierEntry1 : BaseEntity
{
    /// <summary>
    /// 证书类型
    /// </summary>
    [SugarColumn(ColumnName = "FCERTIFICATETYPE")]
    public string Fcertificatetype { get; set; } = string.Empty;

    /// <summary>
    /// 证书名称
    /// </summary>
    [SugarColumn(ColumnName = "FCERTIFICATENAME")]
    public string Fcertificatename { get; set; } = string.Empty;

    /// <summary>
    /// 证书号
    /// </summary>
    [SugarColumn(ColumnName = "FCERTIFICATENO")]
    public string Fcertificateno { get; set; } = string.Empty;

    /// <summary>
    /// 证书生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FCERTIFICATEDATES")]
    public DateTime? Fcertificatedates { get; set; }

    /// <summary>
    /// 证书失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FCERTIFICATEDATEE")]
    public DateTime? Fcertificatedatee { get; set; }

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
