using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验标准表体-检验项标准范围
/// </summary>
[SugarTable("T_QM_INSPECTSTANDARDENTRY")]
public class TQmInspectstandardentry : BaseEntity
{
    /// <summary>
    /// 检验项
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTITEMID")]
    public string Finspectitemid { get; set; } = string.Empty;

    /// <summary>
    /// 标准值
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTITEMSTD")]
    public string Finspectitemstd { get; set; } = string.Empty;

    /// <summary>
    /// 检验值类型
    /// </summary>
    [SugarColumn(ColumnName = "FVALUETYPE")]
    public int Fvaluetype { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
