using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 清场项目方案表体-清场项目
/// </summary>
[SugarTable("T_MI_SITECLEARSCHEMEENTRY")]
public class TMiSiteclearschemeentry : BaseEntity
{
    /// <summary>
    /// 清场项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FSITECLRID")]
    public string Fsiteclrid { get; set; } = string.Empty;

    /// <summary>
    /// 点检类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 是否必录
    /// </summary>
    [SugarColumn(ColumnName = "FINPUT")]
    public bool Finput { get; set; }

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
