using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产任务单工序
/// </summary>
[SugarTable("T_PRD_MOPROCESS")]
public class TPrdMoprocess : BaseEntity
{
    /// <summary>
    /// 工单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public int Fmoentryid { get; set; }

    /// <summary>
    /// 工序代码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 加工时间(分
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTIME")]
    public decimal Fworktime { get; set; }
}
