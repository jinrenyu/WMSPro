using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工单派工管理-承包/维修商
/// </summary>
[SugarTable("T_ENG_WORKORDERMANAGEENTRY7")]
public class TEngWorkordermanageentry7 : BaseEntity
{
    /// <summary>
    /// 合同内码
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACTID")]
    public string Fcontractid { get; set; } = string.Empty;

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
