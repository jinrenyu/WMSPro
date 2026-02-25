using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 省自治区表
/// </summary>
[SugarTable("MAP_PROVINCE")]
public class MapProvince : BaseEntity
{
    /// <summary>
    /// 省层级
    /// </summary>
    [SugarColumn(ColumnName = "PID")]
    public int Pid { get; set; }

    /// <summary>
    /// 省名字
    /// </summary>
    [SugarColumn(ColumnName = "PNAME")]
    public string Pname { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
