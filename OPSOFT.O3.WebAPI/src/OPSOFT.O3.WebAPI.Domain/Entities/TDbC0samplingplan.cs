using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// C=0抽样方案
/// </summary>
[SugarTable("T_DB_C0SAMPLINGPLAN")]
public class TDbC0samplingplan : BaseEntity
{
    /// <summary>
    /// 起始
    /// </summary>
    [SugarColumn(ColumnName = "FSTART")]
    public decimal Fstart { get; set; }

    /// <summary>
    /// 结束
    /// </summary>
    [SugarColumn(ColumnName = "FEND")]
    public decimal Fend { get; set; }

    /// <summary>
    /// 0.01
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTZEROONE")]
    public string Fzeropointzeroone { get; set; } = string.Empty;

    /// <summary>
    /// 0.015
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTZEROONEFIVE")]
    public string Fzeropointzeroonefive { get; set; } = string.Empty;

    /// <summary>
    /// 0.025
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTZEROTWOFIVE")]
    public string Fzeropointzerotwofive { get; set; } = string.Empty;

    /// <summary>
    /// 0.04
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTZEROFOUR")]
    public string Fzeropointzerofour { get; set; } = string.Empty;

    /// <summary>
    /// 0.065
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTZEROSIXFIVE")]
    public string Fzeropointzerosixfive { get; set; } = string.Empty;

    /// <summary>
    /// 0.1
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTONE")]
    public string Fzeropointone { get; set; } = string.Empty;

    /// <summary>
    /// 0.15
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTONEFIVE")]
    public string Fzeropointonefive { get; set; } = string.Empty;

    /// <summary>
    /// 0.25
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTTWOFIVE")]
    public string Fzeropointtwofive { get; set; } = string.Empty;

    /// <summary>
    /// 0.4
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTFOUR")]
    public string Fzeropointfour { get; set; } = string.Empty;

    /// <summary>
    /// 0.65
    /// </summary>
    [SugarColumn(ColumnName = "FZEROPOINTSIXFIVE")]
    public string Fzeropointsixfive { get; set; } = string.Empty;

    /// <summary>
    /// 1.0
    /// </summary>
    [SugarColumn(ColumnName = "FONE")]
    public string Fone { get; set; } = string.Empty;

    /// <summary>
    /// 1.5
    /// </summary>
    [SugarColumn(ColumnName = "FONEPOINTFIV")]
    public string Fonepointfiv { get; set; } = string.Empty;

    /// <summary>
    /// 2.5
    /// </summary>
    [SugarColumn(ColumnName = "FTWOPOINTFIV")]
    public string Ftwopointfiv { get; set; } = string.Empty;

    /// <summary>
    /// 4.0
    /// </summary>
    [SugarColumn(ColumnName = "FFOUPOINT")]
    public string Ffoupoint { get; set; } = string.Empty;

    /// <summary>
    /// 6.5
    /// </summary>
    [SugarColumn(ColumnName = "FSIXPOINTFIV")]
    public string Fsixpointfiv { get; set; } = string.Empty;

    /// <summary>
    /// 10
    /// </summary>
    [SugarColumn(ColumnName = "FTEN")]
    public string Ften { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

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
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
