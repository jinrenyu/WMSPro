using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修单
/// </summary>
[SugarTable("T_ENG_MAINTENANCE")]
public class TEngMaintenance : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 作业内码
    /// </summary>
    [SugarColumn(ColumnName = "FJOBCLASSID")]
    public string Fjobclassid { get; set; } = string.Empty;

    /// <summary>
    /// 模治具内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOULDID")]
    public string Fmouldid { get; set; } = string.Empty;

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 模具延长寿命
    /// </summary>
    [SugarColumn(ColumnName = "FADDUSELIFE")]
    public int Fadduselife { get; set; }

    /// <summary>
    /// 维修人内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

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
    /// 状况描述
    /// </summary>
    [SugarColumn(ColumnName = "FCONDITION")]
    public string Fcondition { get; set; } = string.Empty;

    /// <summary>
    /// 异常描述
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTCASE")]
    public string Ffaultcase { get; set; } = string.Empty;

    /// <summary>
    /// 修理内容
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRSITUATIOR")]
    public string Frepairsituatior { get; set; } = string.Empty;

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

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
