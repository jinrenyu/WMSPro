using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 全国地址
/// </summary>
[SugarTable("MAP_NATIONAL")]
public class MapNational : BaseEntity
{
    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "CODE")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID
    /// </summary>
    [SugarColumn(ColumnName = "PARENTID")]
    public int Parentid { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    [SugarColumn(ColumnName = "LEVEL")]
    public int Level { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
