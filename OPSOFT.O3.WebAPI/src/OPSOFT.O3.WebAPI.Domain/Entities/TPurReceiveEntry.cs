using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 收料通知单明细
/// </summary>
[SugarTable("T_PUR_RECEIVEENTRY")]
public class TPurReceiveEntry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 交货数量
    /// </summary>
    [SugarColumn(ColumnName = "FACTRECEIVEQTY")]
    public decimal Factreceiveqty { get; set; }

    /// <summary>
    /// 累计入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKQTY")]
    public decimal Finstockqty { get; set; }

    /// <summary>
    /// 累计入库数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKBASEQTY")]
    public decimal Finstockbaseqty { get; set; }

    /// <summary>
    /// 预计到货日期
    /// </summary>
    [SugarColumn(ColumnName = "FPREDELIVERYDATE")]
    public DateTime? Fpredeliverydate { get; set; }

    /// <summary>
    /// 供应商交货数量
    /// </summary>
    [SugarColumn(ColumnName = "FSUPDELQTY")]
    public decimal Fsupdelqty { get; set; }

    /// <summary>
    /// 紧急放行
    /// </summary>
    [SugarColumn(ColumnName = "FDISCHARGED")]
    public int Fdischarged { get; set; }

    /// <summary>
    /// 检验方式
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKMETHOD")]
    public int Fcheckmethod { get; set; }

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
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 拒收数量
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTQTY")]
    public decimal Frejectqty { get; set; }

    /// <summary>
    /// 拒收原因
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTREASON")]
    public string Frejectreason { get; set; } = string.Empty;

    /// <summary>
    /// 是否赠品
    /// </summary>
    [SugarColumn(ColumnName = "FGIVEAWAY")]
    public bool Fgiveaway { get; set; }

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
    /// 退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNQTY")]
    public decimal Freturnqty { get; set; }

    /// <summary>
    /// 退料基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNBASEQTY")]
    public decimal Freturnbaseqty { get; set; }

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
    /// 折扣额
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNT")]
    public decimal Fdiscount { get; set; }

    /// <summary>
    /// 折扣率%
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNTRATE")]
    public decimal Fdiscountrate { get; set; }

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
    /// ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID")]
    public string Ferpentryid { get; set; } = string.Empty;

    /// <summary>
    /// 采购申请备注
    /// </summary>
    [SugarColumn(ColumnName = "FREPNOTE")]
    public string Frepnote { get; set; } = string.Empty;

    /// <summary>
    /// 采购申请类型
    /// </summary>
    [SugarColumn(ColumnName = "FCGSQTYPE")]
    public string Fcgsqtype { get; set; } = string.Empty;

    /// <summary>
    /// 研发项目
    /// </summary>
    [SugarColumn(ColumnName = "FYANFAPROJECT")]
    public string Fyanfaproject { get; set; } = string.Empty;

    /// <summary>
    /// 研发子项目
    /// </summary>
    [SugarColumn(ColumnName = "FYFZXM")]
    public string Fyfzxm { get; set; } = string.Empty;

    /// <summary>
    /// 使用部门
    /// </summary>
    [SugarColumn(ColumnName = "FUSEDEPT")]
    public string Fusedept { get; set; } = string.Empty;

    /// <summary>
    /// 拟收仓库
    /// </summary>
    [SugarColumn(ColumnName = "FHGNISHOUSTOCK")]
    public string Fhgnishoustock { get; set; } = string.Empty;

    /// <summary>
    /// 拟收仓位
    /// </summary>
    [SugarColumn(ColumnName = "FHGNISHOUSTOCKPLACE")]
    public string Fhgnishoustockplace { get; set; } = string.Empty;

    /// <summary>
    /// 计划模式
    /// </summary>
    [SugarColumn(ColumnName = "FPLANMODE")]
    public string Fplanmode { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 收料单位
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVUNITID")]
    public string Freceivunitid { get; set; } = string.Empty;

    /// <summary>
    /// 计价单位
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEUNITID")]
    public string Fpriceunitid { get; set; } = string.Empty;

    /// <summary>
    /// 来料检验
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKINCOMING")]
    public bool Fcheckincoming { get; set; }

    /// <summary>
    /// 检验合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FGODQTY")]
    public decimal Fgodqty { get; set; }

    /// <summary>
    /// 检验合格基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FGODBASEQTY")]
    public decimal Fgodbaseqty { get; set; }

    /// <summary>
    /// 样本破坏数
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQTY")]
    public decimal Fscrapqty { get; set; }

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
    /// 供应商批号
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYLOT")]
    public string Fsupplylot { get; set; } = string.Empty;

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FEXPIREDATE")]
    public DateTime? Fexpiredate { get; set; }

    /// <summary>
    /// 需求部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEMANDDEPTID")]
    public string Fdemanddeptid { get; set; } = string.Empty;

    /// <summary>
    /// 需求人
    /// </summary>
    [SugarColumn(ColumnName = "FDEMANDERID")]
    public string Fdemanderid { get; set; } = string.Empty;

    /// <summary>
    /// 随附文件送达
    /// </summary>
    [SugarColumn(ColumnName = "FSFWJSD")]
    public bool Fsfwjsd { get; set; }

    /// <summary>
    /// 送货单送达
    /// </summary>
    [SugarColumn(ColumnName = "FSHDSD")]
    public bool Fshdsd { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }
}
