using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 数据映射单据日志
/// </summary>
[SugarTable("T_BD_SynDataLog")]
public class TBdSynDataLog : BaseEntity
{
    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string FBillNo { get; set; } = string.Empty;

    /// <summary>
    /// 日志日期
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTIONTIME")]
    public DateTime? FExecutionTime { get; set; }

    /// <summary>
    /// 数据映射内码
    /// </summary>
    [SugarColumn(ColumnName = "FSYNDATAMAPPINGID")]
    public string FSynDataMappingId { get; set; } = string.Empty;

    /// <summary>
    /// 同步状态
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTIONSTATUS")]
    public bool FExecutionStatus { get; set; }

    /// <summary>
    /// 日志信息
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTIONRESULT")]
    public string FExecutionResult { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FSYNTYPE")]
    public string FSynType { get; set; } = string.Empty;
}
