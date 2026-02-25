using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 表体实体基类 - 包含所有表体的公共字段
/// </summary>
public abstract class BaseEntryEntity : BaseEntity
{
    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int FEntryId { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string FDetailId { get; set; } = string.Empty;
}
