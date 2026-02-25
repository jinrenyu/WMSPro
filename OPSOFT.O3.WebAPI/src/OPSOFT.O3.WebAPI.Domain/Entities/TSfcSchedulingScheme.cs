using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 排程方案
/// </summary>
[SugarTable("T_SFC_SchedulingScheme")]
public class TSfcSchedulingScheme : BaseEntity
{
    /// <summary>
    /// 排程方案编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 排程方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 第一优先级
    /// </summary>
    [SugarColumn(ColumnName = "FFIRSTLEVEL")]
    public int Ffirstlevel { get; set; }

    /// <summary>
    /// 第二优先级
    /// </summary>
    [SugarColumn(ColumnName = "FSECONDLEVEL")]
    public int Fsecondlevel { get; set; }

    /// <summary>
    /// 第三优先级
    /// </summary>
    [SugarColumn(ColumnName = "FTHIRDLEVEL")]
    public int Fthirdlevel { get; set; }

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
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;
}
