using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// GB2828抽样水平加严
/// </summary>
[SugarTable("T_BD_GB2828SAMPLINGPLANJY")]
public class TBdGb2828samplingplanjy : BaseEntity
{
    /// <summary>
    /// 字码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 样本量
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLESIZE")]
    public decimal Fsamplesize { get; set; }

    /// <summary>
    /// 0.01ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTZEROONE")]
    public string Facrezeropointzeroone { get; set; } = string.Empty;

    /// <summary>
    /// 0.015ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTZEROONEFIV")]
    public string Facrezeropointzeroonefiv { get; set; } = string.Empty;

    /// <summary>
    /// 0.025ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTZEROTWOFIV")]
    public string Facrezeropointzerotwofiv { get; set; } = string.Empty;

    /// <summary>
    /// 0.04ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTZEROFOU")]
    public string Facrezeropointzerofou { get; set; } = string.Empty;

    /// <summary>
    /// 0.065ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTZEROFOUFIV")]
    public string Facrezeropointzerofoufiv { get; set; } = string.Empty;

    /// <summary>
    /// 0.1ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTONE")]
    public string Facrezeropointone { get; set; } = string.Empty;

    /// <summary>
    /// 0.15ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTONEFIV")]
    public string Facrezeropointonefiv { get; set; } = string.Empty;

    /// <summary>
    /// 0.25ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTTWOFIV")]
    public string Facrezeropointtwofiv { get; set; } = string.Empty;

    /// <summary>
    /// 0.4ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTFOU")]
    public string Facrezeropointfou { get; set; } = string.Empty;

    /// <summary>
    /// 0.65ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREZEROPOINTSIXFIV")]
    public string Facrezeropointsixfiv { get; set; } = string.Empty;

    /// <summary>
    /// 1.0ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREONE")]
    public string Facreone { get; set; } = string.Empty;

    /// <summary>
    /// 1.5ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREONEPOINTFIV")]
    public string Facreonepointfiv { get; set; } = string.Empty;

    /// <summary>
    /// 2.5ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACRETWOPOINTFIV")]
    public string Facretwopointfiv { get; set; } = string.Empty;

    /// <summary>
    /// 4.0ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREFOUPOINT")]
    public string Facrefoupoint { get; set; } = string.Empty;

    /// <summary>
    /// 6.5ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACRESIXPOINTFIV")]
    public string Facresixpointfiv { get; set; } = string.Empty;

    /// <summary>
    /// 10ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACRETEN")]
    public string Facreten { get; set; } = string.Empty;

    /// <summary>
    /// 15ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREFIVTEEN")]
    public string Facrefivteen { get; set; } = string.Empty;

    /// <summary>
    /// 25ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACRETWFIV")]
    public string Facretwfiv { get; set; } = string.Empty;

    /// <summary>
    /// 40ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREFOUTY")]
    public string Facrefouty { get; set; } = string.Empty;

    /// <summary>
    /// 65ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACRESIXTYFIV")]
    public string Facresixtyfiv { get; set; } = string.Empty;

    /// <summary>
    /// 100ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREHUNDRED")]
    public string Facrehundred { get; set; } = string.Empty;

    /// <summary>
    /// 150ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREONEHUNDREDFIVTY")]
    public string Facreonehundredfivty { get; set; } = string.Empty;

    /// <summary>
    /// 250ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACRETWOHUNDREDFIVTY")]
    public string Facretwohundredfivty { get; set; } = string.Empty;

    /// <summary>
    /// 400ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREFOUHUNDRED")]
    public string Facrefouhundred { get; set; } = string.Empty;

    /// <summary>
    /// 650ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACRESIXHUNDREDFIVTY")]
    public string Facresixhundredfivty { get; set; } = string.Empty;

    /// <summary>
    /// 1000ACRE
    /// </summary>
    [SugarColumn(ColumnName = "FACREONETHOUSAND")]
    public string Facreonethousand { get; set; } = string.Empty;

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
