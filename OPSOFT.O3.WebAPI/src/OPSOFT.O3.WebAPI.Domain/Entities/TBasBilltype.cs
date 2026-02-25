using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单据类型
/// </summary>
[SugarTable("T_BAS_BILLTYPE")]
public class TBasBilltype : BaseEntity
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
    /// 单据来源
    /// </summary>
    [SugarColumn(ColumnName = "FBILLFORMID")]
    public string Fbillformid { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// Cloud编号
    /// </summary>
    [SugarColumn(ColumnName = "FSHOTNUMBER")]
    public string Fshotnumber { get; set; } = string.Empty;

    /// <summary>
    /// 是否同步数据
    /// </summary>
    [SugarColumn(ColumnName = "FISSYN")]
    public bool Fissyn { get; set; }

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

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;
}
