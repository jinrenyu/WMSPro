using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购入库单表体-物料明细
/// </summary>
[SugarTable("T_STK_INSTOCKENTRY")]
public class TStkInstockentry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 入库类型
    /// </summary>
    [SugarColumn(ColumnName = "FWWINTYPE")]
    public string Fwwintype { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

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
    /// 源单编号
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
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

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
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商批号
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLIERLOT")]
    public string Fsupplierlot { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

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
    /// 折扣率%
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNTRATE")]
    public decimal Fdiscountrate { get; set; }

    /// <summary>
    /// 折扣额
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNT")]
    public decimal Fdiscount { get; set; }

    /// <summary>
    /// 税率%
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE")]
    public decimal Ftaxprice { get; set; }

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
    /// 使用部门
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_BASE", IsNullable = true)]
    public string F_QQQQ_BASE { get; set; } = string.Empty;

    /// <summary>
    /// 未关联应付金额（含税本币）
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_AMOUNT4", IsNullable = true)]
    public string F_QQQQ_AMOUNT4 { get; set; } = string.Empty;

    /// <summary>
    /// 保管者内码
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID", IsNullable = true)]
    public string FKEEPERID { get; set; } = string.Empty;

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
    /// 采购申请类型
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_ASSISTANT2", IsNullable = true)]
    public string F_QQQQ_ASSISTANT2 { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY", IsNullable = true)]
    public decimal? FSECUNITQTY { get; set; }

    /// <summary>
    /// 是否赠品
    /// </summary>
    [SugarColumn(ColumnName = "FGIVEAWAY", IsNullable = true)]
    public bool? FGIVEAWAY { get; set; }

    /// <summary>
    /// 未关联应付金额（含税原币）
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_AMOUNT3", IsNullable = true)]
    public string F_QQQQ_AMOUNT3 { get; set; } = string.Empty;

    /// <summary>
    /// ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID", IsNullable = true)]
    public string FERPENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 研发项目
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_ASSISTANT", IsNullable = true)]
    public string F_QQQQ_ASSISTANT { get; set; } = string.Empty;

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
    /// 价税合计
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT", IsNullable = true)]
    public decimal? FALLAMOUNT { get; set; }

    /// <summary>
    /// 未关联应付金额（未税本币）
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_AMOUNT6", IsNullable = true)]
    public string F_QQQQ_AMOUNT6 { get; set; } = string.Empty;

    /// <summary>
    /// 研发子项目
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_ASSISTANT1", IsNullable = true)]
    public string F_QQQQ_ASSISTANT1 { get; set; } = string.Empty;

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT", IsNullable = true)]
    public decimal? FTAXAMOUNT { get; set; }

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
    /// 订单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERDETAILID", IsNullable = true)]
    public string FORDERDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID", IsNullable = true)]
    public string FSTOCKSTATUSID { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID", IsNullable = true)]
    public string FOWNERID { get; set; } = string.Empty;

    /// <summary>
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID", IsNullable = true)]
    public string FKEEPERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID", IsNullable = true)]
    public string FSECUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 未关联应付金额（未税原币）
    /// </summary>
    [SugarColumn(ColumnName = "F_QQQQ_AMOUNT5", IsNullable = true)]
    public string F_QQQQ_AMOUNT5 { get; set; } = string.Empty;

    /// <summary>
    /// 订单类型
    /// </summary>
    [SugarColumn(ColumnName = "FORDERTYPEID", IsNullable = true)]
    public string FORDERTYPEID { get; set; } = string.Empty;
}
