using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 会议管理-与会人员
/// </summary>
[SugarTable("T_IAS_MEETINGENTRY2")]
public class TIasMeetingentry2 : BaseEntity
{
    /// <summary>
    /// 参会人员帐号
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// 指派人
    /// </summary>
    [SugarColumn(ColumnName = "FDESIGNEETOID")]
    public string Fdesigneetoid { get; set; } = string.Empty;

    /// <summary>
    /// 指派时间
    /// </summary>
    [SugarColumn(ColumnName = "FDESIGNEETODATE")]
    public DateTime? Fdesigneetodate { get; set; }

    /// <summary>
    /// 是否到场
    /// </summary>
    [SugarColumn(ColumnName = "FISSHOWUP")]
    public bool Fisshowup { get; set; }

    /// <summary>
    /// 与会者级别
    /// </summary>
    [SugarColumn(ColumnName = "FLEVEL")]
    public string Flevel { get; set; } = string.Empty;

    /// <summary>
    /// 与会者响应
    /// </summary>
    [SugarColumn(ColumnName = "FRESPONSE")]
    public string Fresponse { get; set; } = string.Empty;

    /// <summary>
    /// 会议实际参加时间
    /// </summary>
    [SugarColumn(ColumnName = "SBEGINACTUALTIME")]
    public DateTime? Sbeginactualtime { get; set; }

    /// <summary>
    /// 与会说明
    /// </summary>
    [SugarColumn(ColumnName = "FEXPLANATION")]
    public string Fexplanation { get; set; } = string.Empty;

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
