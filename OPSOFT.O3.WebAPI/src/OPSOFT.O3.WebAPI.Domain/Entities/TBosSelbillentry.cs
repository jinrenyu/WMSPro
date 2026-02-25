using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 出入库流程配置-单据类型映射
/// </summary>
[SugarTable("T_BOS_SELBILLENTRY")]
public class TBosSelbillentry : BaseEntity
{
    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEID")]
    public string Fsourceid { get; set; } = string.Empty;

    /// <summary>
    /// 目标单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FDESTID")]
    public string Fdestid { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认值
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULT")]
    public bool Fdefault { get; set; }

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
