using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购预测单表体
/// </summary>
[SugarTable("ODK_SRM_PurchaseForecastEntry")]
public class OdkSrmPurchaseForecastEntry : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 关联采购量
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMITQTY")]
    public decimal Fcommitqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

    /// <summary>
    /// 指定人
    /// </summary>
    [SugarColumn(ColumnName = "FDESIGNATEDPERSON")]
    public string Fdesignatedperson { get; set; } = string.Empty;

    /// <summary>
    /// 指定时间
    /// </summary>
    [SugarColumn(ColumnName = "FDESIGNATEDDATE")]
    public DateTime? Fdesignateddate { get; set; }

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

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
