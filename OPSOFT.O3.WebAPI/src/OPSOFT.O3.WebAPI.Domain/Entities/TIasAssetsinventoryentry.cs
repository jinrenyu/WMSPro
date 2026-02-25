using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资产盘点表体-帐存资料
/// </summary>
[SugarTable("T_IAS_ASSETSINVENTORYENTRY")]
public class TIasAssetsinventoryentry : BaseEntity
{
    /// <summary>
    /// 资产内码
    /// </summary>
    [SugarColumn(ColumnName = "FASSETID")]
    public string Fassetid { get; set; } = string.Empty;

    /// <summary>
    /// 存放地点代码
    /// </summary>
    [SugarColumn(ColumnName = "FLOCATIONNUMBER")]
    public string Flocationnumber { get; set; } = string.Empty;

    /// <summary>
    /// 存放地点名称
    /// </summary>
    [SugarColumn(ColumnName = "FLOCATIONNAME")]
    public string Flocationname { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FNUM")]
    public decimal Fnum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
