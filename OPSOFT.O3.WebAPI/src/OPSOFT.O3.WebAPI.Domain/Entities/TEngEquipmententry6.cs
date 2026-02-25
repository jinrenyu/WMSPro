using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备表体-备件
/// </summary>
[SugarTable("T_ENG_EQUIPMENTENTRY6")]
public class TEngEquipmententry6 : BaseEntity
{
    /// <summary>
    /// 备件清单ID
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTSID")]
    public string Fsparepartsid { get; set; } = string.Empty;

    /// <summary>
    /// 是否专用件
    /// </summary>
    [SugarColumn(ColumnName = "FISSPECIAL")]
    public bool Fisspecial { get; set; }

    /// <summary>
    /// 储备定额
    /// </summary>
    [SugarColumn(ColumnName = "FSTORAGEQUANTITY")]
    public decimal Fstoragequantity { get; set; }

    /// <summary>
    /// 再用量
    /// </summary>
    [SugarColumn(ColumnName = "FINUSEAMOUNT")]
    public decimal Finuseamount { get; set; }

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
