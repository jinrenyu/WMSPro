using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 同步信息
/// </summary>
[SugarTable("T_BD_SYNInfo")]
public class TBdSYNInfo : BaseEntity
{
    /// <summary>
    /// 同步表名
    /// </summary>
    [SugarColumn(ColumnName = "FDATANAME")]
    public string Fdataname { get; set; } = string.Empty;

    /// <summary>
    /// 已同步时间
    /// </summary>
    [SugarColumn(ColumnName = "FTIME")]
    public DateTime? Ftime { get; set; }
}
