using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序委外接收表体-接收明细
/// </summary>
[SugarTable("T_SFC_SUBRECEIVEENTRY")]
public class TSfcSubreceiveentry : BaseEntity
{
    /// <summary>
    /// 发出单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FOUTDETAILID")]
    public string Foutdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 表体单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 接收数量
    /// </summary>
    [SugarColumn(ColumnName = "FREVQTY")]
    public decimal Frevqty { get; set; }

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 检查数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKQTY")]
    public decimal Fcheckqty { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 委外金额
    /// </summary>
    [SugarColumn(ColumnName = "FFEEAMOUNT")]
    public decimal Ffeeamount { get; set; }

    /// <summary>
    /// 委外发出源单
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBILLID")]
    public string Fsourcebillid { get; set; } = string.Empty;

    /// <summary>
    /// 委外发出行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEDETAILID")]
    public string Fsourcedetailid { get; set; } = string.Empty;

    /// <summary>
    /// 已下推数量
    /// </summary>
    [SugarColumn(ColumnName = "FPUSHQTY")]
    public decimal Fpushqty { get; set; }

    /// <summary>
    /// 是否委外结算
    /// </summary>
    [SugarColumn(ColumnName = "FISSETTLED")]
    public bool Fissettled { get; set; }

    /// <summary>
    /// 是否委外开票
    /// </summary>
    [SugarColumn(ColumnName = "FISINVOICED")]
    public bool Fisinvoiced { get; set; }

    /// <summary>
    /// 生产工单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODETYPE")]
    public string Fbarcodetype { get; set; } = string.Empty;

    /// <summary>
    /// 汇报行内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

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
