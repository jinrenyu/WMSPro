using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工艺流程表体-工序
/// </summary>
[SugarTable("T_ENG_ROUTEENTRY")]
public class TEngRouteentry : BaseEntity
{
    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流水号
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public string Fseq { get; set; } = string.Empty;

    /// <summary>
    /// 工序绘制的位置X坐标
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTX")]
    public int Fpointx { get; set; }

    /// <summary>
    /// 工序绘制的位置Y坐标
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTY")]
    public int Fpointy { get; set; }

    /// <summary>
    /// 工序颜色
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSCOLOR")]
    public string Fprocesscolor { get; set; } = string.Empty;

    /// <summary>
    /// 序列类型
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSTYPE")]
    public int Fprocesstype { get; set; }

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
