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

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 逻辑条件
    /// </summary>
    [SugarColumn(ColumnName = "FLOGCONDITION", IsNullable = true)]
    public string FLOGCONDITION { get; set; } = string.Empty;

    /// <summary>
    /// 筛选条件
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT", IsNullable = true)]
    public string FCONTENT { get; set; } = string.Empty;

    /// <summary>
    /// 栏位说明
    /// </summary>
    [SugarColumn(ColumnName = "FHEADCAPTION", IsNullable = true)]
    public string FHEADCAPTION { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;
}
