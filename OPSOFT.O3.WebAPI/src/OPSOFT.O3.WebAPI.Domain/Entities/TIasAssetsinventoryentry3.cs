using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资产盘点表体-暂存表
/// </summary>
[SugarTable("T_IAS_ASSETSINVENTORYENTRY3")]
public class TIasAssetsinventoryentry3 : BaseEntity
{
    /// <summary>
    /// 暂存资产内码
    /// </summary>
    [SugarColumn(ColumnName = "FASSETID")]
    public string Fassetid { get; set; } = string.Empty;

    /// <summary>
    /// 暂存数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

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
