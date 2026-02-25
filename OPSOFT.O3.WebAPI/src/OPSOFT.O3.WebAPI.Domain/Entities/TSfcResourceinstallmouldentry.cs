using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源上下模记录
/// </summary>
[SugarTable("T_SFC_RESOURCEINSTALLMOULDENTRY")]
public class TSfcResourceinstallmouldentry : BaseEntity
{
    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 模治具内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOULDID")]
    public string Fmouldid { get; set; } = string.Empty;

    /// <summary>
    /// 上模操作人
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLEMPID")]
    public string Finstallempid { get; set; } = string.Empty;

    /// <summary>
    /// 上模时间
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLTIME")]
    public DateTime? Finstalltime { get; set; }

    /// <summary>
    /// 下模操作人
    /// </summary>
    [SugarColumn(ColumnName = "FREMOVEEMPID")]
    public string Fremoveempid { get; set; } = string.Empty;

    /// <summary>
    /// 下模时间
    /// </summary>
    [SugarColumn(ColumnName = "FREMOVETIME")]
    public DateTime? Fremovetime { get; set; }

    /// <summary>
    /// 累计使用时长（秒）
    /// </summary>
    [SugarColumn(ColumnName = "TOTALUSEDSEC")]
    public decimal Totalusedsec { get; set; }
}
