using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 代码对照表体
/// </summary>
[SugarTable("SYS_CODECONTRASTENTRY")]
public class SysCodeContrastEntry : BaseEntity
{
    /// <summary>
    /// 编码值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE")]
    public string Fvalue { get; set; } = string.Empty;

    /// <summary>
    /// 对应编码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

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
