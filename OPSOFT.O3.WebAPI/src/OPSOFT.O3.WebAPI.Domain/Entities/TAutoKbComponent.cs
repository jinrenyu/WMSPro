using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 自定义看板组件
/// </summary>
[SugarTable("T_AUTOKB_COMPONENT")]
public class TAutoKbComponent : BaseEntity
{
    /// <summary>
    /// 组件名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 组件标题
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 组件样式
    /// </summary>
    [SugarColumn(ColumnName = "FSTYLE")]
    public string Fstyle { get; set; } = string.Empty;

    /// <summary>
    /// 数据集刷新频率
    /// </summary>
    [SugarColumn(ColumnName = "FPERIOD")]
    public decimal Fperiod { get; set; }

    /// <summary>
    /// 数据源1内码
    /// </summary>
    [SugarColumn(ColumnName = "FDATASOURCE1ID")]
    public string Fdatasource1id { get; set; } = string.Empty;

    /// <summary>
    /// 数据源2内码
    /// </summary>
    [SugarColumn(ColumnName = "FDATASOURCE2ID")]
    public string Fdatasource2id { get; set; } = string.Empty;

    /// <summary>
    /// 数据源3内码
    /// </summary>
    [SugarColumn(ColumnName = "FDATASOURCE3ID")]
    public string Fdatasource3id { get; set; } = string.Empty;

    /// <summary>
    /// 数据源4内码
    /// </summary>
    [SugarColumn(ColumnName = "FDATASOURCE4ID")]
    public string Fdatasource4id { get; set; } = string.Empty;

    /// <summary>
    /// 启用哪一个数据源内码
    /// </summary>
    [SugarColumn(ColumnName = "FISUSEDTYPE")]
    public string Fisusedtype { get; set; } = string.Empty;

    /// <summary>
    /// 看板组件唯一内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
