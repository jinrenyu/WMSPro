using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料类型检验配置-检验方案配置
/// </summary>
[SugarTable("T_QMS_INSPECTIONPLAN2")]
public class TQmsInspectionplan2 : BaseEntity
{
    /// <summary>
    /// 检验类型
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONTYPE")]
    public string Finspectiontype { get; set; } = string.Empty;

    /// <summary>
    /// 正常检验方案代码
    /// </summary>
    [SugarColumn(ColumnName = "FNORMALINSPECTIONNUMBER")]
    public string Fnormalinspectionnumber { get; set; } = string.Empty;

    /// <summary>
    /// 正常检验方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FNORMALINSPECTIONNAME")]
    public string Fnormalinspectionname { get; set; } = string.Empty;

    /// <summary>
    /// 启用加严放宽
    /// </summary>
    [SugarColumn(ColumnName = "FISSTRICTRELAX")]
    public bool Fisstrictrelax { get; set; }

    /// <summary>
    /// 检验规则代码
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONRULESNUMBER")]
    public string Finspectionrulesnumber { get; set; } = string.Empty;

    /// <summary>
    /// 检验规则名称
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONRULESNAME")]
    public string Finspectionrulesname { get; set; } = string.Empty;

    /// <summary>
    /// 加严检验方案代码
    /// </summary>
    [SugarColumn(ColumnName = "FTIGHTINSPECTIONNUMBER")]
    public string Ftightinspectionnumber { get; set; } = string.Empty;

    /// <summary>
    /// 加严检验方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FTIGHTINSPECTIONNAME")]
    public string Ftightinspectionname { get; set; } = string.Empty;

    /// <summary>
    /// 放宽检验方案代码
    /// </summary>
    [SugarColumn(ColumnName = "FRELAXINSPECTIONNUMBER")]
    public string Frelaxinspectionnumber { get; set; } = string.Empty;

    /// <summary>
    /// 放宽检验方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FRELAXINSPECTIONNAME")]
    public string Frelaxinspectionname { get; set; } = string.Empty;

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
