using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料辅助属性值表体
/// </summary>
[SugarTable("T_BD_AUXPTYVALUEENTITY")]
public class TBdAuxptyvalueentity : BaseEntity
{
    /// <summary>
    /// 选择
    /// </summary>
    [SugarColumn(ColumnName = "FISSELECT")]
    public bool Fisselect { get; set; }

    /// <summary>
    /// 辅助属性内码
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPTYID")]
    public string Fauxptyid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用
    /// </summary>
    [SugarColumn(ColumnName = "FISDISABLE")]
    public bool Fisdisable { get; set; }

    /// <summary>
    /// 默认
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULT")]
    public bool Fisdefault { get; set; }

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
