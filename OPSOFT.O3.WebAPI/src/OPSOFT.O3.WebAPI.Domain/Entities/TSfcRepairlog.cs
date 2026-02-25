using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报-记录单
/// </summary>
[SugarTable("T_SFC_REPAIRLOG")]
public class TSfcRepairlog : BaseEntity
{
    /// <summary>
    /// 送修单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRID")]
    public string Frepairid { get; set; } = string.Empty;

    /// <summary>
    /// 维修站内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTATIONID")]
    public string Fstationid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 当前班次内码
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTID")]
    public string Fshiftid { get; set; } = string.Empty;

    /// <summary>
    /// 当前班组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID")]
    public string Fteamid { get; set; } = string.Empty;

    /// <summary>
    /// 提交人
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITER")]
    public string Fsubmiter { get; set; } = string.Empty;

    /// <summary>
    /// 维修时间
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTAINTIME")]
    public DateTime? Fmaintaintime { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
