using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 调拨单明细
/// </summary>
[SugarTable("T_STK_STKTRANSFERINENTRY")]
public class TStkStktransferinentry : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 产品类别
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

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
    /// 数值
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

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
    /// 调出仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSTOCKID")]
    public string Fsrcstockid { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSTOCKLOCID")]
    public string Fsrcstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 调出库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSTOCKSTATUSID")]
    public string Fsrcstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 质量类型
    /// </summary>
    [SugarColumn(ColumnName = "FDELICHKQUALIFYTYPE")]
    public string Fdelichkqualifytype { get; set; } = string.Empty;

    /// <summary>
    /// 调入库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 调出货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEOUTID")]
    public string Fownertypeoutid { get; set; } = string.Empty;

    /// <summary>
    /// 调出货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNEROUTID")]
    public string Fowneroutid { get; set; } = string.Empty;

    /// <summary>
    /// 调入货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 调入货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 备用栏位
    /// </summary>
    [SugarColumn(ColumnName = "FBCTYPE")]
    public string Fbctype { get; set; } = string.Empty;

    /// <summary>
    /// ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID")]
    public string Ferpentryid { get; set; } = string.Empty;

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
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 调入仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 调入仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERINTERID")]
    public string Forderinterid { get; set; } = string.Empty;

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
    /// 订单类型
    /// </summary>
    [SugarColumn(ColumnName = "FORDERTYPEID")]
    public string Fordertypeid { get; set; } = string.Empty;

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
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID")]
    public string Fsecunitid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY")]
    public decimal Fsecunitqty { get; set; }

    /// <summary>
    /// 调出保管者内码
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPEROUTID")]
    public string Fkeeperoutid { get; set; } = string.Empty;

    /// <summary>
    /// 调出保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEOUTID")]
    public string Fkeepertypeoutid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
