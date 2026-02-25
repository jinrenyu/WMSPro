using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 巡检方案
/// </summary>
[SugarTable("T_SFC_FCHECKPLAN")]
public class TSfcFcheckplan : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

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
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FEFFECTIVEDATE")]
    public DateTime? Feffectivedate { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE")]
    public DateTime? Fenddate { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FTASKDESCRIBE")]
    public string Ftaskdescribe { get; set; } = string.Empty;

    /// <summary>
    /// 图片
    /// </summary>
    [SugarColumn(ColumnName = "FPICTURE")]
    public byte[]? Fpicture { get; set; }

    /// <summary>
    /// 计数频率
    /// </summary>
    [SugarColumn(ColumnName = "FQTYREQUENCY")]
    public decimal Fqtyrequency { get; set; }

    /// <summary>
    /// 计时频率(小时
    /// </summary>
    [SugarColumn(ColumnName = "FTIMEREQUENCY")]
    public decimal Ftimerequency { get; set; }
}
