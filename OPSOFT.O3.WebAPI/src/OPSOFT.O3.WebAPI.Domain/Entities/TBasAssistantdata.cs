using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 辅助资料类别
/// </summary>
[SugarTable("T_BAS_ASSISTANTDATA")]
public class TBasAssistantdata : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 业务领域内码
    /// </summary>
    [SugarColumn(ColumnName = "FTOPCLASSID")]
    public string Ftopclassid { get; set; } = string.Empty;

    /// <summary>
    /// 上级类别
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTID")]
    public string Fparentid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助资料类别代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 辅助资料类别名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 辅助资料类别描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 系统预设
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// O3系统自用
    /// </summary>
    [SugarColumn(ColumnName = "FISO3USE")]
    public bool Fiso3use { get; set; }

    /// <summary>
    /// 审核人（IApprovable）
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期（IApprovable）
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; }
}
