using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用水监测
/// </summary>
[SugarTable("T_EC_UWATER")]
public class TEcUwater : BaseEntity
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
    /// 水量
    /// </summary>
    [SugarColumn(ColumnName = "FWATER")]
    public int Fwater { get; set; }
}
