using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓位动态组合值
/// </summary>
[SugarTable("T_BAS_FLEXVALUESDETAIL")]
public class TBasFlexvaluesdetail : BaseEntity
{
    /// <summary>
    /// 仓位代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 仓位名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;
}
