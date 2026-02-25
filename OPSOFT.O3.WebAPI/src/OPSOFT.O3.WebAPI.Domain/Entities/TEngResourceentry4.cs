using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源表体-汇报显示配置
/// </summary>
[SugarTable("T_ENG_RESOURCEENTRY4")]
public class TEngResourceentry4 : BaseEntity
{
    /// <summary>
    /// 汇报页签显示内码
    /// </summary>
    [SugarColumn(ColumnName = "FPAGESETID")]
    public string Fpagesetid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报页签显示编码
    /// </summary>
    [SugarColumn(ColumnName = "FPAGESETNUMBER")]
    public string Fpagesetnumber { get; set; } = string.Empty;

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
