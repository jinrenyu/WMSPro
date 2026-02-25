using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验方案表体-附件
/// </summary>
[SugarTable("T_QM_INSPECTSOLUTIONENTRY1")]
public class TQmInspectsolutionentry1 : BaseEntity
{
    /// <summary>
    /// 路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

    /// <summary>
    /// 文件名
    /// </summary>
    [SugarColumn(ColumnName = "FFILENAME")]
    public string Ffilename { get; set; } = string.Empty;

    /// <summary>
    /// 文件全名
    /// </summary>
    [SugarColumn(ColumnName = "FFULLFILENAME")]
    public string Ffullfilename { get; set; } = string.Empty;

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
