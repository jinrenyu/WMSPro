using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓位
/// </summary>
[SugarTable("T_BD_STOCKPLACE")]
public class TBdStockPlace : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; }

    /// <summary>
    /// 仓位代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 仓位名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 仓位描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string FDescription { get; set; } = string.Empty;

    /// <summary>
    /// 所属仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string FStockId { get; set; } = string.Empty;

    /// <summary>
    /// 仓位属性
    /// </summary>
    [SugarColumn(ColumnName = "FSPROPERTY")]
    public string FSpProperty { get; set; } = string.Empty;

    /// <summary>
    /// 允许混放
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWMIX")]
    public bool FAllowMix { get; set; }

    /// <summary>
    /// 最大容量
    /// </summary>
    [SugarColumn(ColumnName = "FMAXCAPACITY")]
    public decimal FMaxCapacity { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; } = DateTime.MinValue;
}
