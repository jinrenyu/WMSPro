using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源表体-资源可支配仓库
/// </summary>
[SugarTable("T_ENG_RESOURCEENTRY5")]
public class TEngResourceentry5 : BaseEntity
{
    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULT")]
    public bool Fisdefault { get; set; }

    /// <summary>
    /// 是否虚仓
    /// </summary>
    [SugarColumn(ColumnName = "FISVIRTUAL")]
    public bool Fisvirtual { get; set; }

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
