using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 标准维护方案_预防性保养
/// </summary>
[SugarTable("T_ENG_STANDMTTASKPROJECTENTRY1")]
public class TEngStandmttaskprojectentry1 : BaseEntity
{
    /// <summary>
    /// 内容及要求
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENTSANDREQUIREMENTS")]
    public string Fcontentsandrequirements { get; set; } = string.Empty;

    /// <summary>
    /// 部位
    /// </summary>
    [SugarColumn(ColumnName = "FPART")]
    public string Fpart { get; set; } = string.Empty;

    /// <summary>
    /// 维护周期
    /// </summary>
    [SugarColumn(ColumnName = "FCYCLE")]
    public int Fcycle { get; set; }

    /// <summary>
    /// 工种内码
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTID")]
    public string Fcraftid { get; set; } = string.Empty;

    /// <summary>
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTTIME")]
    public decimal Fcrafttime { get; set; }

    /// <summary>
    /// 是否停机
    /// </summary>
    [SugarColumn(ColumnName = "FISSTOPMACHINE")]
    public bool Fisstopmachine { get; set; }

    /// <summary>
    /// 维护标准
    /// </summary>
    [SugarColumn(ColumnName = "FSTANDARD")]
    public string Fstandard { get; set; } = string.Empty;

    /// <summary>
    /// 周期单位  1=天，2=周，3=月
    /// </summary>
    [SugarColumn(ColumnName = "FUNIT")]
    public int Funit { get; set; }

    /// <summary>
    /// 维护方法
    /// </summary>
    [SugarColumn(ColumnName = "FMETHOD")]
    public string Fmethod { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
