using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 受托加工收料通知单明细
/// </summary>
[SugarTable("T_STK_OEMRECEIVEENTRY")]
public class TStkOemreceiveentry : BaseEntity
{
    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 规格型号
    /// </summary>
    [SugarColumn(ColumnName = "MODEL")]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

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
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FEXPIRYDATE")]
    public DateTime? Fexpirydate { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 项目编号
    /// </summary>
    [SugarColumn(ColumnName = "FPROJECTNO")]
    public string Fprojectno { get; set; } = string.Empty;

    /// <summary>
    /// 源单分录内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public string Fsrcentryid { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 实到数量
    /// </summary>
    [SugarColumn(ColumnName = "FACTLANDQTY")]
    public decimal Factlandqty { get; set; }

    /// <summary>
    /// 实收数量
    /// </summary>
    [SugarColumn(ColumnName = "FACTRECEIVEQTY")]
    public decimal Factreceiveqty { get; set; }

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
    /// 辅单位
    /// </summary>
    [SugarColumn(ColumnName = "FEXTAUXUNITID")]
    public string Fextauxunitid { get; set; } = string.Empty;

    /// <summary>
    /// 实到数量（辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FACTLANDSECQTY")]
    public decimal Factlandsecqty { get; set; }

    /// <summary>
    /// 实收数量（辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FEXTAUXUNITQTY")]
    public decimal Fextauxunitqty { get; set; }

    /// <summary>
    /// 拒收数量（辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTSECQTY")]
    public decimal Frejectsecqty { get; set; }

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
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID")]
    public string Fkeepertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID")]
    public string Fkeeperid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 实到数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FACTLANDBASEQTY")]
    public decimal Factlandbaseqty { get; set; }

    /// <summary>
    /// 实收数量(基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEQTY")]
    public decimal Fbaseqty { get; set; }

    /// <summary>
    /// 合格入库关联数量（库存辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKJOINAUXQTY", IsNullable = true)]
    public decimal? FSTOCKJOINAUXQTY { get; set; }

    /// <summary>
    /// 源单分录内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEDETAILID", IsNullable = true)]
    public string FSOURCEDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 判退数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FREFUSEBASEQTY", IsNullable = true)]
    public decimal? FREFUSEBASEQTY { get; set; }

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSEQ", IsNullable = true)]
    public string FSRCSEQ { get; set; } = string.Empty;

    /// <summary>
    /// 实到数量（库存辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FACTLANDAUXQTY", IsNullable = true)]
    public decimal? FACTLANDAUXQTY { get; set; }

    /// <summary>
    /// 让步接收数量
    /// </summary>
    [SugarColumn(ColumnName = "FCSNRECEIVEQTY", IsNullable = true)]
    public decimal? FCSNRECEIVEQTY { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLTYPEID", IsNullable = true)]
    public string FSRCBILLTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 不合格入库关联数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FREFUSEJOINBASEQTY", IsNullable = true)]
    public decimal? FREFUSEJOINBASEQTY { get; set; }

    /// <summary>
    /// 不合格入库关联数量（库存辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FREFUSEJOINAUXQTY", IsNullable = true)]
    public decimal? FREFUSEJOINAUXQTY { get; set; }

    /// <summary>
    /// 库存辅单位
    /// </summary>
    [SugarColumn(ColumnName = "FAUXUNITID", IsNullable = true)]
    public string FAUXUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 业务终止
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSEND", IsNullable = true)]
    public string FBUSINESSEND { get; set; } = string.Empty;

    /// <summary>
    /// 检验关联数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKJOINBASEQTY", IsNullable = true)]
    public decimal? FCHECKJOINBASEQTY { get; set; }

    /// <summary>
    /// 合格数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEBASEQTY", IsNullable = true)]
    public decimal? FRECEIVEBASEQTY { get; set; }

    /// <summary>
    /// 让步接收数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FCSNRECEIVEBASEQTY", IsNullable = true)]
    public decimal? FCSNRECEIVEBASEQTY { get; set; }

    /// <summary>
    /// 源单名称
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID", IsNullable = true)]
    public string FSRCFORMID { get; set; } = string.Empty;

    /// <summary>
    /// 终止日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDARE", IsNullable = true)]
    public DateTime? FENDDARE { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 样品破坏数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEDAMAGEBASEQTY", IsNullable = true)]
    public decimal? FSAMPLEDAMAGEBASEQTY { get; set; }

    /// <summary>
    /// 判退数量
    /// </summary>
    [SugarColumn(ColumnName = "FREFUSEQTY", IsNullable = true)]
    public decimal? FREFUSEQTY { get; set; }

    /// <summary>
    /// 拒收数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTBASEQTY", IsNullable = true)]
    public decimal? FREJECTBASEQTY { get; set; }

    /// <summary>
    /// 让步接收入库关联数量（库存辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FCSNRECEIVEJOINAUXQTY", IsNullable = true)]
    public decimal? FCSNRECEIVEJOINAUXQTY { get; set; }

    /// <summary>
    /// 检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKQTY", IsNullable = true)]
    public decimal? FCHECKQTY { get; set; }

    /// <summary>
    /// 退料关联数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNSTKJNBASEQTY", IsNullable = true)]
    public decimal? FRETURNSTKJNBASEQTY { get; set; }

    /// <summary>
    /// 实收数量(库存辅单位)
    /// </summary>
    [SugarColumn(ColumnName = "FAUXUNITQTY", IsNullable = true)]
    public decimal? FAUXUNITQTY { get; set; }

    /// <summary>
    /// 样品破坏数量
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEDAMAGEQTY", IsNullable = true)]
    public decimal? FSAMPLEDAMAGEQTY { get; set; }

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEQTY", IsNullable = true)]
    public decimal? FRECEIVEQTY { get; set; }

    /// <summary>
    /// 终止人
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSENDERID", IsNullable = true)]
    public string FBUSINESSENDERID { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID", IsNullable = true)]
    public string FSOURCEINTERID { get; set; } = string.Empty;

    /// <summary>
    /// 退料关联数量（库存辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNSTKJNAUXQTY", IsNullable = true)]
    public decimal? FRETURNSTKJNAUXQTY { get; set; }

    /// <summary>
    /// 让步接收入库关联数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FCSNRECEIVEJOINBASEQTY", IsNullable = true)]
    public decimal? FCSNRECEIVEJOINBASEQTY { get; set; }

    /// <summary>
    /// 检验数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKBASEQTY", IsNullable = true)]
    public decimal? FCHECKBASEQTY { get; set; }

    /// <summary>
    /// 需要检验
    /// </summary>
    [SugarColumn(ColumnName = "FNEEDCHECK", IsNullable = true)]
    public bool? FNEEDCHECK { get; set; }

    /// <summary>
    /// 检验关联数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKJOINQTY", IsNullable = true)]
    public decimal? FCHECKJOINQTY { get; set; }

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO", IsNullable = true)]
    public string FSRCBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 拒收数量（库存辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTAUXQTY", IsNullable = true)]
    public decimal? FREJECTAUXQTY { get; set; }

    /// <summary>
    /// 合格入库关联数量（基本单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKJOINBASEQTY", IsNullable = true)]
    public decimal? FSTOCKJOINBASEQTY { get; set; }

    /// <summary>
    /// BOM展开需求序号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSEQNO", IsNullable = true)]
    public string FSRCSEQNO { get; set; } = string.Empty;
}
