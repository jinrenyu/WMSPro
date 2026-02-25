using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序首检表体-检验项目
/// </summary>
[SugarTable("T_SFC_FIRSTINPECTIONENTRY")]
public class TSfcFirstinpectionentry : BaseEntity
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
    public int Fanalysisway { get; set; }

    /// <summary>
    /// 检验值(定性
    /// </summary>
    [SugarColumn(ColumnName = "FQUALTESTVALUE")]
    public int Fqualtestvalue { get; set; }
}
