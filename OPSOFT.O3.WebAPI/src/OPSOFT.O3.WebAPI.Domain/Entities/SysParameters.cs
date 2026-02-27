using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统参数表
/// </summary>
[SugarTable("SYS_PARAMETERS")]
public class SysParameters : BaseEntity
{
    /// <summary>
    /// 参数代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 参数名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 参数名称(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;

    /// <summary>
    /// 参数名称(第二语言)
    /// </summary>
    [SugarColumn(ColumnName = "FTNAME", IsNullable = true)]
    public string FTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统默认
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT", IsNullable = true)]
    public bool? ISDEFAULT { get; set; }

    /// <summary>
    /// 数据类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE", IsNullable = true)]
    public string FTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 参数名称(第三语言)
    /// </summary>
    [SugarColumn(ColumnName = "FENAME", IsNullable = true)]
    public string FENAME { get; set; } = string.Empty;

    /// <summary>
    /// 参数值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE", IsNullable = true)]
    public string FVALUE { get; set; } = string.Empty;
}
