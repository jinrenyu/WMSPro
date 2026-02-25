using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 人岗匹配表体
/// </summary>
[SugarTable("T_WM_EMPMATCHENTRY")]
public class TWmEmpmatchentry : BaseEntity
{
    /// <summary>
    /// 岗位内码
    /// </summary>
    [SugarColumn(ColumnName = "FPOSITIONID")]
    public string Fpositionid { get; set; } = string.Empty;

    /// <summary>
    /// 默认岗位
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULT")]
    public bool Fdefault { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
