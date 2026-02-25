using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统表单信息表
/// </summary>
[SugarTable("SYS_BILLTEMPLATE")]
public class SysBillTemplate : BaseEntity
{
    /// <summary>
    /// 表单代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 单据标题
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTITLE")]
    public string Fbilltitle { get; set; } = string.Empty;

    /// <summary>
    /// 序时簿标题
    /// </summary>
    [SugarColumn(ColumnName = "FLISTTITLE")]
    public string Flisttitle { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 表单标识字段
    /// </summary>
    [SugarColumn(ColumnName = "FDISPLAYCOLUMN")]
    public string Fdisplaycolumn { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;
}
