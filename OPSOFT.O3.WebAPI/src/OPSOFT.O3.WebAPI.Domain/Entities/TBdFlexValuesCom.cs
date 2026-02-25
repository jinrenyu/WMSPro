using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓位值组合
/// </summary>
[SugarTable("T_BD_FLEXVALUESCOM")]
public class TBdFlexvaluescom : BaseEntity
{
    /// <summary>
    /// 仓库内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 是否禁用
    /// </summary>
    [SugarColumn(ColumnName = "FISDISABLE")]
    public bool Fisdisable { get; set; }
}
