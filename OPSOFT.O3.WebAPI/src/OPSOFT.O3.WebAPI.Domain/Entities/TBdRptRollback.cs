using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 汇报回退
/// </summary>
[SugarTable("T_BD_RPTROLLBACK")]
public class TBdRptrollback : BaseEntity
{
    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 工作时段
    /// </summary>
    [SugarColumn(ColumnName = "FTIMESPAN")]
    public int Ftimespan { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "FOPTIME")]
    public DateTime? Foptime { get; set; }

    /// <summary>
    /// 操作人
    /// </summary>
    [SugarColumn(ColumnName = "FOPID")]
    public string Fopid { get; set; } = string.Empty;

    /// <summary>
    /// 回滚语句
    /// </summary>
    [SugarColumn(ColumnName = "FSQL")]
    public string Fsql { get; set; } = string.Empty;

    /// <summary>
    /// 是否已执行
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTED")]
    public bool Fexecuted { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTETIME")]
    public DateTime? Fexecutetime { get; set; }

    /// <summary>
    /// 执行人
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// 关键值
    /// </summary>
    [SugarColumn(ColumnName = "FKEY")]
    public string Fkey { get; set; } = string.Empty;

    /// <summary>
    /// 表名称
    /// </summary>
    [SugarColumn(ColumnName = "FTABLENAME")]
    public string Ftablename { get; set; } = string.Empty;

    /// <summary>
    /// 回退方式
    /// </summary>
    [SugarColumn(ColumnName = "FROLLBACKTYPE")]
    public int Frollbacktype { get; set; }

    /// <summary>
    /// 汇报内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID")]
    public string Freportid { get; set; } = string.Empty;

    /// <summary>
    /// 时段ID
    /// </summary>
    [SugarColumn(ColumnName = "FTIMESPANID")]
    public string Ftimespanid { get; set; } = string.Empty;
}
