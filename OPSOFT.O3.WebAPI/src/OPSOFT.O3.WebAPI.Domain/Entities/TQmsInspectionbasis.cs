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
}
