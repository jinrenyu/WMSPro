using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序委外订单表体-序列号明细
/// </summary>
[SugarTable("T_SFC_PROSUBORDERENTRY1")]
public class TSfcProsuborderentry1 : BaseEntity
{
    /// <summary>
    /// 序列号或条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 生产批次号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODETYPE")]
    public int Fbarcodetype { get; set; }

    /// <summary>
    /// 是否产生委外发出
    /// </summary>
    [SugarColumn(ColumnName = "FISPUSH")]
    public bool Fispush { get; set; }

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
