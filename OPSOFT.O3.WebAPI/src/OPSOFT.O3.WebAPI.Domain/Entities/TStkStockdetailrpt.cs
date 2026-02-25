using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料收发
/// </summary>
[SugarTable("T_STK_STOCKDETAILRPT")]
public class TStkStockdetailrpt : BaseEntity
{
    /// <summary>
    /// 起始日期
    /// </summary>
    [SugarColumn(ColumnName = "FBEGINDATE")]
    public DateTime? Fbegindate { get; set; }

    /// <summary>
    /// 截止日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE")]
    public DateTime? Fenddate { get; set; }

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
