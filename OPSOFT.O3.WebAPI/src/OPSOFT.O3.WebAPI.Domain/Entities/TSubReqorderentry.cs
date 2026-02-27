using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外订单明细
/// </summary>
[SugarTable("T_SUB_REQORDERENTRY")]
public class TSubReqorderentry : BaseEntity
{
    /// <summary>
    /// 常用退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXBCOMMITQTY")]
    public decimal Fauxbcommitqty { get; set; }

    /// <summary>
    /// 关联数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXCOMMITQTY")]
    public decimal Fauxcommitqty { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 开票数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTYINVOICE")]
    public decimal Fauxqtyinvoice { get; set; }

    /// <summary>
    /// 基本退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBCOMMITQTY")]
    public decimal Fbcommitqty { get; set; }

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// Bom编号内码
    /// </summary>
    [SugarColumn(ColumnName = "FBOMINTERID")]
    public string Fbominterid { get; set; } = string.Empty;

    /// <summary>
    /// 检验方式
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKMETHOD")]
    public string Fcheckmethod { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 基本关联数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMITQTY")]
    public decimal Fcommitqty { get; set; }

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public int Fsrcentryid { get; set; }

    /// <summary>
    /// 采购订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERENTRYID")]
    public int Forderentryid { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FFETCHDATE")]
    public DateTime? Ffetchdate { get; set; }

    /// <summary>
    /// 顺序号
    /// </summary>
    [SugarColumn(ColumnName = "FINDEX")]
    public int Findex { get; set; }

    /// <summary>
    /// 完工超收比例(%
    /// </summary>
    [SugarColumn(ColumnName = "FINHIGHLIMIT")]
    public decimal Finhighlimit { get; set; }

    /// <summary>
    /// 驳回拒绝意见
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTREFUSENOTE", IsNullable = true)]
    public string FREJECTREFUSENOTE { get; set; } = string.Empty;

    /// <summary>
    /// 供应商确认日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUPCONDATE", IsNullable = true)]
    public DateTime? FSUPCONDATE { get; set; }

    /// <summary>
    /// 税率%
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE", IsNullable = true)]
    public decimal? FTAXRATE { get; set; }

    /// <summary>
    /// 折扣率%
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNTRATE", IsNullable = true)]
    public decimal? FDISCOUNTRATE { get; set; }

    /// <summary>
    /// 行业务关闭标志
    /// </summary>
    [SugarColumn(ColumnName = "FMRPCLOSED", IsNullable = true)]
    public bool? FMRPCLOSED { get; set; }

    /// <summary>
    /// 计划完工时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFINISHDATE", IsNullable = true)]
    public DateTime? FPLANFINISHDATE { get; set; }

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTTYPE", IsNullable = true)]
    public string FPRODUCTTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 换算率
    /// </summary>
    [SugarColumn(ColumnName = "FCOEFFICIENT", IsNullable = true)]
    public decimal? FCOEFFICIENT { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 供应商确认标志
    /// </summary>
    [SugarColumn(ColumnName = "FSUPCONFIRM", IsNullable = true)]
    public string FSUPCONFIRM { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 计划模式
    /// </summary>
    [SugarColumn(ColumnName = "FPLANMODE", IsNullable = true)]
    public string FPLANMODE { get; set; } = string.Empty;

    /// <summary>
    /// 付款关联金额
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEAMOUNTFOR", IsNullable = true)]
    public decimal? FRECEIVEAMOUNTFOR { get; set; }

    /// <summary>
    /// 完工欠收比例(%)
    /// </summary>
    [SugarColumn(ColumnName = "FINLOWLIMIT", IsNullable = true)]
    public decimal? FINLOWLIMIT { get; set; }

    /// <summary>
    /// 关联辅助数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECCOMMITQTY", IsNullable = true)]
    public decimal? FSECCOMMITQTY { get; set; }

    /// <summary>
    /// 确认意见
    /// </summary>
    [SugarColumn(ColumnName = "FSUPCONMEM", IsNullable = true)]
    public string FSUPCONMEM { get; set; } = string.Empty;

    /// <summary>
    /// 确认交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUPCONFETCHDATE", IsNullable = true)]
    public DateTime? FSUPCONFETCHDATE { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 严格跟单标志
    /// </summary>
    [SugarColumn(ColumnName = "FMRPTRACKFLAG", IsNullable = true)]
    public bool? FMRPTRACKFLAG { get; set; }

    /// <summary>
    /// 是否手工行业务关闭
    /// </summary>
    [SugarColumn(ColumnName = "FMRPAUTOCLOSED", IsNullable = true)]
    public bool? FMRPAUTOCLOSED { get; set; }

    /// <summary>
    /// 对应名称
    /// </summary>
    [SugarColumn(ColumnName = "FMAPNAME", IsNullable = true)]
    public string FMAPNAME { get; set; } = string.Empty;

    /// <summary>
    /// 供应商确认人
    /// </summary>
    [SugarColumn(ColumnName = "FSUPCONFIRMOR", IsNullable = true)]
    public string FSUPCONFIRMOR { get; set; } = string.Empty;

    /// <summary>
    /// 基本开票数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTYINVOICE", IsNullable = true)]
    public decimal? FQTYINVOICE { get; set; }

    /// <summary>
    /// 预测单号
    /// </summary>
    [SugarColumn(ColumnName = "FPORNUMBER", IsNullable = true)]
    public string FPORNUMBER { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID", IsNullable = true)]
    public string FMATERIALID { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID", IsNullable = true)]
    public string FSRCID { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 锁单标志:
    /// </summary>
    [SugarColumn(ColumnName = "FMRPLOCKFLAG", IsNullable = true)]
    public bool? FMRPLOCKFLAG { get; set; }

    /// <summary>
    /// 入库基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKBASEQTY", IsNullable = true)]
    public decimal? FSTOCKBASEQTY { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID", IsNullable = true)]
    public string FSUPPLYID { get; set; } = string.Empty;

    /// <summary>
    /// 建议发料日期
    /// </summary>
    [SugarColumn(ColumnName = "FPAYSHIPDATE", IsNullable = true)]
    public DateTime? FPAYSHIPDATE { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERINTERID", IsNullable = true)]
    public string FORDERINTERID { get; set; } = string.Empty;

    /// <summary>
    /// 预测单内码
    /// </summary>
    [SugarColumn(ColumnName = "FPORINTERID", IsNullable = true)]
    public string FPORINTERID { get; set; } = string.Empty;

    /// <summary>
    /// 计划开工时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE", IsNullable = true)]
    public DateTime? FPLANSTARTDATE { get; set; }

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY", IsNullable = true)]
    public decimal? FBASEUNITQTY { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID", IsNullable = true)]
    public string FBASEUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERDETAILID", IsNullable = true)]
    public string FORDERDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 入库上限数量
    /// </summary>
    [SugarColumn(ColumnName = "FINHIGHLIMITQTY", IsNullable = true)]
    public decimal? FINHIGHLIMITQTY { get; set; }

    /// <summary>
    /// 确认数量
    /// </summary>
    [SugarColumn(ColumnName = "FSUPCONQTY", IsNullable = true)]
    public decimal? FSUPCONQTY { get; set; }

    /// <summary>
    /// 源单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID", IsNullable = true)]
    public string FSRCDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 对应代码
    /// </summary>
    [SugarColumn(ColumnName = "FMAPID", IsNullable = true)]
    public string FMAPID { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO", IsNullable = true)]
    public string FMTONO { get; set; } = string.Empty;

    /// <summary>
    /// 辅助开票数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECQTYINVOICE", IsNullable = true)]
    public decimal? FSECQTYINVOICE { get; set; }

    /// <summary>
    /// 入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY", IsNullable = true)]
    public decimal? FSTOCKQTY { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT", IsNullable = true)]
    public string FLOT { get; set; } = string.Empty;

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE", IsNullable = true)]
    public decimal? FTAXPRICE { get; set; }

    /// <summary>
    /// 拒绝意见
    /// </summary>
    [SugarColumn(ColumnName = "FREFUSENOTE", IsNullable = true)]
    public string FREFUSENOTE { get; set; } = string.Empty;

    /// <summary>
    /// 辅助数量/基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECQTY", IsNullable = true)]
    public decimal? FSECQTY { get; set; }

    /// <summary>
    /// 入库下限数量
    /// </summary>
    [SugarColumn(ColumnName = "FINLOWLIMITQTY", IsNullable = true)]
    public decimal? FINLOWLIMITQTY { get; set; }

    /// <summary>
    /// 采购订单号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERNO", IsNullable = true)]
    public string FORDERNO { get; set; } = string.Empty;
}
