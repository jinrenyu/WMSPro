using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// MES检验单结果文件
/// </summary>
[SugarTable("T_SFC_QUALITY_ENTRY2")]
public class TSfcQualityEntry2 : BaseEntity
{
    /// <summary>
    /// 文件大小
    /// </summary>
    [SugarColumn(ColumnName = "FFILESIZE")]
    public string Ffilesize { get; set; } = string.Empty;

    /// <summary>
    /// 存储文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FFILENAME")]
    public string Ffilename { get; set; } = string.Empty;

    /// <summary>
    /// 文件上传路径
    /// </summary>
    [SugarColumn(ColumnName = "FFILEURL")]
    public string Ffileurl { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 日期文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FDATEFILENAME")]
    public string Fdatefilename { get; set; } = string.Empty;

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
