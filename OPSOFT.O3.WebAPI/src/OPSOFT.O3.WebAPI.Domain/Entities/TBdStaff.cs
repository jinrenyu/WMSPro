using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 员工任岗信息
/// </summary>
[SugarTable("T_BD_STAFF")]
public class TBdStaff : BaseEntity
{
    /// <summary>
    /// 员工内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPLOYEEID")]
    public string FEmployeeId { get; set; } = string.Empty;

    /// <summary>
    /// 岗位内码
    /// </summary>
    [SugarColumn(ColumnName = "FPOSTID")]
    public string FPostId { get; set; } = string.Empty;

    /// <summary>
    /// 任职日期
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTDATE")]
    public DateTime? FStartDate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;
}
