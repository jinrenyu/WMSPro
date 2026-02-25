using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料类型检验配置
/// </summary>
[SugarTable("T_QMS_MATERIALTYPEINSPECTION")]
public class TQmsMaterialtypeinspection : BaseEntity
{
    /// <summary>
    /// 检验类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 筛选字段
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDS")]
    public string Ffields { get; set; } = string.Empty;

    /// <summary>
    /// 字段内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 物料id
    /// </summary>
    [SugarColumn(ColumnName = "MATERIALID")]
    public string Materialid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
