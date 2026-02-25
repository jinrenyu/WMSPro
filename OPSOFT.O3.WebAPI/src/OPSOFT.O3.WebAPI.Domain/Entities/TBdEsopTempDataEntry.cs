using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP临时数据表表体-文件
/// </summary>
[SugarTable("T_BD_ESOPTEMPDATA_ENTRY")]
public class TBdEsoptempdataEntry : BaseEntity
{
    /// <summary>
    /// 文件内码
    /// </summary>
    [SugarColumn(ColumnName = "FFILEID")]
    public string Ffileid { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 下发记录内码
    /// </summary>
    [SugarColumn(ColumnName = "ISSUEDNO")]
    public string Issuedno { get; set; } = string.Empty;

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
