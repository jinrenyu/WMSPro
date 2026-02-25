using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 齐套分析-表头
/// </summary>
[SugarTable("T_BD_WHOLEANALYSIS")]
public class TBdWholeanalysis : BaseEntity
{
    /// <summary>
    /// 分析开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 分析结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 仓库范围
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKS")]
    public string Fstocks { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否计算库存合格物料
    /// </summary>
    [SugarColumn(ColumnName = "FISINSTOCK")]
    public bool Fisinstock { get; set; }

    /// <summary>
    /// 是否计算在线物料
    /// </summary>
    [SugarColumn(ColumnName = "FISONLINE")]
    public bool Fisonline { get; set; }

    /// <summary>
    /// 是否计算待入库物料
    /// </summary>
    [SugarColumn(ColumnName = "FISWILLINSTOCK")]
    public bool Fiswillinstock { get; set; }

    /// <summary>
    /// 是否计算在途物料
    /// </summary>
    [SugarColumn(ColumnName = "FISINTRANSIT")]
    public bool Fisintransit { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
