using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 装箱单
/// </summary>
[SugarTable("T_BD_BOX")]
public class TBdBox : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string FBarcode { get; set; } = string.Empty;

    /// <summary>
    /// 装箱底阶明细
    /// </summary>
    [SugarColumn(ColumnName = "FBOXINFO")]
    public string FBoxinfo { get; set; } = string.Empty;

    /// <summary>
    /// 送货地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string FAddress { get; set; } = string.Empty;

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal FAmount { get; set; }

    /// <summary>
    /// 金额(本位币
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT_LC")]
    public decimal FamountLc { get; set; }
}
