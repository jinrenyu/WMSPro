using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验方案-检验项目
/// </summary>
[SugarTable("T_QMS_INSPECTSOLUTIONENTRY")]
public class TQmsInspectsolutionentry : BaseEntity
{
    /// <summary>
    /// 检验项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROGRAMMEID")]
    public string Fprogrammeid { get; set; } = string.Empty;

    /// <summary>
    /// 分析方法
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISWAY")]
    public string Fanalysisway { get; set; } = string.Empty;

    /// <summary>
    /// 破坏性检验
    /// </summary>
    [SugarColumn(ColumnName = "FDESTORYTEST")]
    public bool Fdestorytest { get; set; }

    /// <summary>
    /// 抽样方案
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTITEMID")]
    public string Finspectitemid { get; set; } = string.Empty;

    /// <summary>
    /// 对应数值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE")]
    public decimal Fvalue { get; set; }

    /// <summary>
    /// 自定义方案选择
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEPLANID")]
    public string Fsampleplanid { get; set; } = string.Empty;

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FTESTLEVELID")]
    public string Ftestlevelid { get; set; } = string.Empty;

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL")]
    public string Faql { get; set; } = string.Empty;

    /// <summary>
    /// 测试次数
    /// </summary>
    [SugarColumn(ColumnName = "FTESTTIMES")]
    public decimal Ftesttimes { get; set; }

    /// <summary>
    /// 检验仪器
    /// </summary>
    [SugarColumn(ColumnName = "FTESTINSTRUMENT")]
    public string Ftestinstrument { get; set; } = string.Empty;

    /// <summary>
    /// 规格上限
    /// </summary>
    [SugarColumn(ColumnName = "FUPPERLIMIT")]
    public decimal Fupperlimit { get; set; }

    /// <summary>
    /// 规格下限
    /// </summary>
    [SugarColumn(ColumnName = "FLWERLIMIT")]
    public decimal Flwerlimit { get; set; }

    /// <summary>
    /// 规格单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

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
