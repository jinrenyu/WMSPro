using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备表体-特性分类
/// </summary>
[SugarTable("T_ENG_EQUIPMENTENTRY5")]
public class TEngEquipmententry5 : BaseEntity
{
    /// <summary>
    /// 特性分类内码
    /// </summary>
    [SugarColumn(ColumnName = "FSEQSPID")]
    public string Fseqspid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
