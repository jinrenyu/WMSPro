using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单据删除约束
/// </summary>
[SugarTable("SYS_BILLTEMPLATEDEL")]
public class SysBillTemplateDel : BaseEntity
{
    /// <summary>
    /// 引用表名
    /// </summary>
    [SugarColumn(ColumnName = "FTABLENAME")]
    public string Ftablename { get; set; } = string.Empty;

    /// <summary>
    /// 引用表字段
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDNAME")]
    public string Ffieldname { get; set; } = string.Empty;

    /// <summary>
    /// 单据字段
    /// </summary>
    [SugarColumn(ColumnName = "FBILLFIELDNAME")]
    public string Fbillfieldname { get; set; } = string.Empty;

    /// <summary>
    /// 其他Where
    /// </summary>
    [SugarColumn(ColumnName = "FWHERE")]
    public string Fwhere { get; set; } = string.Empty;

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
