using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序委外申请单表体-明细
/// </summary>
[SugarTable("T_SFC_PROSUBCONTRACTENTRY")]
public class TSfcProsubcontractentry : BaseEntity
{
    /// <summary>
    /// 物料编码内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡编号
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDNO")]
    public string Fflowcardno { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡表体识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODEID")]
    public string Fcodeid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERNO")]
    public string Forderno { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 已下推数量
    /// </summary>
    [SugarColumn(ColumnName = "FPUSHQTY")]
    public decimal Fpushqty { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 委外单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 委外金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMT")]
    public decimal Famt { get; set; }

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
