using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// XXX
/// </summary>
[SugarTable("T_SFC_INPROBARCODERS")]
public class TSfcInprobarcoders : BaseEntity
{
    /// <summary>
    /// 生产条码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCODE")]
    public string Fprocode { get; set; } = string.Empty;

    /// <summary>
    /// 继承原始条码（多工序用同一条码汇报）
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTPROCODE")]
    public string Freportprocode { get; set; } = string.Empty;

    /// <summary>
    /// 表示同一批次条码(采集单内码
    /// </summary>
    [SugarColumn(ColumnName = "FGUID")]
    public string Fguid { get; set; } = string.Empty;
}
