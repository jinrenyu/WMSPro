using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料检验类型明细
/// </summary>
[SugarTable("T_BD_MATERIELCHECKPROJECT02")]
public class TBdMaterielcheckproject02 : BaseEntity
{
    /// <summary>
    /// 检验项目ID
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKPROJECTID")]
    public string Fcheckprojectid { get; set; } = string.Empty;

    /// <summary>
    /// 来料检验
    /// </summary>
    [SugarColumn(ColumnName = "FINCOMMINGCHECK")]
    public bool Fincommingcheck { get; set; }

    /// <summary>
    /// 超期复检
    /// </summary>
    [SugarColumn(ColumnName = "FOVERDUERECHECK")]
    public bool Foverduerecheck { get; set; }

    /// <summary>
    /// 退料检验
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNCHECK")]
    public bool Freturncheck { get; set; }

    /// <summary>
    /// 在库检验
    /// </summary>
    [SugarColumn(ColumnName = "FINLIBRARYCHECK")]
    public bool Finlibrarycheck { get; set; }

    /// <summary>
    /// 制程检验
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSCHECK")]
    public bool Fprocesscheck { get; set; }

    /// <summary>
    /// 出货检验
    /// </summary>
    [SugarColumn(ColumnName = "FSHIPMENTCHECK")]
    public bool Fshipmentcheck { get; set; }

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
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEAVEL")]
    public string Fcheckleavel { get; set; } = string.Empty;

    /// <summary>
    /// 抽样数量
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLENUM")]
    public decimal Fsamplenum { get; set; }

    /// <summary>
    /// AC
    /// </summary>
    [SugarColumn(ColumnName = "FAC")]
    public decimal Fac { get; set; }

    /// <summary>
    /// RE
    /// </summary>
    [SugarColumn(ColumnName = "FRE")]
    public decimal Fre { get; set; }

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL")]
    public string Faql { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKSTAND")]
    public string Fcheckstand { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具分组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLGROUPID")]
    public string Ftoolgroupid { get; set; } = string.Empty;

    /// <summary>
    /// 检验次数
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKCOUNT")]
    public decimal Fcheckcount { get; set; }

    /// <summary>
    /// 检验方法
    /// </summary>
    [SugarColumn(ColumnName = "FQCMETHOD")]
    public string Fqcmethod { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具内码
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLID")]
    public string Ftoolid { get; set; } = string.Empty;

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
