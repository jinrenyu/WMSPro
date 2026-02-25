using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用气监测
/// </summary>
[SugarTable("T_EC_UGAS")]
public class TEcUgas : BaseEntity
{
    /// <summary>
    /// 设备号
    /// </summary>
    [SugarColumn(ColumnName = "FDEVICENUM")]
    public string Fdevicenum { get; set; } = string.Empty;

    /// <summary>
    /// 时间
    /// </summary>
    [SugarColumn(ColumnName = "FTIME")]
    public DateTime? Ftime { get; set; }

    /// <summary>
    /// 气量
    /// </summary>
    [SugarColumn(ColumnName = "FGAS")]
    public int Fgas { get; set; }
}
