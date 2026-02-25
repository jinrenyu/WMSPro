using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备表体-部位信息
/// </summary>
[SugarTable("T_ENG_EQUIPMENTENTRY3")]
public class TEngEquipmententry3 : BaseEntity
{
    /// <summary>
    /// 部位号
    /// </summary>
    [SugarColumn(ColumnName = "FPARTNO")]
    public string Fpartno { get; set; } = string.Empty;

    /// <summary>
    /// 部位名称
    /// </summary>
    [SugarColumn(ColumnName = "FPARTNAME")]
    public string Fpartname { get; set; } = string.Empty;

    /// <summary>
    /// 是否日点检
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECKEVERYDAY")]
    public bool Fischeckeveryday { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否派工
    /// </summary>
    [SugarColumn(ColumnName = "FISASSIGN")]
    public bool Fisassign { get; set; }

    /// <summary>
    /// 点检负责人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEADERID")]
    public string Fcheckleaderid { get; set; } = string.Empty;

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
