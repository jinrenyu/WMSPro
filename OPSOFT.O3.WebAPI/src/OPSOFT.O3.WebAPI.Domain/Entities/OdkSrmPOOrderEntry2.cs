using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购订单表体--排程送货明细
/// </summary>
[SugarTable("ODK_SRM_POOrderEntry2")]
public class OdkSrmPOOrderEntry2 : BaseEntity
{
    /// <summary>
    /// 采购订单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FPODETAILID")]
    public string Fpodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 计划送货行明细
    /// </summary>
    [SugarColumn(ColumnName = "FPCDETAILID")]
    public string Fpcdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 变更前日期
    /// </summary>
    [SugarColumn(ColumnName = "FBEFDATE")]
    public DateTime? Fbefdate { get; set; }

    /// <summary>
    /// 变更前数量
    /// </summary>
    [SugarColumn(ColumnName = "FBEFQTY")]
    public decimal Fbefqty { get; set; }

    /// <summary>
    /// 变更后日期
    /// </summary>
    [SugarColumn(ColumnName = "FAFTDATE")]
    public DateTime? Faftdate { get; set; }

    /// <summary>
    /// 变更后数量
    /// </summary>
    [SugarColumn(ColumnName = "FAFTQTY")]
    public decimal Faftqty { get; set; }

    /// <summary>
    /// 变动描述
    /// </summary>
    [SugarColumn(ColumnName = "FFBINF")]
    public string Ffbinf { get; set; } = string.Empty;

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
