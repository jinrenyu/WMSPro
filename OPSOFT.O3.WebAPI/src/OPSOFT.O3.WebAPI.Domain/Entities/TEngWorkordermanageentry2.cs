using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工单派工管理-备件
/// </summary>
[SugarTable("T_ENG_WORKORDERMANAGEENTRY2")]
public class TEngWorkordermanageentry2 : BaseEntity
{
    /// <summary>
    /// 管理部位工单内码
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONID")]
    public string Frelationid { get; set; } = string.Empty;

    /// <summary>
    /// 备件内码
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTID")]
    public string Fsparepartid { get; set; } = string.Empty;

    /// <summary>
    /// 备件数量
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTCOUNT")]
    public decimal Fsparepartcount { get; set; }

    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 备件费用
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTSCOST")]
    public decimal Fsparepartscost { get; set; }

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
