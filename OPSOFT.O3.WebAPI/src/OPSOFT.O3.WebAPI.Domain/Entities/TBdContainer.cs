using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 容器
/// </summary>
[SugarTable("T_BD_CONTAINER")]
public class TBdContainer : BaseEntity
{
    /// <summary>
    /// 代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 容器状态
    /// </summary>
    [SugarColumn(ColumnName = "FCTSTATUS")]
    public int FCtStatus { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? FCheckDate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string FDisableId { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? FDisableDate { get; set; }

    /// <summary>
    /// 容器类型
    /// </summary>
    [SugarColumn(ColumnName = "FCTSTYPE")]
    public int FCtSType { get; set; }

    /// <summary>
    /// 容器容积
    /// </summary>
    [SugarColumn(ColumnName = "FCTSVOLUME")]
    public decimal FCtSVolume { get; set; }

    /// <summary>
    /// 容器承重
    /// </summary>
    [SugarColumn(ColumnName = "FCTSWEIGH")]
    public decimal FCtSWeigh { get; set; }

    /// <summary>
    /// 长
    /// </summary>
    [SugarColumn(ColumnName = "FLONG")]
    public decimal FLong { get; set; }

    /// <summary>
    /// 宽
    /// </summary>
    [SugarColumn(ColumnName = "FWIDE")]
    public decimal FWide { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    [SugarColumn(ColumnName = "FHIGH")]
    public decimal FHigh { get; set; }
}
