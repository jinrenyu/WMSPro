using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统主模块信息
/// </summary>
[SugarTable("SYS_ROTATEINFO")]
public class SysRotateInfo : BaseEntity
{
    /// <summary>
    /// 模块代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 模块名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 模块名称(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;

    /// <summary>
    /// 显示序号
    /// </summary>
    [SugarColumn(ColumnName = "RO_CSEQ", IsNullable = true)]
    public string RO_CSEQ { get; set; } = string.Empty;

    /// <summary>
    /// 模块名称(第二语言)
    /// </summary>
    [SugarColumn(ColumnName = "FTNAME", IsNullable = true)]
    public string FTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统默认
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT", IsNullable = true)]
    public bool? ISDEFAULT { get; set; }

    /// <summary>
    /// 模块名称(第三语言)
    /// </summary>
    [SugarColumn(ColumnName = "FENAME", IsNullable = true)]
    public string FENAME { get; set; } = string.Empty;

    /// <summary>
    /// 图标信息
    /// </summary>
    [SugarColumn(ColumnName = "FIMAGE", IsNullable = true)]
    public string FIMAGE { get; set; } = string.Empty;

    /// <summary>
    /// 是否可见
    /// </summary>
    [SugarColumn(ColumnName = "SHOW_YN", IsNullable = true)]
    public string SHOW_YN { get; set; } = string.Empty;

    /// <summary>
    /// 权限数值
    /// </summary>
    [SugarColumn(ColumnName = "RO_BITMAP", IsNullable = true)]
    public int? RO_BITMAP { get; set; }
}
