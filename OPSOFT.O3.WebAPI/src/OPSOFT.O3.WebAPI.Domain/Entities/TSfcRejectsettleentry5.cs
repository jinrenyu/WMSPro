using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良品处理表体的表体-明细对应清单
/// </summary>
[SugarTable("T_SFC_REJECTSETTLEENTRY5")]
public class TSfcRejectsettleentry5 : BaseEntity
{
    /// <summary>
    /// 表体1内码
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYDETAILID")]
    public string Fentrydetailid { get; set; } = string.Empty;

    /// <summary>
    /// 表体1明细汇报内码
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYREPORTID")]
    public string Fentryreportid { get; set; } = string.Empty;

    /// <summary>
    /// 关联量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
