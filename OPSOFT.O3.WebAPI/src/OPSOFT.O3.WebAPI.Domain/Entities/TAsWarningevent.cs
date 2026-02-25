using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯预警事件
/// </summary>
[SugarTable("T_AS_WARNINGEVENT")]
public class TAsWarningevent : BaseEntity
{
    /// <summary>
    /// 事件代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 事件名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 事件类型ID
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
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
    /// ERP编码
    /// </summary>
    [SugarColumn(ColumnName = "FERPNUMBER")]
    public string Ferpnumber { get; set; } = string.Empty;

    /// <summary>
    /// 停工呼叫事件
    /// </summary>
    [SugarColumn(ColumnName = "FSTOPCALLID")]
    public string Fstopcallid { get; set; } = string.Empty;
}
