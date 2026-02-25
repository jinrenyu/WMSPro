using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 绩效考评表头
/// </summary>
[SugarTable("ODK_SRM_Performance")]
public class OdkSrmPerformance : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 考评人
    /// </summary>
    [SugarColumn(ColumnName = "FEXPECTID")]
    public string Fexpectid { get; set; } = string.Empty;

    /// <summary>
    /// 考评组织
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERBILLNO")]
    public string Ftenderbillno { get; set; } = string.Empty;

    /// <summary>
    /// 考评模板
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERINTERID")]
    public string Ftenderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 考评日期从
    /// </summary>
    [SugarColumn(ColumnName = "FBDATE")]
    public DateTime? Fbdate { get; set; }

    /// <summary>
    /// 考评日期至
    /// </summary>
    [SugarColumn(ColumnName = "FEDATE")]
    public DateTime? Fedate { get; set; }

    /// <summary>
    /// 考评说明
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE")]
    public bool Fisrelease { get; set; }

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
