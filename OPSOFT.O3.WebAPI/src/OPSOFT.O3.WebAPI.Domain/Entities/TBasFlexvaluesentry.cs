using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓位集值明细
/// </summary>
[SugarTable("T_BAS_FLEXVALUESENTRY")]
public class TBasFlexvaluesentry : BaseEntity
{
    /// <summary>
    /// 仓位集值代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集值名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集值描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集值是否禁用
    /// </summary>
    [SugarColumn(ColumnName = "FFORBID")]
    public bool Fforbid { get; set; }

    /// <summary>
    /// 行状态
    /// </summary>
    [SugarColumn(ColumnName = "FROWLSTATUS")]
    public string Frowlstatus { get; set; } = string.Empty;

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
