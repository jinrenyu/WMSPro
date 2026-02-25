using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 报价单-物料信息
/// </summary>
[SugarTable("ODK_SRM_QuoteEntry")]
public class OdkSrmQuoteEntry : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 年需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FYEARQTY")]
    public decimal Fyearqty { get; set; }

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FDEMANDDATE")]
    public DateTime? Fdemanddate { get; set; }

    /// <summary>
    /// 可供数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUOTEQTY")]
    public decimal Fquoteqty { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 采购方建议单价
    /// </summary>
    [SugarColumn(ColumnName = "FPROPPRICE")]
    public decimal Fpropprice { get; set; }

    /// <summary>
    /// 备货/施工周期
    /// </summary>
    [SugarColumn(ColumnName = "FPREPARECYCLE")]
    public int Fpreparecycle { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE")]
    public DateTime? Fdeliverydate { get; set; }

    /// <summary>
    /// 是否含税
    /// </summary>
    [SugarColumn(ColumnName = "FISTAX")]
    public bool Fistax { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAX")]
    public decimal Ftax { get; set; }

    /// <summary>
    /// 税前金额
    /// </summary>
    [SugarColumn(ColumnName = "FPRETAXAMT")]
    public decimal Fpretaxamt { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMT")]
    public decimal Ftaxamt { get; set; }

    /// <summary>
    /// 总额
    /// </summary>
    [SugarColumn(ColumnName = "FAMT")]
    public decimal Famt { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 采购申请单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREQINTERID")]
    public string Freqinterid { get; set; } = string.Empty;

    /// <summary>
    /// 采购申请单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FREQDETAILID")]
    public string Freqdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 询价单表体供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPDETAILID")]
    public string Fsupdetailid { get; set; } = string.Empty;

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
