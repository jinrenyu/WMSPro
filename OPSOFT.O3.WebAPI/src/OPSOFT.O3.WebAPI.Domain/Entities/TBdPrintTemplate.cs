using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 打印模板（条码标签设计模板）
/// </summary>
[SugarTable("T_BD_PRINTTEMPLATE")]
public class TBdPrintTemplate : BaseEntity
{
    /// <summary>
    /// 模板编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER", Length = 50)]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 模板名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME", Length = 100)]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 适用单据来源（编码规则表单键：PUR_PurchaseOrder=采购订单标签 / PUR_ReceiveBill=收料通知单标签）
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSOURCE", Length = 100)]
    public string FBillSource { get; set; } = string.Empty;

    /// <summary>
    /// 模板内容（hiprint 模板 JSON，长文本）
    /// </summary>
    [SugarColumn(ColumnName = "FTEMPLATE", ColumnDataType = "text", IsNullable = true)]
    public string FTemplate { get; set; } = string.Empty;

    /// <summary>
    /// 纸张宽度（mm）
    /// </summary>
    [SugarColumn(ColumnName = "FPAPERWIDTH")]
    public decimal FPaperWidth { get; set; }

    /// <summary>
    /// 纸张高度（mm）
    /// </summary>
    [SugarColumn(ColumnName = "FPAPERHEIGHT")]
    public decimal FPaperHeight { get; set; }

    /// <summary>
    /// 是否默认模板（同一单据来源仅一个为默认）
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULT")]
    public bool FIsDefault { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION", Length = 500)]
    public string FDescription { get; set; } = string.Empty;
}
