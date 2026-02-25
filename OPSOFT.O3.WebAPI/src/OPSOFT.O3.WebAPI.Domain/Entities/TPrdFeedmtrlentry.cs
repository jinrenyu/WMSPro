using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产补料单明细
/// </summary>
[SugarTable("T_PRD_FEEDMTRLENTRY")]
public class TPrdFeedmtrlentry : BaseEntity
{
    /// <summary>
    /// 实发数量
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALQTY")]
    public decimal Factualqty { get; set; }

    /// <summary>
    /// 可超发数量
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWOVERQTY")]
    public decimal Fallowoverqty { get; set; }

    /// <summary>
    /// 总成本
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 申请数量
    /// </summary>
    [SugarColumn(ColumnName = "FAPPQTY")]
    public decimal Fappqty { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// Bom版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 有效期至(变更后
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }
}
