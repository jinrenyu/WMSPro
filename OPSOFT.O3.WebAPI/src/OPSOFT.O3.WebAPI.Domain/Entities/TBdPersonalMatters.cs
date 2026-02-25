using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 个人事项
/// </summary>
[SugarTable("T_BD_PERSONALMATTERS")]
public class TBdPersonalmatters : BaseEntity
{
    /// <summary>
    /// 个人信息
    /// </summary>
    [SugarColumn(ColumnName = "FINFO")]
    public string Finfo { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FPERSONALDATE")]
    public DateTime? Fpersonaldate { get; set; }
}
