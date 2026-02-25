using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码编码流水号档
/// </summary>
[SugarTable("SYS_BARCODENO")]
public class SysBarcodeNo : BaseEntity
{
    /// <summary>
    /// 规码ID
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSTYPEID")]
    public string Fclasstypeid { get; set; } = string.Empty;

    /// <summary>
    /// 编码标识
    /// </summary>
    [SugarColumn(ColumnName = "FBILLCODE")]
    public string Fbillcode { get; set; } = string.Empty;

    /// <summary>
    /// 目前流水号
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public int Fseq { get; set; }
}
