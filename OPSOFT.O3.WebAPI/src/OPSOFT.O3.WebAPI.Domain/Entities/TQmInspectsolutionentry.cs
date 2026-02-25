using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验方案表体-检验项目
/// </summary>
[SugarTable("T_QM_INSPECTSOLUTIONENTRY")]
public class TQmInspectsolutionentry : BaseEntity
{
    /// <summary>
    /// 检验项目
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTITEMID")]
    public string Finspectitemid { get; set; } = string.Empty;

    /// <summary>
    /// 分析方法
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISWAY")]
    public int Fanalysisway { get; set; }

    /// <summary>
    /// 缺陷等级
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTLEVEL")]
    public int Ffaultlevel { get; set; }

    /// <summary>
    /// 质检结果来源
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKRESSOURCE")]
    public int Fcheckressource { get; set; }

    /// <summary>
    /// 重点检查
    /// </summary>
    [SugarColumn(ColumnName = "FKEYPOINTTEST")]
    public bool Fkeypointtest { get; set; }

    /// <summary>
    /// 破坏性检验
    /// </summary>
    [SugarColumn(ColumnName = "FDESTORYTEST")]
    public bool Fdestorytest { get; set; }

    /// <summary>
    /// 检验方法
    /// </summary>
    [SugarColumn(ColumnName = "FTESTWAY")]
    public string Ftestway { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准
    /// </summary>
    [SugarColumn(ColumnName = "FTESTSTANDARD")]
    public string Fteststandard { get; set; } = string.Empty;

    /// <summary>
    /// 检验仪器
    /// </summary>
    [SugarColumn(ColumnName = "FTESTINSTRUMENT")]
    public string Ftestinstrument { get; set; } = string.Empty;

    /// <summary>
    /// FAQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL")]
    public string Faql { get; set; } = string.Empty;

    /// <summary>
    /// 严格度
    /// </summary>
    [SugarColumn(ColumnName = "FSTRINGENCY")]
    public int Fstringency { get; set; }

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FTESTLEVEL")]
    public int Ftestlevel { get; set; }

    /// <summary>
    /// 常用单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 比较符
    /// </summary>
    [SugarColumn(ColumnName = "FCMPOPERATORS")]
    public int Fcmpoperators { get; set; }

    /// <summary>
    /// 目标值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE")]
    public decimal Fvalue { get; set; }

    /// <summary>
    /// 上限
    /// </summary>
    [SugarColumn(ColumnName = "FUPPERLIMIT")]
    public decimal Fupperlimit { get; set; }

    /// <summary>
    /// 下限
    /// </summary>
    [SugarColumn(ColumnName = "FLWERLIMIT")]
    public decimal Flwerlimit { get; set; }

    /// <summary>
    /// 上偏差
    /// </summary>
    [SugarColumn(ColumnName = "FUPPERDEVIATION")]
    public decimal Fupperdeviation { get; set; }

    /// <summary>
    /// 下偏差
    /// </summary>
    [SugarColumn(ColumnName = "FLWERDEVIATION")]
    public decimal Flwerdeviation { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
    /// 检验次数
    /// </summary>
    [SugarColumn(ColumnName = "FTESTCOUNT")]
    public decimal Ftestcount { get; set; }

    /// <summary>
    /// 标准表达式
    /// </summary>
    [SugarColumn(ColumnName = "FSTDEXPRESS")]
    public string Fstdexpress { get; set; } = string.Empty;

    /// <summary>
    /// 检验仪器分组
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOOLGROUPID")]
    public string Finstoolgroupid { get; set; } = string.Empty;

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
