using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产叫料物料明细
/// </summary>
[SugarTable("T_PRD_CALLENTRY")]
public class TPrdCallentry : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 挂起数量
    /// </summary>
    [SugarColumn(ColumnName = "FHANGQTY")]
    public decimal Fhangqty { get; set; }

    /// <summary>
    /// 挂起基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FHANGBASEQTY")]
    public decimal Fhangbaseqty { get; set; }

    /// <summary>
    /// 用料时间
    /// </summary>
    [SugarColumn(ColumnName = "FUSEDATE")]
    public DateTime? Fusedate { get; set; }

    /// <summary>
    /// 紧急
    /// </summary>
    [SugarColumn(ColumnName = "FISURGENT")]
    public bool Fisurgent { get; set; }

    /// <summary>
    /// 生产用料清单编码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMBILLNO")]
    public string Fppbombillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产用料清单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMENTRYID")]
    public string Fppbomentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产用料清单行号
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMENTRYSEQ")]
    public int Fppbomentryseq { get; set; }

    /// <summary>
    /// 生产用料清单内码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMID")]
    public string Fppbomid { get; set; } = string.Empty;

    /// <summary>
    /// 已发料数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKEDQTY")]
    public decimal Fpickedqty { get; set; }

    /// <summary>
    /// 已发料基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKEDBASEQTY")]
    public decimal Fpickedbaseqty { get; set; }

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
