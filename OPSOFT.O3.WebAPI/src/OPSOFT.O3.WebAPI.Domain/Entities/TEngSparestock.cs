using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 备件盘点
/// </summary>
[SugarTable("T_ENG_SPARESTOCK")]
public class TEngSparestock : BaseEntity
{
    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 盘点人
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKPEOID")]
    public string Fstockpeoid { get; set; } = string.Empty;

    /// <summary>
    /// 盘点日期
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREDATE")]
    public DateTime? Fsparedate { get; set; }

    /// <summary>
    /// 备件盘点单名称
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKNAME")]
    public string Fstockname { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人内码
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 出入库类型
    /// </summary>
    [SugarColumn(ColumnName = "FADDSTORAGETYPEID")]
    public string Faddstoragetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
