using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 表单插件
/// </summary>
[SugarTable("SYS_BILLTEMPLATEENTRY6")]
public class SysBilltemplateentry6 : BaseEntity
{
    /// <summary>
    /// 插件优先顺序
    /// </summary>
    [SugarColumn(ColumnName = "FINDEX")]
    public int Findex { get; set; }

    /// <summary>
    /// 插件类别
    /// </summary>
    [SugarColumn(ColumnName = "FPLUGTYPE")]
    public string Fplugtype { get; set; } = string.Empty;

    /// <summary>
    /// 插件程序集
    /// </summary>
    [SugarColumn(ColumnName = "FASSEMBLY")]
    public string Fassembly { get; set; } = string.Empty;

    /// <summary>
    /// 插件描述
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
