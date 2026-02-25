using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备表体-技术参数
/// </summary>
[SugarTable("T_ENG_EQUIPMENTENTRY2")]
public class TEngEquipmententry2 : BaseEntity
{
    /// <summary>
    /// 技术参数内码
    /// </summary>
    [SugarColumn(ColumnName = "FPARAID")]
    public string Fparaid { get; set; } = string.Empty;

    /// <summary>
    /// 标准值
    /// </summary>
    [SugarColumn(ColumnName = "FSTDVALUE")]
    public decimal Fstdvalue { get; set; }

    /// <summary>
    /// 实测值
    /// </summary>
    [SugarColumn(ColumnName = "FACTVALUE")]
    public decimal Factvalue { get; set; }

    /// <summary>
    /// 测量日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

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
