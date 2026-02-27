using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售订单明细
/// </summary>
[SugarTable("T_SAL_ORDERENTRY")]
public class TSalOrderentry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

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
    /// BOM代码
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

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
    /// 行业务关闭
    /// </summary>
    [SugarColumn(ColumnName = "FMRPFREEZESTATUS")]
    public string Fmrpfreezestatus { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKUNITID")]
    public string Fstockunitid { get; set; } = string.Empty;

    /// <summary>
    /// 库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY")]
    public decimal Fstockqty { get; set; }

    /// <summary>
    /// 批次文体
    /// </summary>
    [SugarColumn(ColumnName = "FLOT_TEXT")]
    public string FlotText { get; set; } = string.Empty;

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
    /// 实际含税价格
    /// </summary>
    [SugarColumn(ColumnName = "FSECPRICEDISCOUNT", IsNullable = true)]
    public decimal? FSECPRICEDISCOUNT { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 建议交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FADVICECONSIGNDATE", IsNullable = true)]
    public DateTime? FADVICECONSIGNDATE { get; set; }

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
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT", IsNullable = true)]
    public decimal? FAMOUNT { get; set; }

    /// <summary>
    /// 累计退货数量（销售）
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNQTY", IsNullable = true)]
    public decimal? FRETURNQTY { get; set; }

    /// <summary>
    /// 套件行内码
    /// </summary>
    [SugarColumn(ColumnName = "FROWID", IsNullable = true)]
    public string FROWID { get; set; } = string.Empty;

    /// <summary>
    /// 要货日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE", IsNullable = true)]
    public DateTime? FDELIVERYDATE { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

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
    /// 累计退货基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNBASEQTY", IsNullable = true)]
    public decimal? FRETURNBASEQTY { get; set; }

    /// <summary>
    /// 累计出库基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKOUTBASEQTY", IsNullable = true)]
    public decimal? FSTOCKOUTBASEQTY { get; set; }

    /// <summary>
    /// 金额(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT_LC", IsNullable = true)]
    public decimal? FAMOUNT_LC { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FCONSIGNDATE", IsNullable = true)]
    public DateTime? FCONSIGNDATE { get; set; }

    /// <summary>
    /// 库存组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKORGID", IsNullable = true)]
    public string FSTOCKORGID { get; set; } = string.Empty;

    /// <summary>
    /// 累计出库数量（销售）
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKOUTQTY", IsNullable = true)]
    public decimal? FSTOCKOUTQTY { get; set; }

    /// <summary>
    /// 父项产品
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTMATID", IsNullable = true)]
    public string FPARENTMATID { get; set; } = string.Empty;
}
