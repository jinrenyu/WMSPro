using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档-原材料源单档
/// </summary>
[SugarTable("T_BD_BARCODERS1")]
public class TBdBarcoders1 : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单内码【主单据】
    /// </summary>
    [SugarColumn(ColumnName = "FPOID")]
    public string Fpoid { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FPOBILLNO")]
    public string Fpobillno { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单表体行号
    /// </summary>
    [SugarColumn(ColumnName = "FPOENTREYID")]
    public int Fpoentreyid { get; set; }

    /// <summary>
    /// 采购订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPODETAILID")]
    public string Fpodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 收料单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FPURID")]
    public string Fpurid { get; set; } = string.Empty;

    /// <summary>
    /// 收料单编号
    /// </summary>
    [SugarColumn(ColumnName = "FPURBILLNO")]
    public string Fpurbillno { get; set; } = string.Empty;

    /// <summary>
    /// 收料表体行号
    /// </summary>
    [SugarColumn(ColumnName = "FPURENTRYID")]
    public int Fpurentryid { get; set; }

    /// <summary>
    /// 收料表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPURDETAILID")]
    public string Fpurdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLIERID")]
    public string Fsupplierid { get; set; } = string.Empty;

    /// <summary>
    /// IQC检验员
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTID")]
    public string Finspectid { get; set; } = string.Empty;

    /// <summary>
    /// IQC检验日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTDATE")]
    public DateTime? Finspectdate { get; set; }
}
