using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工艺流程表体-路线
/// </summary>
[SugarTable("T_ENG_ROUTEENTRY1")]
public class TEngRouteentry1 : BaseEntity
{
    /// <summary>
    /// 工艺工序明细内码-左边
    /// </summary>
    [SugarColumn(ColumnName = "FLPRODETAILID")]
    public string Flprodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工艺工序明细内码-右边
    /// </summary>
    [SugarColumn(ColumnName = "FRPRODETAILID")]
    public string Frprodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 关系开始绘制的位置X坐标
    /// </summary>
    [SugarColumn(ColumnName = "FBEGINPOINTX")]
    public decimal Fbeginpointx { get; set; }

    /// <summary>
    /// 关系开始绘制的位置Y坐标
    /// </summary>
    [SugarColumn(ColumnName = "FBEGINPOINTY")]
    public decimal Fbeginpointy { get; set; }

    /// <summary>
    /// 关系结束绘制的位置X坐标
    /// </summary>
    [SugarColumn(ColumnName = "FENDPOINTX")]
    public decimal Fendpointx { get; set; }

    /// <summary>
    /// 关系结束绘制的位置Y坐标
    /// </summary>
    [SugarColumn(ColumnName = "FENDPOINTY")]
    public decimal Fendpointy { get; set; }

    /// <summary>
    /// 关系线颜色
    /// </summary>
    [SugarColumn(ColumnName = "FLINECOLOR")]
    public string Flinecolor { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
