using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用户过滤方案信息表体
/// </summary>
[SugarTable("SYS_USERFILTERINFOENTRY")]
public class SysUserFilterInfoEntry : BaseEntity
{
    /// <summary>
    /// 功能代码
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSTYPEID")]
    public string Fclasstypeid { get; set; } = string.Empty;

    /// <summary>
    /// 用户帐号
    /// </summary>
    [SugarColumn(ColumnName = "FUSER_ID")]
    public string FuserId { get; set; } = string.Empty;

    /// <summary>
    /// 左边括号
    /// </summary>
    [SugarColumn(ColumnName = "FLEFT")]
    public string Fleft { get; set; } = string.Empty;

    /// <summary>
    /// 字段
    /// </summary>
    [SugarColumn(ColumnName = "FDATAMEMBER")]
    public string Fdatamember { get; set; } = string.Empty;

    /// <summary>
    /// 过滤条件
    /// </summary>
    [SugarColumn(ColumnName = "FFILTEROPERATOR")]
    public string Ffilteroperator { get; set; } = string.Empty;

    /// <summary>
    /// 值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE")]
    public string Fvalue { get; set; } = string.Empty;

    /// <summary>
    /// 右边括号
    /// </summary>
    [SugarColumn(ColumnName = "FRIGHT")]
    public string Fright { get; set; } = string.Empty;

    /// <summary>
    /// 逻辑
    /// </summary>
    [SugarColumn(ColumnName = "FLOGICALOPERATORS")]
    public string Flogicaloperators { get; set; } = string.Empty;

    /// <summary>
    /// 是否区分大小写
    /// </summary>
    [SugarColumn(ColumnName = "FISCASESENSITIVE")]
    public bool Fiscasesensitive { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
