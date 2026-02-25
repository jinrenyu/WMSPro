using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 组件-以API方式数据源
/// </summary>
[SugarTable("T_AUTOKB_DATASOURCE4")]
public class TAutoKbDatasource4 : BaseEntity
{
    /// <summary>
    /// 数据源唯一内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 与组件内码的关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 配置信息
    /// </summary>
    [SugarColumn(ColumnName = "FCONFIGURATION")]
    public string Fconfiguration { get; set; } = string.Empty;
}
