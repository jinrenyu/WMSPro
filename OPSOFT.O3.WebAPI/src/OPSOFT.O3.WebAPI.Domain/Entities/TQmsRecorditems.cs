using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验方案-记录项目
/// </summary>
[SugarTable("T_QMS_RECORDITEMS")]
public class TQmsRecorditems : BaseEntity
{
    /// <summary>
    /// 记录项目id
    /// </summary>
    [SugarColumn(ColumnName = "RECORDITEMSID")]
    public string Recorditemsid { get; set; } = string.Empty;

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
