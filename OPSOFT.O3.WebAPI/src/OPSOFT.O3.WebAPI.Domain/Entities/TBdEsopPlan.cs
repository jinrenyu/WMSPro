using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP计划
/// </summary>
[SugarTable("T_BD_ESOPPLAN")]
public class TBdEsopplan : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 使用状态
    /// </summary>
    [SugarColumn(ColumnName = "FUSESTATUS")]
    public int Fusestatus { get; set; }

    /// <summary>
    /// 当前状态
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENTSTATUS")]
    public int Fcurrentstatus { get; set; }

    /// <summary>
    /// 模板内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOULDID")]
    public string Fmouldid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
