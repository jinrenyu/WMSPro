using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 月结表
/// </summary>
[SugarTable("T_GL_CLOSEACCOUNT")]
public class TGlCloseaccount : BaseEntity
{
    /// <summary>
    /// 年
    /// </summary>
    [SugarColumn(ColumnName = "FYEAR")]
    public string Fyear { get; set; } = string.Empty;

    /// <summary>
    /// 月份
    /// </summary>
    [SugarColumn(ColumnName = "FPERIOD")]
    public int Fperiod { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 换算率
    /// </summary>
    [SugarColumn(ColumnName = "FCOEFFICIENT")]
    public decimal Fcoefficient { get; set; }

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 默认入库仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 默认入库仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 生产/采购日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// MTONO
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 供应厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 入库日期
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKINDATE")]
    public DateTime? Fstockindate { get; set; }

    /// <summary>
    /// 期初结存数量
    /// </summary>
    [SugarColumn(ColumnName = "FBEGQTY")]
    public decimal Fbegqty { get; set; }

    /// <summary>
    /// 期初数量（辅助计量单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSECBEGQTY")]
    public decimal Fsecbegqty { get; set; }

    /// <summary>
    /// 期末结存数量
    /// </summary>
    [SugarColumn(ColumnName = "FENDQTY")]
    public decimal Fendqty { get; set; }

    /// <summary>
    /// 期末数量（辅助计量单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSECENDQTY")]
    public decimal Fsecendqty { get; set; }

    /// <summary>
    /// 本期收入数量
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVE")]
    public decimal Freceive { get; set; }

    /// <summary>
    /// 本期收入数量（辅助计量单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSECRECEIVE")]
    public decimal Fsecreceive { get; set; }

    /// <summary>
    /// 本期发出数量
    /// </summary>
    [SugarColumn(ColumnName = "FSEND")]
    public decimal Fsend { get; set; }

    /// <summary>
    /// 本期发出数量（辅助计量单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSECSEND")]
    public decimal Fsecsend { get; set; }

    /// <summary>
    /// 本年收入数量
    /// </summary>
    [SugarColumn(ColumnName = "FYTDRECEIVE")]
    public decimal Fytdreceive { get; set; }

    /// <summary>
    /// 本年收入数量（辅助计量单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSECYTDRECEIVE")]
    public decimal Fsecytdreceive { get; set; }

    /// <summary>
    /// 本年发出数量
    /// </summary>
    [SugarColumn(ColumnName = "FYTDSEND")]
    public decimal Fytdsend { get; set; }

    /// <summary>
    /// 本年发出数量（辅助计量单位）
    /// </summary>
    [SugarColumn(ColumnName = "FSECYTDSEND")]
    public decimal Fsecytdsend { get; set; }

    /// <summary>
    /// 期初结存余额
    /// </summary>
    [SugarColumn(ColumnName = "FBEGBAL")]
    public decimal Fbegbal { get; set; }

    /// <summary>
    /// 期末结存余额
    /// </summary>
    [SugarColumn(ColumnName = "FENDBAL")]
    public decimal Fendbal { get; set; }

    /// <summary>
    /// 本期收入余额
    /// </summary>
    [SugarColumn(ColumnName = "FDEBIT")]
    public decimal Fdebit { get; set; }

    /// <summary>
    /// 本期发出余额
    /// </summary>
    [SugarColumn(ColumnName = "FCREDIT")]
    public decimal Fcredit { get; set; }

    /// <summary>
    /// 本年收入余额
    /// </summary>
    [SugarColumn(ColumnName = "FYTDDEBIT")]
    public decimal Fytddebit { get; set; }

    /// <summary>
    /// 本年发出余额
    /// </summary>
    [SugarColumn(ColumnName = "FYTDCREDIT")]
    public decimal Fytdcredit { get; set; }

    /// <summary>
    /// 期初结存材料成本差异
    /// </summary>
    [SugarColumn(ColumnName = "FBEGDIFF")]
    public decimal Fbegdiff { get; set; }

    /// <summary>
    /// 本期收入成本差异
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEDIFF")]
    public decimal Freceivediff { get; set; }

    /// <summary>
    /// 本期发出成本差异
    /// </summary>
    [SugarColumn(ColumnName = "FSENDDIFF")]
    public decimal Fsenddiff { get; set; }

    /// <summary>
    /// 期末结存材料成本差异
    /// </summary>
    [SugarColumn(ColumnName = "FENDDIFF")]
    public decimal Fenddiff { get; set; }

    /// <summary>
    /// 本年收入差异
    /// </summary>
    [SugarColumn(ColumnName = "FYTDRECEIVEDIFF")]
    public decimal Fytdreceivediff { get; set; }

    /// <summary>
    /// 本年发出差异
    /// </summary>
    [SugarColumn(ColumnName = "FYTDSENDDIFF")]
    public decimal Fytdsenddiff { get; set; }

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
    /// 本期收入数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "FBASERECEIVE")]
    public decimal Fbasereceive { get; set; }

    /// <summary>
    /// 本期发出数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "FBASESEND")]
    public decimal Fbasesend { get; set; }

    /// <summary>
    /// 期初结存数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "FBASEBEGQTY")]
    public decimal Fbasebegqty { get; set; }

    /// <summary>
    /// 期末结存数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "FBASEENDQTY")]
    public decimal Fbaseendqty { get; set; }

    /// <summary>
    /// 本年收入数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "FBASEYTDRECEIVE")]
    public decimal Fbaseytdreceive { get; set; }

    /// <summary>
    /// 本年发出数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "FBASEYTDSEND")]
    public decimal Fbaseytdsend { get; set; }

    /// <summary>
    /// 仓库库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }

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
