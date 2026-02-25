using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 模具维修单
/// </summary>
[SugarTable("T_ENG_MOLDMAINTENANCE")]
public class TEngMoldmaintenance : BaseEntity
{
    /// <summary>
    /// 维修完成日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 作业内码
    /// </summary>
    [SugarColumn(ColumnName = "FJOBCLASSID")]
    public string Fjobclassid { get; set; } = string.Empty;

    /// <summary>
    /// 模具内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOULDID")]
    public string Fmouldid { get; set; } = string.Empty;

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPARTMENTID")]
    public string Fdepartmentid { get; set; } = string.Empty;

    /// <summary>
    /// 维修人内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPINFOID")]
    public string Fempinfoid { get; set; } = string.Empty;

    /// <summary>
    /// 模具延长寿命
    /// </summary>
    [SugarColumn(ColumnName = "FADDUSELIFE")]
    public decimal Fadduselife { get; set; }

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
    [SugarColumn(ColumnName = "FREPAIRSITUATION")]
    public string Frepairsituation { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
