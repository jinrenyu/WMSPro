using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 成品标签核对
/// </summary>
[SugarTable("T_PRD_BARCODECHECK")]
public class TPrdBarcodecheck : BaseEntity
{
    /// <summary>
    /// 内包装条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODEINNER")]
    public string Fbarcodeinner { get; set; } = string.Empty;

    /// <summary>
    /// 中包装条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODEMIDDLE")]
    public string Fbarcodemiddle { get; set; } = string.Empty;

    /// <summary>
    /// 外包装条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODEOUTER")]
    public string Fbarcodeouter { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
