using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商档案实地评鉴
/// </summary>
[SugarTable("ODK_SRM_SupplierEntry6")]
public class OdkSrmSupplierEntry6 : BaseEntity
{
    /// <summary>
    /// 评估单号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;

    /// <summary>
    /// 评估单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSDETAILID")]
    public string Fassessdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 评鉴人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// ISO9000得分
    /// </summary>
    [SugarColumn(ColumnName = "FISO9000SCORE")]
    public decimal Fiso9000score { get; set; }

    /// <summary>
    /// 等级ID
    /// </summary>
    [SugarColumn(ColumnName = "FLEVELID")]
    public string Flevelid { get; set; } = string.Empty;

    /// <summary>
    /// ISO14000得分
    /// </summary>
    [SugarColumn(ColumnName = "FISO14000SCORE")]
    public decimal Fiso14000score { get; set; }

    /// <summary>
    /// 评鉴结果
    /// </summary>
    [SugarColumn(ColumnName = "FYNQUALIFIED")]
    public bool Fynqualified { get; set; }

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

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
