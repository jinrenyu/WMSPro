using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用电监测
/// </summary>
[SugarTable("T_EC_ELECTRICITY")]
public class TEcElectricity : BaseEntity
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
    /// 电量
    /// </summary>
    [SugarColumn(ColumnName = "FELECTRICITY")]
    public decimal Felectricity { get; set; }

    /// <summary>
    /// 功率
    /// </summary>
    [SugarColumn(ColumnName = "FPOWER")]
    public int Fpower { get; set; }

    /// <summary>
    /// 电压
    /// </summary>
    [SugarColumn(ColumnName = "FVOLTAGE")]
    public int Fvoltage { get; set; }

    /// <summary>
    /// 电流
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENT")]
    public int Fcurrent { get; set; }
}
