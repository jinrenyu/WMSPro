using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外入库单明细
/// </summary>
[SugarTable("T_SUB_INSTKPICKMTLENTRY")]
public class TSubInstkpickmtlentry : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 应收数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 实收数量
    /// </summary>
    [SugarColumn(ColumnName = "FREALQTY")]
    public decimal Frealqty { get; set; }

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTTYPE")]
    public string Fproducttype { get; set; } = string.Empty;

    /// <summary>
    /// 入库类型
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKTYPE")]
    public string Finstocktype { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单分录内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public string Fmoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ")]
    public int Fmoentryseq { get; set; }

    /// <summary>
    /// 项目编号
    /// </summary>
    [SugarColumn(ColumnName = "FPROJECTNO")]
    public string Fprojectno { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID")]
    public string Fsrcdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public int Fsrcentryid { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public string Fkfdate { get; set; } = string.Empty;

    /// <summary>
    /// 有效期至(变更后
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }
}
