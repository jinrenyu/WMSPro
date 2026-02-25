using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 质量终检表体-缺陷
/// </summary>
[SugarTable("T_SFC_LASTINPECTIONENTRY1")]
public class TSfcLastinpectionentry1 : BaseEntity
{
    /// <summary>
    /// 缺陷类型
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTTYPE")]
    public string Ffaulttype { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷原因
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTREASON")]
    public string Ffaultreason { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷数量
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTQTY")]
    public decimal Ffaultqty { get; set; }

    /// <summary>
    /// 缺陷等级
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTLEVEL")]
    public int Ffaultlevel { get; set; }

    /// <summary>
    /// 缺陷后果
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTRESULT")]
    public string Ffaultresult { get; set; } = string.Empty;

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
