using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外用料清单
/// </summary>
[SugarTable("T_SUB_PPBOM")]
public class TSubPpbom : BaseEntity
{
    /// <summary>
    /// 产品编号
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public string Fbaseunitqty { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQTYPE")]
    public string Fsubreqtype { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQID")]
    public string Fsubreqid { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单号
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQBILLNO")]
    public string Fsubreqbillno { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQENTRYID")]
    public string Fsubreqentryid { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSUBREQENTRYSEQ")]
    public int Fsubreqentryseq { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FDOCUMENTSTATUS")]
    public string Fdocumentstatus { get; set; } = string.Empty;

    /// <summary>
    /// 产品货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTOWNERTYPEID")]
    public string Fparentownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 产品货主
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTOWNERID")]
    public string Fparentownerid { get; set; } = string.Empty;

    /// <summary>
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEORGID")]
    public string Fsettleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 委外订单行镜像
    /// </summary>
    [SugarColumn(ColumnName = "FREQENTRYMIRROR")]
    public string Freqentrymirror { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单ID
    /// </summary>
    [SugarColumn(ColumnName = "FSALEORDERID")]
    public string Fsaleorderid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单
    /// </summary>
    [SugarColumn(ColumnName = "FSALEORDERNO")]
    public string Fsaleorderno { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSALEORDERENTRYID")]
    public string Fsaleorderentryid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSALEORDERENTRYSEQ")]
    public int Fsaleorderentryseq { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 采购订单ID
    /// </summary>
    [SugarColumn(ColumnName = "FPURPOORDERID")]
    public string Fpurpoorderid { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单
    /// </summary>
    [SugarColumn(ColumnName = "FPURPOORDERNO")]
    public string Fpurpoorderno { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FPURPOORDERENTRYID")]
    public string Fpurpoorderentryid { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FPURPOORDERENTRYSEQ")]
    public string Fpurpoorderentryseq { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
