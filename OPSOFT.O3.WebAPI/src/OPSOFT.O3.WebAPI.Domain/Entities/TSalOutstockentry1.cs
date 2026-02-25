using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售出库条码明细
/// </summary>
[SugarTable("T_SAL_OUTSTOCKENTRY1")]
public class TSalOutstockentry1 : BaseEntity
{
    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 录入类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 条码数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

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
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public string Fbaseunitqty { get; set; } = string.Empty;

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
    /// 是否箱码
    /// </summary>
    [SugarColumn(ColumnName = "FISBOX")]
    public bool Fisbox { get; set; }

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE")]
    public decimal Ftaxprice { get; set; }

    /// <summary>
    /// 税率%
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

    /// <summary>
    /// 折扣率%
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNTRATE")]
    public decimal Fdiscountrate { get; set; }

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
    /// 订单类型
    /// </summary>
    [SugarColumn(ColumnName = "FORDERTYPEID")]
    public string Fordertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE")]
    public int Fbartype { get; set; }

    /// <summary>
    /// 箱码
    /// </summary>
    [SugarColumn(ColumnName = "FBOXBARCODE")]
    public string Fboxbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 父项产品
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTMATID")]
    public string Fparentmatid { get; set; } = string.Empty;

    /// <summary>
    /// 套件行内码
    /// </summary>
    [SugarColumn(ColumnName = "FROWID")]
    public string Frowid { get; set; } = string.Empty;

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
    /// 有效期至(变更后
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }
}
