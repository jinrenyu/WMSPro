using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档-装箱底阶明细
/// </summary>
[SugarTable("T_BD_BARCODERS4")]
public class TBdBarcoders4 : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 装箱底阶明细
    /// </summary>
    [SugarColumn(ColumnName = "FBOXINFO")]
    public string Fboxinfo { get; set; } = string.Empty;
}
