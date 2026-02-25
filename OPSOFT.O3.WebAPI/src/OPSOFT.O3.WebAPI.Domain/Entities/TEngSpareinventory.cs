using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 备件及时库存
/// </summary>
[SugarTable("T_ENG_SPAREINVENTORY")]
public class TEngSpareinventory : BaseEntity
{
    /// <summary>
    /// 备件数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public int Fcount { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
