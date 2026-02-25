using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 齐套分析-在库物料表
/// </summary>
[SugarTable("T_BD_WHOLEANALYSIS_MATERIAL_INSTOCK")]
public class TBdWholeanalysisMaterialInstock : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 库存类型
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKTYPE")]
    public string Fstocktype { get; set; } = string.Empty;

    /// <summary>
    /// 分析前可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FBEFOREANALYQTY")]
    public decimal Fbeforeanalyqty { get; set; }

    /// <summary>
    /// 分析后可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FAFTERANALYQTY")]
    public decimal Fafteranalyqty { get; set; }

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
