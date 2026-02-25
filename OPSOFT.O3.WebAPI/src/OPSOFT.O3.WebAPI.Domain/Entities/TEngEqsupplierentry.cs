using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备供应商表体
/// </summary>
[SugarTable("T_ENG_EQSUPPLIERENTRY")]
public class TEngEqsupplierentry : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
    /// 代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

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
