using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 发货通知单明细
/// </summary>
[SugarTable("T_SAL_DELIVERYNOTICEENTRY")]
public class TSalDeliverynoticeentry : BaseEntity
{
    /// <summary>
    /// 物料编码
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
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 发货数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 零售条形码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

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
    /// 出货仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 出货仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 计价单位
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEUNITID")]
    public string Fpriceunitid { get; set; } = string.Empty;

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
    /// 税额(本位币
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT_LC")]
    public decimal FtaxamountLc { get; set; }

    /// <summary>
    /// 备货仓库
    /// </summary>
    [SugarColumn(ColumnName = "FBACKUPSTOCKID", IsNullable = true)]
    public string FBACKUPSTOCKID { get; set; } = string.Empty;

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
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCEDATE", IsNullable = true)]
    public DateTime? FPRODUCEDATE { get; set; }

    /// <summary>
    /// 检验不合格数
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY", IsNullable = true)]
    public decimal? FBADQTY { get; set; }

    /// <summary>
    /// 累计出库数量
    /// </summary>
    [SugarColumn(ColumnName = "FSUMOUTQTY", IsNullable = true)]
    public decimal? FSUMOUTQTY { get; set; }

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
    /// 发货检验
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKSENDING", IsNullable = true)]
    public bool? FCHECKSENDING { get; set; }

    /// <summary>
    /// 价税合计
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT", IsNullable = true)]
    public decimal? FALLAMOUNT { get; set; }

    /// <summary>
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEORGID", IsNullable = true)]
    public string FSETTLEORGID { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID", IsNullable = true)]
    public string FSRCFORMID { get; set; } = string.Empty;

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT", IsNullable = true)]
    public decimal? FAMOUNT { get; set; }

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
    /// 要货日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE", IsNullable = true)]
    public DateTime? FDELIVERYDATE { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT", IsNullable = true)]
    public decimal? FTAXAMOUNT { get; set; }

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID", IsNullable = true)]
    public int? FSRCENTRYID { get; set; }

    /// <summary>
    /// 检验合格数
    /// </summary>
    [SugarColumn(ColumnName = "FGOODQTY", IsNullable = true)]
    public decimal? FGOODQTY { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 备货仓位
    /// </summary>
    [SugarColumn(ColumnName = "FBACKUPSTOCKLOCID", IsNullable = true)]
    public string FBACKUPSTOCKLOCID { get; set; } = string.Empty;

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
    /// 累计出库数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSUMOUTBASEQTY", IsNullable = true)]
    public decimal? FSUMOUTBASEQTY { get; set; }

    /// <summary>
    /// 库存组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKORGID", IsNullable = true)]
    public string FSTOCKORGID { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO", IsNullable = true)]
    public string FSRCBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 样本破坏数
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQTY", IsNullable = true)]
    public decimal? FSCRAPQTY { get; set; }

    /// <summary>
    /// 父项产品
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTMATID", IsNullable = true)]
    public string FPARENTMATID { get; set; } = string.Empty;
}
