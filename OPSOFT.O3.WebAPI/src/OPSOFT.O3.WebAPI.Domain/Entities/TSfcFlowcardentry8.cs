using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡的表体-工序汇报控制
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY8")]
public class TSfcFlowcardentry8 : BaseEntity
{
    /// <summary>
    /// 前置工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPREPROCESSID")]
    public string Fpreprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 前置工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPREDETAILID")]
    public string Fpredetailid { get; set; } = string.Empty;

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
