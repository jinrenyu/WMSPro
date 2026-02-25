using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 表索引信息
/// </summary>
[SugarTable("SYS_INDEXDESCRIPTION")]
public class SysIndexDescription : BaseEntity
{
    /// <summary>
    /// 索引代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 索引名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 索引类型(1=唯一簇索引;2=唯一索引;3=普通索引
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;
}
