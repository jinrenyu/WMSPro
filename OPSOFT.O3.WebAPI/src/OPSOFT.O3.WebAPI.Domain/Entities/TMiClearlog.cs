using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 清场检查记录
/// </summary>
[SugarTable("T_MI_CLEARLOG")]
public class TMiClearlog : BaseEntity
{
    /// <summary>
    /// 任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 清场方案ID
    /// </summary>
    [SugarColumn(ColumnName = "FCLEARSCHEMEID")]
    public string Fclearschemeid { get; set; } = string.Empty;

    /// <summary>
    /// 数据状态
    /// </summary>
    [SugarColumn(ColumnName = "FDATASTATUS")]
    public string Fdatastatus { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡工序ID
    /// </summary>
    [SugarColumn(ColumnName = "FLOWCARDENTRYID")]
    public string Flowcardentryid { get; set; } = string.Empty;

    /// <summary>
    /// 是否全部清场
    /// </summary>
    [SugarColumn(ColumnName = "FOVERED")]
    public bool Fovered { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
