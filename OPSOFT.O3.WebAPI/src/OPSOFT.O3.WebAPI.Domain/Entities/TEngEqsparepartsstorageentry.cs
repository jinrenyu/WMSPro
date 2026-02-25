using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 备件调整之备件信息
/// </summary>
[SugarTable("T_ENG_EQSPAREPARTSSTORAGEENTRY")]
public class TEngEqsparepartsstorageentry : BaseEntity
{
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 备件编号
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTSID")]
    public string Fsparepartsid { get; set; } = string.Empty;

    /// <summary>
    /// 价格
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 税率(%
    /// </summary>
    [SugarColumn(ColumnName = "FRATE")]
    public decimal Frate { get; set; }
}
