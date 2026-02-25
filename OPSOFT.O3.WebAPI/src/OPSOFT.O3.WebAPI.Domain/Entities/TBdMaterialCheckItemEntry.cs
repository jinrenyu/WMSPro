using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料检验项目-检验方案
/// </summary>
[SugarTable("T_BD_MATERIALCHECKITEMENTRY")]
public class TBdMaterialcheckitementry : BaseEntity
{
    /// <summary>
    /// 检验类型
    /// </summary>
    [SugarColumn(ColumnName = "FQMTYPE")]
    public string Fqmtype { get; set; } = string.Empty;

    /// <summary>
    /// 加严/放宽
    /// </summary>
    [SugarColumn(ColumnName = "FISTIGHTRELAX")]
    public bool Fistightrelax { get; set; }

    /// <summary>
    /// 加严检验方案ID
    /// </summary>
    [SugarColumn(ColumnName = "FTIGHTID")]
    public string Ftightid { get; set; } = string.Empty;

    /// <summary>
    /// 放宽检验方案ID
    /// </summary>
    [SugarColumn(ColumnName = "FRELAXID")]
    public string Frelaxid { get; set; } = string.Empty;

    /// <summary>
    /// 正常检验方案ID
    /// </summary>
    [SugarColumn(ColumnName = "FNORMALID")]
    public string Fnormalid { get; set; } = string.Empty;

    /// <summary>
    /// 检验规则ID
    /// </summary>
    [SugarColumn(ColumnName = "FQMRULEID")]
    public string Fqmruleid { get; set; } = string.Empty;

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
