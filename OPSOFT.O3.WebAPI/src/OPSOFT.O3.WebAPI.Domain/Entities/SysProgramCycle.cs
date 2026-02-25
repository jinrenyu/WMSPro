using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 明细功能信息
/// </summary>
[SugarTable("SYS_PROGRAMCYCLE")]
public class SysProgramCycle : BaseEntity
{
    /// <summary>
    /// 功能代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 功能名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 父阶菜单
    /// </summary>
    [SugarColumn(ColumnName = "FPARENT")]
    public string Fparent { get; set; } = string.Empty;

    /// <summary>
    /// 父阶菜单名称
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTNA")]
    public string Fparentna { get; set; } = string.Empty;

    /// <summary>
    /// 父阶菜单序号
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTSEQ")]
    public string Fparentseq { get; set; } = string.Empty;

    /// <summary>
    /// 显示序号
    /// </summary>
    [SugarColumn(ColumnName = "RO_CSEQ")]
    public string RoCseq { get; set; } = string.Empty;

    /// <summary>
    /// 是否可见
    /// </summary>
    [SugarColumn(ColumnName = "SHOW_YN")]
    public string ShowYn { get; set; } = string.Empty;

    /// <summary>
    /// 功能类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统默认
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// 窗口显示模式
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWTYPE")]
    public string Fshowtype { get; set; } = string.Empty;

    /// <summary>
    /// 分组名称
    /// </summary>
    [SugarColumn(ColumnName = "FGNAME")]
    public string Fgname { get; set; } = string.Empty;

    /// <summary>
    /// 程序类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRTYPE")]
    public string Fprtype { get; set; } = string.Empty;
}
