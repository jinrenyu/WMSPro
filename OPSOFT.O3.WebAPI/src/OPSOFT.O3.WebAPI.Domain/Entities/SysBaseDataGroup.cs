using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 基本资料分组共用表结构
/// </summary>
[SugarTable("SYS_BASEDATAGROUP")]
public class SysBaseDataGroup : BaseEntity
{
    /// <summary>
    /// 系统功能
    /// </summary>
    [SugarColumn(ColumnName = "FPRGKEY")]
    public string Fprgkey { get; set; } = string.Empty;

    /// <summary>
    /// 分组代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 分组名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 分组名称(第一语言)
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;

    /// <summary>
    /// 分组名称(第二语言)
    /// </summary>
    [SugarColumn(ColumnName = "FTNAME")]
    public string Ftname { get; set; } = string.Empty;

    /// <summary>
    /// 分组名称(第三语言)
    /// </summary>
    [SugarColumn(ColumnName = "FENAME")]
    public string Fename { get; set; } = string.Empty;

    /// <summary>
    /// 父阶内码
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTID")]
    public string Fparentid { get; set; } = string.Empty;

    /// <summary>
    /// 长编码
    /// </summary>
    [SugarColumn(ColumnName = "FFULLNUMBER")]
    public string Ffullnumber { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;
}
