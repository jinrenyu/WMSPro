using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备表体-附件信息
/// </summary>
[SugarTable("T_ENG_EQUIPMENTENTRY1")]
public class TEngEquipmententry1 : BaseEntity
{
    /// <summary>
    /// 附件编号
    /// </summary>
    [SugarColumn(ColumnName = "FATNUMBER")]
    public string Fatnumber { get; set; } = string.Empty;

    /// <summary>
    /// 附件名称
    /// </summary>
    [SugarColumn(ColumnName = "FATNAME")]
    public string Fatname { get; set; } = string.Empty;

    /// <summary>
    /// 出厂日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODATE")]
    public DateTime? Fprodate { get; set; }

    /// <summary>
    /// 设备原值
    /// </summary>
    [SugarColumn(ColumnName = "FORIGINALVALUE")]
    public decimal Foriginalvalue { get; set; }

    /// <summary>
    /// 重量(T
    /// </summary>
    [SugarColumn(ColumnName = "FWEIGHT")]
    public decimal Fweight { get; set; }
}
