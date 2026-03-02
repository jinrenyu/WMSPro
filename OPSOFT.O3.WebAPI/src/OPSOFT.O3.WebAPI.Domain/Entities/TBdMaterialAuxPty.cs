using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料辅助属性控制
/// </summary>
[SugarTable("T_BD_MATERIALAUXPTY")]
public class TBdMaterialAuxPty : BaseEntryEntity
{
    /// <summary>
    /// 辅助属性内码
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPERTYID")]
    public string FAuxPropertyId { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用
    /// </summary>
    [SugarColumn(ColumnName = "FISENABLE")]
    public bool FIsEnable { get; set; }

    /// <summary>
    /// 组织控制
    /// </summary>
    [SugarColumn(ColumnName = "FISCOMCONTROL")]
    public bool FIsComControl { get; set; }

    /// <summary>
    /// 必录
    /// </summary>
    [SugarColumn(ColumnName = "FISMUSTINPUT")]
    public bool FIsMustInput { get; set; }

    /// <summary>
    /// 影响价格
    /// </summary>
    [SugarColumn(ColumnName = "FISAFFECTPRICE")]
    public bool FIsAffectPrice { get; set; }

    /// <summary>
    /// 影响计划
    /// </summary>
    [SugarColumn(ColumnName = "FISAFFECTPLAN")]
    public bool FIsAffectPlan { get; set; }

    /// <summary>
    /// 影响出库成本
    /// </summary>
    [SugarColumn(ColumnName = "FISAFFECTCOST")]
    public bool FIsAffectCost { get; set; }

    /// <summary>
    /// 值设置状态
    /// </summary>
    [SugarColumn(ColumnName = "FVALUESET")]
    public bool FValueSet { get; set; }

    /// <summary>
    /// 主数据内码
    /// </summary>
    [SugarColumn(ColumnName = "FMASTERID")]
    public string FMasterId { get; set; } = string.Empty;
}
