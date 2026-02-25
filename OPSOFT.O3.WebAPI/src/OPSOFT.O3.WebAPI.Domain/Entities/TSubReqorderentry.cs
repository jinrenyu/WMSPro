using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外订单明细
/// </summary>
[SugarTable("T_SUB_REQORDERENTRY")]
public class TSubReqorderentry : BaseEntity
{
    /// <summary>
    /// 常用退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXBCOMMITQTY")]
    public decimal Fauxbcommitqty { get; set; }

    /// <summary>
    /// 关联数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXCOMMITQTY")]
    public decimal Fauxcommitqty { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 开票数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTYINVOICE")]
    public decimal Fauxqtyinvoice { get; set; }

    /// <summary>
    /// 基本退料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBCOMMITQTY")]
    public decimal Fbcommitqty { get; set; }

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// Bom编号内码
    /// </summary>
    [SugarColumn(ColumnName = "FBOMINTERID")]
    public string Fbominterid { get; set; } = string.Empty;

    /// <summary>
    /// 检验方式
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKMETHOD")]
    public string Fcheckmethod { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 基本关联数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMITQTY")]
    public decimal Fcommitqty { get; set; }

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public int Fsrcentryid { get; set; }

    /// <summary>
    /// 采购订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERENTRYID")]
    public int Forderentryid { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FFETCHDATE")]
    public DateTime? Ffetchdate { get; set; }

    /// <summary>
    /// 顺序号
    /// </summary>
    [SugarColumn(ColumnName = "FINDEX")]
    public int Findex { get; set; }

    /// <summary>
    /// 完工超收比例(%
    /// </summary>
    [SugarColumn(ColumnName = "FINHIGHLIMIT")]
    public decimal Finhighlimit { get; set; }
}
