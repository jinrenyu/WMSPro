using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购订单表体--排程送货明细
/// </summary>
[SugarTable("ODK_SRM_POOrderEntry1")]
public class OdkSrmPOOrderEntry1 : BaseEntity
{
    /// <summary>
    /// 需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 承诺交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FFBDATE")]
    public DateTime? Ffbdate { get; set; }

    /// <summary>
    /// 承诺交货数量
    /// </summary>
    [SugarColumn(ColumnName = "FFBQTY")]
    public decimal Ffbqty { get; set; }

    /// <summary>
    /// 反馈信息
    /// </summary>
    [SugarColumn(ColumnName = "FFBINF")]
    public string Ffbinf { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FPODETAILID")]
    public string Fpodetailid { get; set; } = string.Empty;

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
