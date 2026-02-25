using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良品处理表体的表体-维修（维修清单）
/// </summary>
[SugarTable("T_SFC_REJECTSETTLEENTRY7")]
public class TSfcRejectsettleentry7 : BaseEntity
{
    /// <summary>
    /// 维修站内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRID")]
    public string Frepairid { get; set; } = string.Empty;

    /// <summary>
    /// 关联量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 回流工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTPROCESSID")]
    public string Fnextprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 条码内码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 送修单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORDID")]
    public string Frepordid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
