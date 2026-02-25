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
}
