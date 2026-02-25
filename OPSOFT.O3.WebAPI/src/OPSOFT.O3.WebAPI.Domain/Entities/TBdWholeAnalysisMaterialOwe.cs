using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 齐套分析-物料整体欠料表
/// </summary>
[SugarTable("T_BD_WHOLEANALYSIS_MATERIAL_OWE")]
public class TBdWholeanalysisMaterialOwe : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 总需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FREQUESTQTY")]
    public decimal Frequestqty { get; set; }

    /// <summary>
    /// 总库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY")]
    public decimal Fstockqty { get; set; }

    /// <summary>
    /// 齐套欠料数量
    /// </summary>
    [SugarColumn(ColumnName = "FLACKQTY")]
    public decimal Flackqty { get; set; }

    /// <summary>
    /// 分析前库存可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FBEFOREANALYSISINSTOCKQTY")]
    public decimal Fbeforeanalysisinstockqty { get; set; }

    /// <summary>
    /// 分析后库存可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FAFTERANALYSISINSTOCKQTY")]
    public decimal Fafteranalysisinstockqty { get; set; }

    /// <summary>
    /// 分析前在线可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FBEFOREANALYSISONLINEQTY")]
    public decimal Fbeforeanalysisonlineqty { get; set; }

    /// <summary>
    /// 分析后在线可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FAFTERANALYSISONLINEQTY")]
    public decimal Fafteranalysisonlineqty { get; set; }

    /// <summary>
    /// 分析前待入库可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FBEFOREANALYSISWILLINSTOCKQTY")]
    public decimal Fbeforeanalysiswillinstockqty { get; set; }

    /// <summary>
    /// 分析后待入库可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FAFTERANALYSISWILLINSTOCKQTY")]
    public decimal Fafteranalysiswillinstockqty { get; set; }

    /// <summary>
    /// 红牌锁定数量
    /// </summary>
    [SugarColumn(ColumnName = "FLOCKQTY")]
    public decimal Flockqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
