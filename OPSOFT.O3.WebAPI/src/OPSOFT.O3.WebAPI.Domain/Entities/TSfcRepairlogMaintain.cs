using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报-记录单-维修处理
/// </summary>
[SugarTable("T_SFC_REPAIRLOG_MAINTAIN")]
public class TSfcRepairlogMaintain : BaseEntity
{
    /// <summary>
    /// 批次号/SN号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 新条码
    /// </summary>
    [SugarColumn(ColumnName = "FNEWBARCODE")]
    public string Fnewbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 不良内码
    /// </summary>
    [SugarColumn(ColumnName = "FREASONID")]
    public string Freasonid { get; set; } = string.Empty;

    /// <summary>
    /// 处理方式
    /// </summary>
    [SugarColumn(ColumnName = "FDISPOAL")]
    public int Fdispoal { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 目标工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 目标维修站内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTATIONID")]
    public string Fstationid { get; set; } = string.Empty;

    /// <summary>
    /// 需要复检
    /// </summary>
    [SugarColumn(ColumnName = "FISRECHECK")]
    public bool Fisrecheck { get; set; }

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
