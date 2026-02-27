using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验水平-检验依据
/// </summary>
[SugarTable("T_QMS_INSPECTIONBASIS")]
public class TQmsInspectionbasis : BaseEntity
{
    /// <summary>
    /// 抽样数量下限
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBERMIN")]
    public decimal Fnumbermin { get; set; }

    /// <summary>
    /// 抽样数量上限
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBERMAX")]
    public decimal Fnumbermax { get; set; }

    /// <summary>
    /// AC(收
    /// </summary>
    [SugarColumn(ColumnName = "FAC")]
    public decimal Fac { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// AC(退)
    /// </summary>
    [SugarColumn(ColumnName = "FRE", IsNullable = true)]
    public decimal? FRE { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }
}
