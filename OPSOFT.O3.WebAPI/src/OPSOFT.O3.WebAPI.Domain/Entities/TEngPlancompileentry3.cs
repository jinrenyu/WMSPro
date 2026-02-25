using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维护计划表_日常性保养
/// </summary>
[SugarTable("T_ENG_PLANCOMPILEENTRY3")]
public class TEngPlancompileentry3 : BaseEntity
{
    /// <summary>
    /// 内容及要求
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENTSANDREQUIREMENTS")]
    public string Fcontentsandrequirements { get; set; } = string.Empty;

    /// <summary>
    /// 工种内码
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTID")]
    public string Fcraftid { get; set; } = string.Empty;

    /// <summary>
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTTIME")]
    public decimal Fcrafttime { get; set; }

    /// <summary>
    /// 是否停机
    /// </summary>
    [SugarColumn(ColumnName = "FISSTOPMACHINE")]
    public bool Fisstopmachine { get; set; }

    /// <summary>
    /// 维护标准
    /// </summary>
    [SugarColumn(ColumnName = "FSTANDARD")]
    public string Fstandard { get; set; } = string.Empty;

    /// <summary>
    /// 维护方法
    /// </summary>
    [SugarColumn(ColumnName = "FMETHOD")]
    public string Fmethod { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 计划维护时间
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTMAINTDATE")]
    public DateTime? Fnextmaintdate { get; set; }

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEID")]
    public string Fmachineid { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 工单编号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERBILLNO")]
    public string Forderbillno { get; set; } = string.Empty;

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
