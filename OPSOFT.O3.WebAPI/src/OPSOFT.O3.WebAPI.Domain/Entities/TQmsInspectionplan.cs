using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料检验配置-检验方案配置
/// </summary>
[SugarTable("T_QMS_INSPECTIONPLAN")]
public class TQmsInspectionplan : BaseEntity
{
    /// <summary>
    /// 检验类型
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONTYPE")]
    public string Finspectiontype { get; set; } = string.Empty;

    /// <summary>
    /// 启用加严放宽
    /// </summary>
    [SugarColumn(ColumnName = "FISSTRICTRELAX")]
    public bool Fisstrictrelax { get; set; }

    /// <summary>
    /// 正常检验方案ID
    /// </summary>
    [SugarColumn(ColumnName = "FNORMALINSPECTIONID")]
    public string Fnormalinspectionid { get; set; } = string.Empty;

    /// <summary>
    /// 检验规则ID
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONRULESID")]
    public string Finspectionrulesid { get; set; } = string.Empty;

    /// <summary>
    /// 加严检验方案ID
    /// </summary>
    [SugarColumn(ColumnName = "FTIGHTINSPECTIONID")]
    public string Ftightinspectionid { get; set; } = string.Empty;

    /// <summary>
    /// 放宽检验方案ID
    /// </summary>
    [SugarColumn(ColumnName = "FRELAXINSPECTION")]
    public string Frelaxinspection { get; set; } = string.Empty;

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
