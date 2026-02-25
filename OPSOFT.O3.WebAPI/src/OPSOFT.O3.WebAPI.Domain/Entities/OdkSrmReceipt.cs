using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 收货单表头
/// </summary>
[SugarTable("ODK_SRM_Receipt")]
public class OdkSrmReceipt : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 收退货标识(0:收货 1:退货
    /// </summary>
    [SugarColumn(ColumnName = "FROB")]
    public bool Frob { get; set; }
}
