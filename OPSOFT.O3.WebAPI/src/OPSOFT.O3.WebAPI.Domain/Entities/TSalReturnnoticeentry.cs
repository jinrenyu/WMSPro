using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 退货通知单明细
/// </summary>
[SugarTable("T_SAL_RETURNNOTICEENTRY")]
public class TSalReturnnoticeentry : BaseEntity
{
    /// <summary>
    /// 客户物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMAPID")]
    public string Fmapid { get; set; } = string.Empty;

    /// <summary>
    /// 客户物料名称
    /// </summary>
    [SugarColumn(ColumnName = "FMAPNAME")]
    public string Fmapname { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMONUNITID")]
    public string Fcommonunitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY")]
    public decimal Fstockqty { get; set; }

    /// <summary>
    /// 基础库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKBASEQTY")]
    public decimal Fstockbaseqty { get; set; }

    /// <summary>
    /// 批次文体
    /// </summary>
    [SugarColumn(ColumnName = "FLOT_TEXT")]
    public string FlotText { get; set; } = string.Empty;

    /// <summary>
    /// 出货仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 计价数量
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEUNITQTY")]
    public decimal Fpriceunitqty { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE")]
    public decimal Ftaxprice { get; set; }

    /// <summary>
    /// 是否赠品
    /// </summary>
    [SugarColumn(ColumnName = "FISFREE")]
    public bool Fisfree { get; set; }

    /// <summary>
    /// 税率%
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT")]
    public decimal Ftaxamount { get; set; }

    /// <summary>
    /// 税额(本位币
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT_LC")]
    public decimal FtaxamountLc { get; set; }

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FFETCHADD", IsNullable = true)]
    public string FFETCHADD { get; set; } = string.Empty;

    /// <summary>
    /// 退货数量
    /// </summary>
    [SugarColumn(ColumnName = "FBCOMMITQTY", IsNullable = true)]
    public decimal? FBCOMMITQTY { get; set; }

    /// <summary>
    /// 折扣额
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNT", IsNullable = true)]
    public decimal? FDISCOUNT { get; set; }

    /// <summary>
    /// 折扣率%
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNTRATE", IsNullable = true)]
    public decimal? FDISCOUNTRATE { get; set; }

    /// <summary>
    /// 订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERBILLNO", IsNullable = true)]
    public string FORDERBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 退货检验
    /// </summary>
    [SugarColumn(ColumnName = "FISRETURNCHECK", IsNullable = true)]
    public bool? FISRETURNCHECK { get; set; }

    /// <summary>
    /// 出货仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 订单明细行号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERENTRYID", IsNullable = true)]
    public int? FORDERENTRYID { get; set; }

    /// <summary>
    /// 报废判退数量
    /// </summary>
    [SugarColumn(ColumnName = "FJUNKEDQTY", IsNullable = true)]
    public decimal? FJUNKEDQTY { get; set; }

    /// <summary>
    /// 价税合计
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT", IsNullable = true)]
    public decimal? FALLAMOUNT { get; set; }

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID", IsNullable = true)]
    public string FSRCFORMID { get; set; } = string.Empty;

    /// <summary>
    /// 退货类型
    /// </summary>
    [SugarColumn(ColumnName = "FRMTYPE", IsNullable = true)]
    public string FRMTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 不合格判退数量
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUALIFIEDQTY", IsNullable = true)]
    public decimal? FUNQUALIFIEDQTY { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT", IsNullable = true)]
    public decimal? FAMOUNT { get; set; }

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID", IsNullable = true)]
    public string FBOMID { get; set; } = string.Empty;

    /// <summary>
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FFETCHDATE", IsNullable = true)]
    public DateTime? FFETCHDATE { get; set; }

    /// <summary>
    /// 套件行内码
    /// </summary>
    [SugarColumn(ColumnName = "FROWID", IsNullable = true)]
    public string FROWID { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID", IsNullable = true)]
    public string FSRCID { get; set; } = string.Empty;

    /// <summary>
    /// 退货日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE", IsNullable = true)]
    public DateTime? FDELIVERYDATE { get; set; }

    /// <summary>
    /// 销售单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID", IsNullable = true)]
    public int? FSRCENTRYID { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERINTERID", IsNullable = true)]
    public string FORDERINTERID { get; set; } = string.Empty;

    /// <summary>
    /// 价税合计(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT_LC", IsNullable = true)]
    public decimal? FALLAMOUNT_LC { get; set; }

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY", IsNullable = true)]
    public decimal? FBASEUNITQTY { get; set; }

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE", IsNullable = true)]
    public string FROWTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 已检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKQTY", IsNullable = true)]
    public decimal? FCHECKQTY { get; set; }

    /// <summary>
    /// 订单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERDETAILID", IsNullable = true)]
    public string FORDERDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 金额(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT_LC", IsNullable = true)]
    public decimal? FAMOUNT_LC { get; set; }

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID", IsNullable = true)]
    public string FSRCDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 检验合格基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FGOODBASEQTY", IsNullable = true)]
    public decimal? FGOODBASEQTY { get; set; }

    /// <summary>
    /// 退货数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FBCOMMITBASEQTY", IsNullable = true)]
    public decimal? FBCOMMITBASEQTY { get; set; }

    /// <summary>
    /// 合格判退数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUALIFIEDQTY", IsNullable = true)]
    public decimal? FQUALIFIEDQTY { get; set; }

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO", IsNullable = true)]
    public string FSRCBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 出库数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXSTOCKQTY", IsNullable = true)]
    public decimal? FAUXSTOCKQTY { get; set; }

    /// <summary>
    /// 订单类型
    /// </summary>
    [SugarColumn(ColumnName = "FORDERTYPEID", IsNullable = true)]
    public string FORDERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 父项产品
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTMATID", IsNullable = true)]
    public string FPARENTMATID { get; set; } = string.Empty;
}
