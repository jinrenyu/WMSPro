using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产汇报表-表体
/// </summary>
[SugarTable("T_PRD_MORPTENTRY")]
public class TPrdMorptentry : BaseEntity
{
    /// <summary>
    /// 生产汇报类型
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTTYPE")]
    public string Freporttype { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FFAILQTY")]
    public decimal Ffailqty { get; set; }

    /// <summary>
    /// 待返修数量
    /// </summary>
    [SugarColumn(ColumnName = "FREWORKQTY")]
    public decimal Freworkqty { get; set; }

    /// <summary>
    /// 报废数量
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQTY")]
    public decimal Fscrapqty { get; set; }

    /// <summary>
    /// 返工数量
    /// </summary>
    [SugarColumn(ColumnName = "FREMADEQTY")]
    public decimal Fremadeqty { get; set; }

    /// <summary>
    /// 完成数量
    /// </summary>
    [SugarColumn(ColumnName = "FFINISHQTY")]
    public decimal Ffinishqty { get; set; }

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public int Frowtype { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

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
    /// 基本单位报废数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESCRAPQTY")]
    public decimal Fbasescrapqty { get; set; }

    /// <summary>
    /// 基本单位检验选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELINSPECTQTY")]
    public decimal Fbaseselinspectqty { get; set; }

    /// <summary>
    /// 检验选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FSELINSPECTQTY")]
    public decimal Fselinspectqty { get; set; }

    /// <summary>
    /// 基本单位报废品入库选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESELSCRAPINQTY")]
    public decimal Fbaseselscrapinqty { get; set; }

    /// <summary>
    /// 报废品入库选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FSELSCRAPINQTY")]
    public decimal Fselscrapinqty { get; set; }

    /// <summary>
    /// 基本单位检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEINSPECTQTY")]
    public decimal Fbaseinspectqty { get; set; }

    /// <summary>
    /// 检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTQTY")]
    public decimal Finspectqty { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 时间单位
    /// </summary>
    [SugarColumn(ColumnName = "FTIMEUNITID")]
    public int Ftimeunitid { get; set; }

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ")]
    public int Fmoentryseq { get; set; }

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID")]
    public string Fsrcdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public int Fsrcentryid { get; set; }

    /// <summary>
    /// 生产订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public string Fmoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位完成数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEFINISHQTY")]
    public decimal Fbasefinishqty { get; set; }

    /// <summary>
    /// 基本单位合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEQUAQTY")]
    public decimal Fbasequaqty { get; set; }

    /// <summary>
    /// 基本单位不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEFAILQTY")]
    public decimal Fbasefailqty { get; set; }

    /// <summary>
    /// 基本单位待返修数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEREWORKQTY")]
    public decimal Fbasereworkqty { get; set; }

    /// <summary>
    /// 基本单位合格品入库选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTOCKINQUASELQTY")]
    public decimal Fbasestockinquaselqty { get; set; }

    /// <summary>
    /// 基本单位不合格品入库选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTOCKINFAILSELQTY")]
    public decimal Fbasestockinfailselqty { get; set; }

    /// <summary>
    /// 入库组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKINORGID")]
    public string Fstockinorgid { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }

    /// <summary>
    /// 基本单位返工数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEREMADEQTY")]
    public decimal Fbaseremadeqty { get; set; }

    /// <summary>
    /// 基本单位返工入库选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTOCKINREMADESELQTY")]
    public decimal Fbasestockinremadeselqty { get; set; }

    /// <summary>
    /// 返工品入库选单数
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKINREMADESELQTY")]
    public decimal Fstockinremadeselqty { get; set; }

    /// <summary>
    /// 合格品入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKINSELQTY")]
    public decimal Fstockinselqty { get; set; }

    /// <summary>
    /// 基本单位合格品入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASESTOCKINSELQTY")]
    public decimal Fbasestockinselqty { get; set; }

    /// <summary>
    /// 领料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKMTRLSELQTY")]
    public decimal Fpickmtrlselqty { get; set; }

    /// <summary>
    /// 基本单位领料选单数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEPICKMTRLSELQTY")]
    public decimal Fbasepickmtrlselqty { get; set; }

    /// <summary>
    /// 生产订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

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
