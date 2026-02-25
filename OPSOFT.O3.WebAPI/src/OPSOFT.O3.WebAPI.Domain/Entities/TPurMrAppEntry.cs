using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 退料通知单明细
/// </summary>
[SugarTable("T_PUR_MRAPPENTRY")]
public class TPurMrAppEntry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 物料
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
    /// 申请退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FMRAPPQTY")]
    public decimal Fmrappqty { get; set; }

    /// <summary>
    /// 补料数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPLENISHQTY")]
    public decimal Freplenishqty { get; set; }

    /// <summary>
    /// 扣款数量
    /// </summary>
    [SugarColumn(ColumnName = "FKEAPAMTQTY")]
    public decimal Fkeapamtqty { get; set; }

    /// <summary>
    /// 退料原因
    /// </summary>
    [SugarColumn(ColumnName = "FRMREASON")]
    public string Frmreason { get; set; } = string.Empty;

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
    /// 退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FMRQTY")]
    public decimal Fmrqty { get; set; }

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
    /// 退料金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 金额(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT_LC")]
    public decimal FamountLc { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT")]
    public decimal Ftaxamount { get; set; }

    /// <summary>
    /// 税额(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT_LC")]
    public decimal FtaxamountLc { get; set; }

    /// <summary>
    /// 价税合计
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT")]
    public decimal Fallamount { get; set; }

    /// <summary>
    /// 价税合计(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT_LC")]
    public decimal FallamountLc { get; set; }

    /// <summary>
    /// 是否赠品
    /// </summary>
    [SugarColumn(ColumnName = "FGIVEAWAY")]
    public bool Fgiveaway { get; set; }

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 库存单位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKUNITID")]
    public string Fstockunitid { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY")]
    public decimal Fstockqty { get; set; }

    /// <summary>
    /// 计价单位
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEUNITID")]
    public string Fpriceunitid { get; set; } = string.Empty;

    /// <summary>
    /// 采购单位
    /// </summary>
    [SugarColumn(ColumnName = "FCARRYUNITID")]
    public string Fcarryunitid { get; set; } = string.Empty;

    /// <summary>
    /// 计价数量
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEUNITQTY")]
    public decimal Fpriceunitqty { get; set; }

    /// <summary>
    /// 采购数量
    /// </summary>
    [SugarColumn(ColumnName = "FCARRYQTY")]
    public decimal Fcarryqty { get; set; }

    /// <summary>
    /// 累计退料基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FMRBBASEQTY")]
    public decimal Fmrbbaseqty { get; set; }

    /// <summary>
    /// 累计退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FMRBQTY")]
    public decimal Fmrbqty { get; set; }

    /// <summary>
    /// 订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERBILLNO")]
    public string Forderbillno { get; set; } = string.Empty;

    /// <summary>
    /// 订单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERDETAILID")]
    public string Forderdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 订单明细行号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERENTRYID")]
    public int Forderentryid { get; set; }

    /// <summary>
    /// 订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERINTERID")]
    public string Forderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者内码
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID")]
    public string Fkeeperid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID")]
    public string Fkeepertypeid { get; set; } = string.Empty;

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
}
