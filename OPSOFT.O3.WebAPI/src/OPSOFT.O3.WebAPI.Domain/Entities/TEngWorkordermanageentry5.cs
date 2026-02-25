using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工单派工管理-维护工种/检修人
/// </summary>
[SugarTable("T_ENG_WORKORDERMANAGEENTRY5")]
public class TEngWorkordermanageentry5 : BaseEntity
{
    /// <summary>
    /// 管理部位工单内码
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONID")]
    public string Frelationid { get; set; } = string.Empty;

    /// <summary>
    /// 维护人员内码
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTMANID")]
    public string Fmaintmanid { get; set; } = string.Empty;

    /// <summary>
    /// 工种内码
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTID")]
    public string Fcraftid { get; set; } = string.Empty;

    /// <summary>
    /// 实际工时
    /// </summary>
    [SugarColumn(ColumnName = "FACRAFTTIME")]
    public decimal Facrafttime { get; set; }

    /// <summary>
    /// 工时费用
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTCOST")]
    public decimal Fcraftcost { get; set; }

    /// <summary>
    /// 部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

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
