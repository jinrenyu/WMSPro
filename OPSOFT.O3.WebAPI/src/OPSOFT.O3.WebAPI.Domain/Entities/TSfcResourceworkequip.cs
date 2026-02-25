using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源绑定设备信息
/// </summary>
[SugarTable("T_SFC_RESOURCEWORKEQUIP")]
public class TSfcResourceworkequip : BaseEntity
{
    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPID")]
    public string Fequipid { get; set; } = string.Empty;
}
