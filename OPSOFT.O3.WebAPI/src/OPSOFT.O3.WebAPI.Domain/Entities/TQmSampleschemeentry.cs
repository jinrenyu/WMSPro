using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 抽样方案表体
/// </summary>
[SugarTable("T_QM_SAMPLESCHEMEENTRY")]
public class TQmSampleschemeentry : BaseEntity
{
    /// <summary>
    /// 批量
    /// </summary>
    [SugarColumn(ColumnName = "FBATCH")]
    public int Fbatch { get; set; }

    /// <summary>
    /// 样本量字码
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLECODE")]
    public string Fsamplecode { get; set; } = string.Empty;

    /// <summary>
    /// 样本量
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLESIZE")]
    public int Fsamplesize { get; set; }

    /// <summary>
    /// 允收数
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWEDNUMBER")]
    public int Fallowednumber { get; set; }

    /// <summary>
    /// 拒收数
    /// </summary>
    [SugarColumn(ColumnName = "FREJECTIONNUMBER")]
    public int Frejectionnumber { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
