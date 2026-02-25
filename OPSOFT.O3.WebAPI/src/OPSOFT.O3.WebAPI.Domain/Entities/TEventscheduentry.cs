using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 事件调度-事件定义
/// </summary>
[SugarTable("T_EVENTSCHEDUENTRY")]
public class TEventscheduentry : BaseEntity
{
    /// <summary>
    /// 查询条件(字段
    /// </summary>
    [SugarColumn(ColumnName = "FILTER")]
    public string Filter { get; set; } = string.Empty;
}
