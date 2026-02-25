using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购订单明细
/// </summary>
[SugarTable("T_PUR_POORDERENTRY")]
public class TPurPoOrderEntry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 委外产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTTYPE")]
    public string Fproducttype { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 零售条形码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 累计收料数量
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEQTY")]
    public decimal Freceiveqty { get; set; }

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

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
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 需求组织
    /// </summary>
    [SugarColumn(ColumnName = "FREQUIREORGID")]
    public string Frequireorgid { get; set; } = string.Empty;

    /// <summary>
    /// 收料组织
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEORGID")]
    public string Freceiveorgid { get; set; } = string.Empty;

    /// <summary>
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEORGID")]
    public string Fsettleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 携带的主业务单位
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBIZUNITID")]
    public decimal Fsrcbizunitid { get; set; }

    /// <summary>
    /// 库存基本分母
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKBASEDEN")]
    public decimal Fstockbaseden { get; set; }

    /// <summary>
    /// 采购基本分子
    /// </summary>
    [SugarColumn(ColumnName = "FPURBASENUM")]
    public string Fpurbasenum { get; set; } = string.Empty;

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE")]
    public decimal Ftaxprice { get; set; }

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
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 金额(本位币)
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT_LC")]
    public decimal FamountLc { get; set; }

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPMATID")]
    public string Fsupmatid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商物料名称
    /// </summary>
    [SugarColumn(ColumnName = "FSUPMATNAME")]
    public string Fsupmatname { get; set; } = string.Empty;

    /// <summary>
    /// 供应商批号
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLIERLOT")]
    public string Fsupplierlot { get; set; } = string.Empty;

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
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE")]
    public DateTime? Fdeliverydate { get; set; }

    /// <summary>
    /// 业务关闭
    /// </summary>
    [SugarColumn(ColumnName = "FMRPCLOSESTATUS")]
    public bool Fmrpclosestatus { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 累计退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FMRBQTY")]
    public decimal Fmrbqty { get; set; }

    /// <summary>
    /// 累计入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKQTY")]
    public decimal Finstockqty { get; set; }

    /// <summary>
    /// 累计收料基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEBASEQTY")]
    public decimal Freceivebaseqty { get; set; }

    /// <summary>
    /// 累计退料基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FMRBBASEQTY")]
    public decimal Fmrbbaseqty { get; set; }

    /// <summary>
    /// 累计入库基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKBASEQTY")]
    public decimal Finstockbaseqty { get; set; }
}
