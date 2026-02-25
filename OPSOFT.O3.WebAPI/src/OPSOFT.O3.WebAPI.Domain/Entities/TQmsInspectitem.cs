using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验项目
/// </summary>
[SugarTable("T_QMS_INSPECTITEM")]
public class TQmsInspectitem : BaseEntity
{
    /// <summary>
    /// 检验项目编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 分析方法
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISWAY")]
    public string Fanalysisway { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEPLANID")]
    public string Fsampleplanid { get; set; } = string.Empty;

    /// <summary>
    /// 破坏性检验
    /// </summary>
    [SugarColumn(ColumnName = "FDESTORYTEST")]
    public bool Fdestorytest { get; set; }

    /// <summary>
    /// 检验项目分组
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONITEMS")]
    public string Finspectionitems { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FTESTLEVEL")]
    public string Ftestlevel { get; set; } = string.Empty;

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
    /// 规格下限
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIFICATIONSMIN")]
    public decimal Fspecificationsmin { get; set; }

    /// <summary>
    /// 规格上限
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIFICATIONSMAX")]
    public decimal Fspecificationsmax { get; set; }

    /// <summary>
    /// 规格单位
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIFICATIONUNIT")]
    public string Fspecificationunit { get; set; } = string.Empty;

    /// <summary>
    /// 检验仪器
    /// </summary>
    [SugarColumn(ColumnName = "FTESTINSTRUMENT")]
    public string Ftestinstrument { get; set; } = string.Empty;

    /// <summary>
    /// 对应数值
    /// </summary>
    [SugarColumn(ColumnName = "FRIGHTINSTRUMENT")]
    public string Frightinstrument { get; set; } = string.Empty;

    /// <summary>
    /// 自定义方案
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMSCHEME")]
    public string Fcustomscheme { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
