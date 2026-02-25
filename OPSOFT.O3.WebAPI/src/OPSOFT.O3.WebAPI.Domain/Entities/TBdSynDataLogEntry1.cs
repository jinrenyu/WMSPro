using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 数据映射单据行字段日志
/// </summary>
[SugarTable("T_BD_SynDataLogEntry1")]
public class TBdSynDataLogEntry1 : BaseEntryEntity
{
    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string FBodyId { get; set; } = string.Empty;

    /// <summary>
    /// 日志日期
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTIONTIME")]
    public DateTime? FExecutionTime { get; set; }

    /// <summary>
    /// 数据映射内码
    /// </summary>
    [SugarColumn(ColumnName = "FFIELD")]
    public string FField { get; set; } = string.Empty;

    /// <summary>
    /// 日志信息
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTIONRESULT")]
    public string FExecutionResult { get; set; } = string.Empty;
}
