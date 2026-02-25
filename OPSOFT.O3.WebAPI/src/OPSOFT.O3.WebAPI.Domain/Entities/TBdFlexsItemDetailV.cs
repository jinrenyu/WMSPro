using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 辅助属性动态组合值
/// </summary>
[SugarTable("T_BD_FLEXSITEMDETAILV")]
public class TBdFlexsitemdetailv : BaseEntity
{
    /// <summary>
    /// 操作代码
    /// </summary>
    [SugarColumn(ColumnName = "FOPCODE")]
    public string Fopcode { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;
}
