using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户序时簿字段信息设定
/// </summary>
[SugarTable("SYS_USERLISTTEMPLATE")]
public class SysUserListTemplate : BaseEntity
{
    /// <summary>
    /// 功能代码
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSTYPEID")]
    public string Fclasstypeid { get; set; } = string.Empty;

    /// <summary>
    /// 序时簿或是表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 用户帐号
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// 显示顺序
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWINDEX")]
    public int Fshowindex { get; set; }

    /// <summary>
    /// 此单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 字段说明
    /// </summary>
    [SugarColumn(ColumnName = "FHEADCAPTION")]
    public string Fheadcaption { get; set; } = string.Empty;

    /// <summary>
    /// 对应的字段实体
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDNAME")]
    public string Ffieldname { get; set; } = string.Empty;

    /// <summary>
    /// 显示宽度
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public decimal Fwidth { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    [SugarColumn(ColumnName = "FISVISIBLE")]
    public bool Fisvisible { get; set; }
}
