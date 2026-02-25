using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报-记录单-员工
/// </summary>
[SugarTable("T_SFC_REPAIRLOG_EMPLOYEE")]
public class TSfcRepairlogEmployee : BaseEntity
{
    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 工种内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTYPEID")]
    public string Fworktypeid { get; set; } = string.Empty;

    /// <summary>
    /// 上岗时间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTIME")]
    public DateTime? Fworktime { get; set; }

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
