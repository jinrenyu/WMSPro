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

    /// <summary>
    /// 出入库价格
    /// </summary>
    [SugarColumn(ColumnName = "FADDSTORAGEPRICE", IsNullable = true)]
    public decimal? FADDSTORAGEPRICE { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO", IsNullable = true)]
    public string FBATCHNO { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT", IsNullable = true)]
    public decimal? FAMOUNT { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FMONEY", IsNullable = true)]
    public decimal? FMONEY { get; set; }
}
