using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料辅助属性值
/// </summary>
[SugarTable("T_BD_AUXPTYVALUE")]
public class TBdAuxptyvalue : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALAUXPROPERTYID")]
    public string Fmaterialauxpropertyid { get; set; } = string.Empty;

    /// <summary>
    /// 功能标识
    /// </summary>
    [SugarColumn(ColumnName = "FFORMID")]
    public string Fformid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;

    /// <summary>
    /// 系统预置
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }
}
