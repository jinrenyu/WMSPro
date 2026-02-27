using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售退货单明细
/// </summary>
[SugarTable("T_SAL_RETURNSTOCKENTRY")]
public class TSalReturnstockentry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 客户物料
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTMATID")]
    public string Fcustmatid { get; set; } = string.Empty;

    /// <summary>
    /// 零售条形码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 应退数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 实退数量
    /// </summary>
    [SugarColumn(ColumnName = "FREALQTY")]
    public decimal Frealqty { get; set; }

    /// <summary>
    /// 退货类型
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNTYPE")]
    public string Freturntype { get; set; } = string.Empty;

    /// <summary>
    /// 税率%
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

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
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCEDATE")]
    public DateTime? Fproducedate { get; set; }

    /// <summary>
    /// 拒收数量
    /// </summary>
    [SugarColumn(ColumnName = "FREFUSEQTY")]
    public decimal Frefuseqty { get; set; }

    /// <summary>
    /// 补货数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRQTY")]
    public decimal Frepairqty { get; set; }

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
    /// 金额(本位币
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT_LC")]
    public decimal FamountLc { get; set; }

    /// <summary>
    /// 保管者内码
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID", IsNullable = true)]
    public string FKEEPERID { get; set; } = string.Empty;

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
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID", IsNullable = true)]
    public string FOWNERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 税额(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT_LC", IsNullable = true)]
    public decimal? FTAXAMOUNT_LC { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY", IsNullable = true)]
    public decimal? FSECUNITQTY { get; set; }

    /// <summary>
    /// ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID", IsNullable = true)]
    public string FERPENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE", IsNullable = true)]
    public DateTime? FKFDATE { get; set; }

    /// <summary>
    /// 订单明细行号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERENTRYID", IsNullable = true)]
    public int? FORDERENTRYID { get; set; }

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
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT", IsNullable = true)]
    public decimal? FTAXAMOUNT { get; set; }

    /// <summary>
    /// 常用单位
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
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID", IsNullable = true)]
    public string FBASEUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 订单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERDETAILID", IsNullable = true)]
    public string FORDERDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 有效期至(变更后)
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE", IsNullable = true)]
    public DateTime? FUSEFULDATE { get; set; }

    /// <summary>
    /// 仓库库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID", IsNullable = true)]
    public string FSTOCKSTATUSID { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID", IsNullable = true)]
    public string FOWNERID { get; set; } = string.Empty;

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID", IsNullable = true)]
    public string FSRCDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO", IsNullable = true)]
    public string FMTONO { get; set; } = string.Empty;

    /// <summary>
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID", IsNullable = true)]
    public string FKEEPERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE", IsNullable = true)]
    public decimal? FTAXPRICE { get; set; }

    /// <summary>
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID", IsNullable = true)]
    public string FSECUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 质量类型
    /// </summary>
    [SugarColumn(ColumnName = "FQUALIFYTYPE", IsNullable = true)]
    public string FQUALIFYTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO", IsNullable = true)]
    public string FSRCBILLNO { get; set; } = string.Empty;

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
