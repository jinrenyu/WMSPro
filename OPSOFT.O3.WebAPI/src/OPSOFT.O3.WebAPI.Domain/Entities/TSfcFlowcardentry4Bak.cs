using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡表体的表体-人员-备份
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY4_BAK")]
public class TSfcFlowcardentry4Bak : BaseEntity
{
    /// <summary>
    /// 变更单单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHGINTERID")]
    public string Fchginterid { get; set; } = string.Empty;

    /// <summary>
    /// 变更单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHGDETAILID")]
    public string Fchgdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 职员/班组
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

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
