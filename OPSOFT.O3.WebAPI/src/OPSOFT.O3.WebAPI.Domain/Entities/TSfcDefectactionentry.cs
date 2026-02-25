using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良维修工序
/// </summary>
[SugarTable("T_SFC_DEFECTACTIONENTRY")]
public class TSfcDefectactionentry : BaseEntity
{
    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序排序码
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public string Fseq { get; set; } = string.Empty;

    /// <summary>
    /// 是否已返工
    /// </summary>
    [SugarColumn(ColumnName = "FREWORKED")]
    public bool Freworked { get; set; }

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
