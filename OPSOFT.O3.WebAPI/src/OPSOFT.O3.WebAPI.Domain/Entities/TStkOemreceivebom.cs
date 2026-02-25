using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 受托加工收料单源单明细
/// </summary>
[SugarTable("T_STK_OEMRECEIVEBOM")]
public class TStkOemreceivebom : BaseEntity
{
    /// <summary>
    /// 展开项
    /// </summary>
    [SugarColumn(ColumnName = "FSELECT")]
    public string Fselect { get; set; } = string.Empty;

    /// <summary>
    /// 销售组织
    /// </summary>
    [SugarColumn(ColumnName = "FSALORGID")]
    public string Fsalorgid { get; set; } = string.Empty;

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 要货日期
    /// </summary>
    [SugarColumn(ColumnName = "FREQUIREDATE")]
    public DateTime? Frequiredate { get; set; }

    /// <summary>
    /// 订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSEQ")]
    public string Fsrcseq { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID")]
    public string Fsrcdetailid { get; set; } = string.Empty;

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
