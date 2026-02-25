using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验项目
/// </summary>
[SugarTable("T_QM_INSPECTITEM")]
public class TQmInspectitem : BaseEntity
{
    /// <summary>
    /// 检验项目代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 检验单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 分析方法
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISWAY")]
    public int Fanalysisway { get; set; }

    /// <summary>
    /// 破坏性检验
    /// </summary>
    [SugarColumn(ColumnName = "FDESTORYTEST")]
    public bool Fdestorytest { get; set; }

    /// <summary>
    /// 检验标准
    /// </summary>
    [SugarColumn(ColumnName = "FTESTSTANDARD")]
    public string Fteststandard { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEPLANID")]
    public string Fsampleplanid { get; set; } = string.Empty;

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FTESTLEVEL")]
    public int Ftestlevel { get; set; }

    /// <summary>
    /// 严格度
    /// </summary>
    [SugarColumn(ColumnName = "FSTRINGENCY")]
    public int Fstringency { get; set; }

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL")]
    public string Faql { get; set; } = string.Empty;

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
    /// 检验仪器内码
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOOLID")]
    public string Finstoolid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 检验仪器分组
    /// </summary>
    [SugarColumn(ColumnName = "FTESTINSTRUMENT")]
    public string Ftestinstrument { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准表达式
    /// </summary>
    [SugarColumn(ColumnName = "FCOMSTANDARD")]
    public string Fcomstandard { get; set; } = string.Empty;
}
