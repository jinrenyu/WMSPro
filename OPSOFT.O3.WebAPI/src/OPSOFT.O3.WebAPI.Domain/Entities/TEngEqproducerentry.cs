using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备生产厂商表体-供货信息
/// </summary>
[SugarTable("T_ENG_EQPRODUCERENTRY")]
public class TEngEqproducerentry : BaseEntity
{
    /// <summary>
    /// 设备规格
    /// </summary>
    [SugarColumn(ColumnName = "FSIZE")]
    public string Fsize { get; set; } = string.Empty;

    /// <summary>
    /// 设备型号
    /// </summary>
    [SugarColumn(ColumnName = "FMODEL")]
    public string Fmodel { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPID")]
    public string Fequipid { get; set; } = string.Empty;

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
