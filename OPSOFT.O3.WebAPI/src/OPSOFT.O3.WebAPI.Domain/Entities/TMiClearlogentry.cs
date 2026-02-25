using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 清场检查记录-清场项目
/// </summary>
[SugarTable("T_MI_CLEARLOGENTRY")]
public class TMiClearlogentry : BaseEntity
{
    /// <summary>
    /// 清场项目ID
    /// </summary>
    [SugarColumn(ColumnName = "FSITECLEARID")]
    public string Fsiteclearid { get; set; } = string.Empty;

    /// <summary>
    /// 执行要求
    /// </summary>
    [SugarColumn(ColumnName = "FEXEREQ")]
    public string Fexereq { get; set; } = string.Empty;

    /// <summary>
    /// 执行工具
    /// </summary>
    [SugarColumn(ColumnName = "FEXETOOL")]
    public string Fexetool { get; set; } = string.Empty;

    /// <summary>
    /// 是否是重点项目
    /// </summary>
    [SugarColumn(ColumnName = "FISIMP")]
    public bool Fisimp { get; set; }

    /// <summary>
    /// 检查结果
    /// </summary>
    [SugarColumn(ColumnName = "FRESULT")]
    public bool Fresult { get; set; }

    /// <summary>
    /// 清场人
    /// </summary>
    [SugarColumn(ColumnName = "FCLEANTER")]
    public string Fcleanter { get; set; } = string.Empty;

    /// <summary>
    /// 清场时间
    /// </summary>
    [SugarColumn(ColumnName = "FCLEANTIME")]
    public DateTime? Fcleantime { get; set; }

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
