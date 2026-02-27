using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档
/// </summary>
[SugarTable("T_BD_BARCODERS")]
public class TBdBarcoders : BaseEntity
{
    /// <summary>
    /// 总数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALQTY")]
    public decimal Ftotalqty { get; set; }

    /// <summary>
    /// 制造日期
    /// </summary>
    [SugarColumn(ColumnName = "FMFGDATE")]
    public DateTime? Fmfgdate { get; set; }

    /// <summary>
    /// 原始条码
    /// </summary>
    [SugarColumn(ColumnName = "FOBARCODE")]
    public string Fobarcode { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 条码状态
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODESTATUS")]
    public int Fbarcodestatus { get; set; }

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE")]
    public int Fbartype { get; set; }

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 是否箱码
    /// </summary>
    [SugarColumn(ColumnName = "FISBOX")]
    public bool Fisbox { get; set; }

    /// <summary>
    /// 箱码条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBOXCODETYPE")]
    public int Fboxcodetype { get; set; }

    /// <summary>
    /// 是否混装
    /// </summary>
    [SugarColumn(ColumnName = "FISMIX")]
    public bool Fismix { get; set; }

    /// <summary>
    /// 批次内码
    /// </summary>
    [SugarColumn(ColumnName = "FLOTID")]
    public string Flotid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUS")]
    public int Fstockstatus { get; set; }

    /// <summary>
    /// 质量(0=未检验 1=合格 2=不合格 3=待判定
    /// </summary>
    [SugarColumn(ColumnName = "FQUALITYSTATUS")]
    public int Fqualitystatus { get; set; }

    /// <summary>
    /// 打印来源
    /// </summary>
    [SugarColumn(ColumnName = "FKID", IsNullable = true)]
    public string FKID { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID", IsNullable = true)]
    public string FAUXPROPID { get; set; } = string.Empty;

    /// <summary>
    /// 保管者内码
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID", IsNullable = true)]
    public string FKEEPERID { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID", IsNullable = true)]
    public string FOWNERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 生成时数量
    /// </summary>
    [SugarColumn(ColumnName = "FFORMQTY", IsNullable = true)]
    public decimal? FFORMQTY { get; set; }

    /// <summary>
    /// 销售单位
    /// </summary>
    [SugarColumn(ColumnName = "FSALEUNITID", IsNullable = true)]
    public string FSALEUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 检验日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTDATE", IsNullable = true)]
    public DateTime? FINSPECTDATE { get; set; }

    /// <summary>
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY", IsNullable = true)]
    public decimal? FSECUNITQTY { get; set; }

    /// <summary>
    /// 常用单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 打印日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE", IsNullable = true)]
    public DateTime? FDATE { get; set; }

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE", IsNullable = true)]
    public DateTime? FKFDATE { get; set; }

    /// <summary>
    /// 本次生成条码标识
    /// </summary>
    [SugarColumn(ColumnName = "FCURRID", IsNullable = true)]
    public string FCURRID { get; set; } = string.Empty;

    /// <summary>
    /// 被装箱
    /// </summary>
    [SugarColumn(ColumnName = "FINBOX", IsNullable = true)]
    public bool? FINBOX { get; set; }

    /// <summary>
    /// 基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEQTY", IsNullable = true)]
    public decimal? FBASEQTY { get; set; }

    /// <summary>
    /// 常用单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 送货单内码
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYID", IsNullable = true)]
    public string FDELIVERYID { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID", IsNullable = true)]
    public string FSUPPLYID { get; set; } = string.Empty;

    /// <summary>
    /// 检验人
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTID", IsNullable = true)]
    public string FINSPECTID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID", IsNullable = true)]
    public string FBASEUNITID { get; set; } = string.Empty;

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
    /// 销售数量
    /// </summary>
    [SugarColumn(ColumnName = "FSALEQTY", IsNullable = true)]
    public decimal? FSALEQTY { get; set; }

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
    /// 送货单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDETAILID", IsNullable = true)]
    public string FDELIVERYDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID", IsNullable = true)]
    public string FSTOCKID { get; set; } = string.Empty;
}
