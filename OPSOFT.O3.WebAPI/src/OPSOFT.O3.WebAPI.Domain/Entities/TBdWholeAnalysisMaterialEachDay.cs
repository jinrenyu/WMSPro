using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 齐套分析-物料按日期整体分析表
/// </summary>
[SugarTable("T_BD_WHOLEANALYSIS_MATERIAL_EACHDAY")]
public class TBdWholeanalysisMaterialEachday : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FANALYDATE")]
    public DateTime? Fanalydate { get; set; }

    /// <summary>
    /// 需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FREQUESTQTY")]
    public decimal Frequestqty { get; set; }

    /// <summary>
    /// 真实需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALREQUESTQTY")]
    public decimal Factualrequestqty { get; set; }

    /// <summary>
    /// 齐套物料数
    /// </summary>
    [SugarColumn(ColumnName = "FWHOLEQTY")]
    public decimal Fwholeqty { get; set; }

    /// <summary>
    /// 剩余可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FLEFTQTY")]
    public decimal Fleftqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 缺料数量
    /// </summary>
    [SugarColumn(ColumnName = "FLACK")]
    public decimal Flack { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

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
