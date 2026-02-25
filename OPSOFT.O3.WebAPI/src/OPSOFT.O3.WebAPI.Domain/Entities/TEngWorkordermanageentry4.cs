using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工单派工管理-工/检具
/// </summary>
[SugarTable("T_ENG_WORKORDERMANAGEENTRY4")]
public class TEngWorkordermanageentry4 : BaseEntity
{
    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public int Fcount { get; set; }

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
