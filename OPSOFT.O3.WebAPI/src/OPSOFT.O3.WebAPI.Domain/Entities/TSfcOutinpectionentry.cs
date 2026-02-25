using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外检验表体-检验项目
/// </summary>
[SugarTable("T_SFC_OUTINPECTIONENTRY")]
public class TSfcOutinpectionentry : BaseEntity
{
    /// <summary>
    /// 检验项目
    /// </summary>
    [SugarColumn(ColumnName = "FTESTPROJECTID")]
    public string Ftestprojectid { get; set; } = string.Empty;

    /// <summary>
    /// 分析方法
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISWAY")]
    public string Fanalysisway { get; set; } = string.Empty;

    /// <summary>
    /// 检验值(定性
    /// </summary>
    [SugarColumn(ColumnName = "FQUALTESTVALUE")]
    public string Fqualtestvalue { get; set; } = string.Empty;
}
