using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 齐套分析-物料明细
/// </summary>
[SugarTable("T_BD_WHOLEANALYSIS_MATERIALINFO")]
public class TBdWholeanalysisMaterialinfo : BaseEntity
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
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 用料类型
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALTYPE")]
    public int Fmaterialtype { get; set; }

    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEGROUP")]
    public decimal Freplacegroup { get; set; }

    /// <summary>
    /// 用量
    /// </summary>
    [SugarColumn(ColumnName = "FUNITQTY")]
    public decimal Funitqty { get; set; }

    /// <summary>
    /// 实际需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALQTY")]
    public decimal Factualqty { get; set; }

    /// <summary>
    /// 工单总需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FREQUESTQTY")]
    public decimal Frequestqty { get; set; }

    /// <summary>
    /// 工单已领数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKEDQTY")]
    public decimal Fpickedqty { get; set; }

    /// <summary>
    /// 可满足齐套数量
    /// </summary>
    [SugarColumn(ColumnName = "FCANWHOLEQTY")]
    public decimal Fcanwholeqty { get; set; }

    /// <summary>
    /// 总需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FALLWHOLEQTY")]
    public decimal Fallwholeqty { get; set; }

    /// <summary>
    /// 物料齐套状态
    /// </summary>
    [SugarColumn(ColumnName = "FWHOLESTATUS")]
    public string Fwholestatus { get; set; } = string.Empty;

    /// <summary>
    /// 在库可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKQTY")]
    public decimal Finstockqty { get; set; }

    /// <summary>
    /// 在线可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FONLINEQTY")]
    public decimal Fonlineqty { get; set; }

    /// <summary>
    /// 待入库可用数量
    /// </summary>
    [SugarColumn(ColumnName = "FWILLINSTOCKQTY")]
    public decimal Fwillinstockqty { get; set; }

    /// <summary>
    /// 齐套后剩余可用数
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISEDLEFTQTY")]
    public decimal Fanalysisedleftqty { get; set; }

    /// <summary>
    /// 最大齐套数量
    /// </summary>
    [SugarColumn(ColumnName = "FMAXWHOLEQTY")]
    public decimal Fmaxwholeqty { get; set; }

    /// <summary>
    /// 齐套欠料数量
    /// </summary>
    [SugarColumn(ColumnName = "FLACKQTY")]
    public decimal Flackqty { get; set; }

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
