using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 班组群
/// </summary>
[SugarTable("T_PRD_TEAMGROUP")]
public class TPrdTeamgroup : BaseEntity
{
    /// <summary>
    /// 班组群代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 班组群名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 群类型
    /// </summary>
    [SugarColumn(ColumnName = "FGROUPTYPE")]
    public string Fgrouptype { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPT")]
    public string Fdept { get; set; } = string.Empty;

    /// <summary>
    /// 班组长
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMLEADER")]
    public string Fteamleader { get; set; } = string.Empty;

    /// <summary>
    /// 推送到钉钉的ID
    /// </summary>
    [SugarColumn(ColumnName = "FDDID")]
    public string Fddid { get; set; } = string.Empty;

    /// <summary>
    /// 群备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
