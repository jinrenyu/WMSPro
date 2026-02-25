using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP提交
/// </summary>
[SugarTable("T_BD_ESOPFRMSUBMIT")]
public class TBdEsopfrmsubmit : BaseEntity
{
    /// <summary>
    /// 确认部门
    /// </summary>
    [SugarColumn(ColumnName = "FSUREDPID")]
    public string Fsuredpid { get; set; } = string.Empty;

    /// <summary>
    /// 确认人
    /// </summary>
    [SugarColumn(ColumnName = "FSUREID")]
    public string Fsureid { get; set; } = string.Empty;

    /// <summary>
    /// 审核部门
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYDPID")]
    public string Fverifydpid { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYID")]
    public string Fverifyid { get; set; } = string.Empty;

    /// <summary>
    /// 复核部门
    /// </summary>
    [SugarColumn(ColumnName = "FREVIEWDPID")]
    public string Freviewdpid { get; set; } = string.Empty;

    /// <summary>
    /// 复核人
    /// </summary>
    [SugarColumn(ColumnName = "FREVIEWID")]
    public string Freviewid { get; set; } = string.Empty;

    /// <summary>
    /// 意见
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 提交人
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITID")]
    public string Fsubmitid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
