using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验单结果文件
/// </summary>
[SugarTable("T_BD_QM_CL_FILE")]
public class TBdQmClFile : BaseEntity
{
    /// <summary>
    /// 文件大小
    /// </summary>
    [SugarColumn(ColumnName = "FFILESIZE")]
    public string FFilesize { get; set; } = string.Empty;

    /// <summary>
    /// 存储文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FFILENAME")]
    public string FFilename { get; set; } = string.Empty;

    /// <summary>
    /// 文件上传路径
    /// </summary>
    [SugarColumn(ColumnName = "FFILEURL")]
    public string FFileurl { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 日期文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FDATEFILENAME")]
    public string FDateFilename { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int FEntryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string FDetailid { get; set; } = string.Empty;
}
