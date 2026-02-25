using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外用料清单明细
/// </summary>
[SugarTable("T_SUB_PPBOMENTRY")]
public class TSubPpbomentry : BaseEntity
{
    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEGROUP")]
    public string Freplacegroup { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 子项类型
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALTYPE")]
    public string Fmaterialtype { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 用量类型
    /// </summary>
    [SugarColumn(ColumnName = "FDOSAGETYPE")]
    public string Fdosagetype { get; set; } = string.Empty;

    /// <summary>
    /// 使用比例(%
    /// </summary>
    [SugarColumn(ColumnName = "FUSERATE")]
    public decimal Fuserate { get; set; }
}
