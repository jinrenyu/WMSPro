using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 治具变更单表体
/// </summary>
[SugarTable("T_ENG_JIGCHANGEENTRY")]
public class TEngJigchangeentry : BaseEntity
{
    /// <summary>
    /// 0=入库 1=出库
    /// </summary>
    [SugarColumn(ColumnName = "FISOUT")]
    public int Fisout { get; set; }

    /// <summary>
    /// 治具内码
    /// </summary>
    [SugarColumn(ColumnName = "FJIGID")]
    public string Fjigid { get; set; } = string.Empty;

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 员工内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public int Fqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
