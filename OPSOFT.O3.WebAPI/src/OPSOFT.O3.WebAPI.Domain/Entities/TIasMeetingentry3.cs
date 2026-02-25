using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 会议管理-任务项第二、三负责人
/// </summary>
[SugarTable("T_IAS_MEETINGENTRY3")]
public class TIasMeetingentry3 : BaseEntity
{
    /// <summary>
    /// 职员内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
