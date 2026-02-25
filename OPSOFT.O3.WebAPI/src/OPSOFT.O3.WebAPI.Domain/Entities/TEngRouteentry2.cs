using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工艺流程表体-首尾点
/// </summary>
[SugarTable("T_ENG_ROUTEENTRY2")]
public class TEngRouteentry2 : BaseEntity
{
    /// <summary>
    /// 工序开始位置X坐标
    /// </summary>
    [SugarColumn(ColumnName = "FBPOINTX")]
    public decimal Fbpointx { get; set; }

    /// <summary>
    /// 工序开始位置Y坐标
    /// </summary>
    [SugarColumn(ColumnName = "FBPOINTY")]
    public decimal Fbpointy { get; set; }

    /// <summary>
    /// 工序结束位置X坐标
    /// </summary>
    [SugarColumn(ColumnName = "FEPOINTX")]
    public decimal Fepointx { get; set; }

    /// <summary>
    /// 工序结束位置Y坐标
    /// </summary>
    [SugarColumn(ColumnName = "FEPOINTY")]
    public decimal Fepointy { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
