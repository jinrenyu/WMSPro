using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序汇报表体-车间汇报
/// </summary>
[SugarTable("T_SFC_OPTRPTENTRY")]
public class TSfcOptrptentry : BaseEntity
{
    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public int Fsrcentryid { get; set; }

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID")]
    public string Fsrcdetailid { get; set; } = string.Empty;

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
    /// 生产订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public string Fmoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ")]
    public int Fmoentryseq { get; set; }

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 工序单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 完工数量
    /// </summary>
    [SugarColumn(ColumnName = "FFINISHQTY")]
    public decimal Ffinishqty { get; set; }

    /// <summary>
    /// 检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKQTY")]
    public decimal Fcheckqty { get; set; }

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 作业时间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTIME")]
    public DateTime? Fworktime { get; set; }

    /// <summary>
    /// 作业人时(分
    /// </summary>
    [SugarColumn(ColumnName = "FPERSONTIME")]
    public decimal Fpersontime { get; set; }

    /// <summary>
    /// 合格品入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKINQUAAUXQTY", IsNullable = true)]
    public decimal? FSTOCKINQUAAUXQTY { get; set; }

    /// <summary>
    /// 基本单位检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASECHECKQTY", IsNullable = true)]
    public decimal? FBASECHECKQTY { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 基本单位不合格品入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTOCKINFAILQTY", IsNullable = true)]
    public decimal? FBASESTOCKINFAILQTY { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEQUAQTY", IsNullable = true)]
    public decimal? FBASEQUAQTY { get; set; }

    /// <summary>
    /// 作业机时(分)
    /// </summary>
    [SugarColumn(ColumnName = "FEQUPTIME", IsNullable = true)]
    public decimal? FEQUPTIME { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID", IsNullable = true)]
    public string FBASEUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 工单工序行内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOPROCESSID", IsNullable = true)]
    public string FMOPROCESSID { get; set; } = string.Empty;

    /// <summary>
    /// 不合格品入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKINFAILAUXQTY", IsNullable = true)]
    public decimal? FSTOCKINFAILAUXQTY { get; set; }

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRDTYPE", IsNullable = true)]
    public int? FPRDTYPE { get; set; }

    /// <summary>
    /// 基本单位不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEFAILQTY", IsNullable = true)]
    public decimal? FBASEFAILQTY { get; set; }

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID", IsNullable = true)]
    public string FBOMID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位完工数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEFINISHQTY", IsNullable = true)]
    public decimal? FBASEFINISHQTY { get; set; }

    /// <summary>
    /// 基本单位合格品入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTOCKINQUAQTY", IsNullable = true)]
    public decimal? FBASESTOCKINQUAQTY { get; set; }
}
