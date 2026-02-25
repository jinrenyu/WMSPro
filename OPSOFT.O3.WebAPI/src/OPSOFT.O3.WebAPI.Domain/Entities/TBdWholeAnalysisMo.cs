using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 齐套分析-生产订单范围表
/// </summary>
[SugarTable("T_BD_WHOLEANALYSIS_MO")]
public class TBdWholeanalysisMo : BaseEntity
{
    /// <summary>
    /// 生产订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public string Fmoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ")]
    public int Fmoentryseq { get; set; }

    /// <summary>
    /// 成品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 预占
    /// </summary>
    [SugarColumn(ColumnName = "FISPRELOCK")]
    public bool Fisprelock { get; set; }

    /// <summary>
    /// 预占时间
    /// </summary>
    [SugarColumn(ColumnName = "FPRELOCKTIME")]
    public DateTime? Fprelocktime { get; set; }

    /// <summary>
    /// 预占人
    /// </summary>
    [SugarColumn(ColumnName = "FPRELOCKEMP")]
    public string Fprelockemp { get; set; } = string.Empty;

    /// <summary>
    /// 齐套状态
    /// </summary>
    [SugarColumn(ColumnName = "FWHOLESTATUS")]
    public string Fwholestatus { get; set; } = string.Empty;

    /// <summary>
    /// 分析顺序
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISSEQ")]
    public int Fanalysisseq { get; set; }

    /// <summary>
    /// 最大齐套数
    /// </summary>
    [SugarColumn(ColumnName = "FMAXWHOELQTY")]
    public decimal Fmaxwhoelqty { get; set; }

    /// <summary>
    /// 计划开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTTIME")]
    public DateTime? Fplanstarttime { get; set; }

    /// <summary>
    /// 计划结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANENDTIME")]
    public DateTime? Fplanendtime { get; set; }

    /// <summary>
    /// 计划数量
    /// </summary>
    [SugarColumn(ColumnName = "FPLANQTY")]
    public decimal Fplanqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

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
