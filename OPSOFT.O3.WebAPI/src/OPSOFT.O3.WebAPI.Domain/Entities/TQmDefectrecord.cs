using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 汇报过程缺陷记录
/// </summary>
[SugarTable("T_QM_DEFECTRECORD")]
public class TQmDefectrecord : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 缺陷类型
    /// </summary>
    [SugarColumn(ColumnName = "FDEFECTTYPE")]
    public string Fdefecttype { get; set; } = string.Empty;

    /// <summary>
    /// 原因代码
    /// </summary>
    [SugarColumn(ColumnName = "FREASONID")]
    public string Freasonid { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷等级
    /// </summary>
    [SugarColumn(ColumnName = "FDEFECTLEVEL")]
    public int Fdefectlevel { get; set; }

    /// <summary>
    /// 缺陷数量
    /// </summary>
    [SugarColumn(ColumnName = "FDEFECTQTY")]
    public decimal Fdefectqty { get; set; }

    /// <summary>
    /// 汇报内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID")]
    public string Freportid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
