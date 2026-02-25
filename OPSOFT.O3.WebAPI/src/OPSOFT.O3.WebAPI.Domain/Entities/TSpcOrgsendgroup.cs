using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 消息推送分组
/// </summary>
[SugarTable("T_SPC_ORGSENDGROUP")]
public class TSpcOrgsendgroup : BaseEntity
{
    /// <summary>
    /// 消息推送分组名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 消息推送分组编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;
}
