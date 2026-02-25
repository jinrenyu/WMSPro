using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 会议管理-关联会议
/// </summary>
[SugarTable("T_IAS_MEETINGENTRY1")]
public class TIasMeetingentry1 : BaseEntity
{
    /// <summary>
    /// 关联会议内码
    /// </summary>
    [SugarColumn(ColumnName = "FRELEVANCEID")]
    public string Frelevanceid { get; set; } = string.Empty;

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
