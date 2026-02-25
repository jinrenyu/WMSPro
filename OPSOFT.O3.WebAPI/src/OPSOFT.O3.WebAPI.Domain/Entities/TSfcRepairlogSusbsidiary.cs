using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报-记录单-辅料
/// </summary>
[SugarTable("T_SFC_REPAIRLOG_SUSBSIDIARY")]
public class TSfcRepairlogSusbsidiary : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 条码类别
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODETYPE")]
    public int Fbarcodetype { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 批次号/SN号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAURPROP")]
    public string Faurprop { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

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
