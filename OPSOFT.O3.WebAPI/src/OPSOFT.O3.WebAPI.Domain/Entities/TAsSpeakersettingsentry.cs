using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯喇叭配置表体_字段配置
/// </summary>
[SugarTable("T_AS_SPEAKERSETTINGSENTRY")]
public class TAsSpeakersettingsentry : BaseEntity
{
    /// <summary>
    /// 字段名
    /// </summary>
    [SugarColumn(ColumnName = "FFIELD")]
    public string Ffield { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
