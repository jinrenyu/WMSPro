using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 序时簿报表模板
/// </summary>
[SugarTable("SYS_BILLTEMPLATEENTRY5")]
public class SysBilltemplateentry5 : BaseEntity
{
    /// <summary>
    /// 模板代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 模板描述
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTION")]
    public string Fcaption { get; set; } = string.Empty;

    /// <summary>
    /// 模板描述
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTIONGB")]
    public string Fcaptiongb { get; set; } = string.Empty;

    /// <summary>
    /// 模板描述
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTIONTW")]
    public string Fcaptiontw { get; set; } = string.Empty;

    /// <summary>
    /// 模板描述
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTIONEN")]
    public string Fcaptionen { get; set; } = string.Empty;

    /// <summary>
    /// 报表模板
    /// </summary>
    [SugarColumn(ColumnName = "FTEMPLATE")]
    public byte[]? Ftemplate { get; set; }

    /// <summary>
    /// 是否默认模板
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULT")]
    public bool Fisdefault { get; set; }

    /// <summary>
    /// 是否打印成PDF
    /// </summary>
    [SugarColumn(ColumnName = "FISPDF")]
    public bool Fispdf { get; set; }

    /// <summary>
    /// 自定义SQL
    /// </summary>
    [SugarColumn(ColumnName = "FSQL")]
    public string Fsql { get; set; } = string.Empty;

    /// <summary>
    /// 是否自定义SQL
    /// </summary>
    [SugarColumn(ColumnName = "FISCUSTOMSQL")]
    public bool Fiscustomsql { get; set; }

    /// <summary>
    /// 选择模板条件
    /// </summary>
    [SugarColumn(ColumnName = "FSTRWHERE")]
    public string Fstrwhere { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
