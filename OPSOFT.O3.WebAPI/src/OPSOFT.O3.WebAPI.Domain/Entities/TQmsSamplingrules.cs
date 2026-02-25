using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 抽样方案-抽样规则
/// </summary>
[SugarTable("T_QMS_SAMPLINGRULES")]
public class TQmsSamplingrules : BaseEntity
{
    /// <summary>
    /// 送检数量下限
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBERMIN")]
    public decimal Fnumbermin { get; set; }

    /// <summary>
    /// 送检数量上限
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBERMAX")]
    public decimal Fnumbermax { get; set; }

    /// <summary>
    /// 抽样数量
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEQUANTITY")]
    public decimal Fsamplequantity { get; set; }

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
