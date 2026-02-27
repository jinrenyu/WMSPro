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

    /// <summary>
    /// 附件路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH", IsNullable = true)]
    public string FPATH { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 功率(KW)
    /// </summary>
    [SugarColumn(ColumnName = "FPOWER", IsNullable = true)]
    public decimal? FPOWER { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITNAME", IsNullable = true)]
    public string FUNITNAME { get; set; } = string.Empty;
}
