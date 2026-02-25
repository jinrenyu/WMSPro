using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 业务员
/// </summary>
[SugarTable("T_BD_OPERATORENTRY")]
public class TBdOperatorentry : BaseEntity
{
    /// <summary>
    /// 业务员代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 业务员名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 员工任岗内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTAFFID")]
    public string Fstaffid { get; set; } = string.Empty;

    /// <summary>
    /// 员工代码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPNUMBER")]
    public string Fempnumber { get; set; } = string.Empty;

    /// <summary>
    /// 用户账号
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public string UserId { get; set; } = string.Empty;

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
