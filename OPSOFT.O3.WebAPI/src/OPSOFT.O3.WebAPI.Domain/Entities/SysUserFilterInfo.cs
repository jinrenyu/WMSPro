using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户过滤方案
/// </summary>
[SugarTable("SYS_USERFILTERINFO")]
public class SysUserFilterInfo : BaseEntity
{
    /// <summary>
    /// 功能代码
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSTYPEID")]
    public string Fclasstypeid { get; set; } = string.Empty;

    /// <summary>
    /// 方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 是否为默认方案
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULT")]
    public bool Fdefault { get; set; }

    /// <summary>
    /// 是否为当前方案
    /// </summary>
    [SugarColumn(ColumnName = "FACTIVE")]
    public bool Factive { get; set; }

    /// <summary>
    /// 用户帐号
    /// </summary>
    [SugarColumn(ColumnName = "FUSER_ID")]
    public string FuserId { get; set; } = string.Empty;

    /// <summary>
    /// 第一过滤索引号
    /// </summary>
    [SugarColumn(ColumnName = "FDATEINDEX")]
    public string Fdateindex { get; set; } = string.Empty;

    /// <summary>
    /// 第二过滤索引号
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYINDEX")]
    public string Fverifyindex { get; set; } = string.Empty;

    /// <summary>
    /// 第三过滤索引号
    /// </summary>
    [SugarColumn(ColumnName = "FFAILINDEX")]
    public string Ffailindex { get; set; } = string.Empty;

    /// <summary>
    /// 第四过滤索引号
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSEINDEX")]
    public string Fcloseindex { get; set; } = string.Empty;

    /// <summary>
    /// 选择确认时是否保存
    /// </summary>
    [SugarColumn(ColumnName = "FWHENSURESAVE")]
    public bool Fwhensuresave { get; set; }

    /// <summary>
    /// 最大显示
    /// </summary>
    [SugarColumn(ColumnName = "FMAXROW")]
    public int Fmaxrow { get; set; }

    /// <summary>
    /// 每页显示
    /// </summary>
    [SugarColumn(ColumnName = "FPAGESIZE")]
    public int Fpagesize { get; set; }
}
