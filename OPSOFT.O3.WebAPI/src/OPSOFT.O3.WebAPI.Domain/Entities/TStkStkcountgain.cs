using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 盘盈单表头
/// </summary>
[SugarTable("T_STK_STKCOUNTGAIN")]
public class TStkStkcountgain : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 盘点来源
    /// </summary>
    [SugarColumn(ColumnName = "FSRCTYPE")]
    public int Fsrctype { get; set; }

    /// <summary>
    /// 物料盘点作业内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTKCOUNTINPUTID")]
    public string Fstkcountinputid { get; set; } = string.Empty;

    /// <summary>
    /// 盘点方案内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTKCOUNTSCHEMEID")]
    public string Fstkcountschemeid { get; set; } = string.Empty;

    /// <summary>
    /// 库存组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKORGID")]
    public string Fstockorgid { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
