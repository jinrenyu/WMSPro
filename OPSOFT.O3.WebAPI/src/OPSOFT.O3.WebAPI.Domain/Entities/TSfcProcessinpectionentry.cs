using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 过程检验表体-检验项目
/// </summary>
[SugarTable("T_SFC_PROCESSINPECTIONENTRY")]
public class TSfcProcessinpectionentry : BaseEntity
{
    /// <summary>
    /// 检验项目
    /// </summary>
    [SugarColumn(ColumnName = "FTESTPROJECTID")]
    public string Ftestprojectid { get; set; } = string.Empty;

    /// <summary>
    /// 分析方法
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISWAY")]
    public int Fanalysisway { get; set; }

    /// <summary>
    /// 检验值(定性
    /// </summary>
    [SugarColumn(ColumnName = "FQUALTESTVALUE")]
    public string Fqualtestvalue { get; set; } = string.Empty;

    /// <summary>
    /// 检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FTESTRESULT", IsNullable = true)]
    public int? FTESTRESULT { get; set; }

    /// <summary>
    /// 缺陷等级
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTLEVEL", IsNullable = true)]
    public int? FFAULTLEVEL { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 破坏性检验
    /// </summary>
    [SugarColumn(ColumnName = "FDESTORYTEST", IsNullable = true)]
    public bool? FDESTORYTEST { get; set; }

    /// <summary>
    /// 质量标准
    /// </summary>
    [SugarColumn(ColumnName = "FTESTSTANDARDID", IsNullable = true)]
    public string FTESTSTANDARDID { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 检验方法
    /// </summary>
    [SugarColumn(ColumnName = "FTESTWAYID", IsNullable = true)]
    public string FTESTWAYID { get; set; } = string.Empty;

    /// <summary>
    /// 检验值(定量)
    /// </summary>
    [SugarColumn(ColumnName = "FQUANTESTVALUE", IsNullable = true)]
    public decimal? FQUANTESTVALUE { get; set; }

    /// <summary>
    /// 上限
    /// </summary>
    [SugarColumn(ColumnName = "FUPPERLIMIT", IsNullable = true)]
    public decimal? FUPPERLIMIT { get; set; }

    /// <summary>
    /// 重点检查
    /// </summary>
    [SugarColumn(ColumnName = "FKEYPOINTTEST", IsNullable = true)]
    public bool? FKEYPOINTTEST { get; set; }

    /// <summary>
    /// 严格度
    /// </summary>
    [SugarColumn(ColumnName = "FSTRINGENCY", IsNullable = true)]
    public int? FSTRINGENCY { get; set; }

    /// <summary>
    /// 下限
    /// </summary>
    [SugarColumn(ColumnName = "FLWERLIMIT", IsNullable = true)]
    public decimal? FLWERLIMIT { get; set; }

    /// <summary>
    /// 目标值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE", IsNullable = true)]
    public decimal? FVALUE { get; set; }

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL", IsNullable = true)]
    public string FAQL { get; set; } = string.Empty;

    /// <summary>
    /// 检验值(其他)
    /// </summary>
    [SugarColumn(ColumnName = "FOTHERTESTVALUE", IsNullable = true)]
    public decimal? FOTHERTESTVALUE { get; set; }

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FTESTLEVEL", IsNullable = true)]
    public int? FTESTLEVEL { get; set; }

    /// <summary>
    /// 检验仪器
    /// </summary>
    [SugarColumn(ColumnName = "FTESTINSTRUMENTID", IsNullable = true)]
    public string FTESTINSTRUMENTID { get; set; } = string.Empty;
}
