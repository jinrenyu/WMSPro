using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 简单生产入库单-明细
/// </summary>
[SugarTable("T_SP_INSTOCKENTRY")]
public class TSpInstockentry : BaseEntity
{
    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 入库类型
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKTYPE")]
    public bool Finstocktype { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 应收数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 基本单位应收数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEMUSTQTY")]
    public decimal Fbasemustqty { get; set; }

    /// <summary>
    /// 实收数量
    /// </summary>
    [SugarColumn(ColumnName = "FREALQTY")]
    public decimal Frealqty { get; set; }

    /// <summary>
    /// 基本单位实收数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEREALQTY")]
    public decimal Fbaserealqty { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKUNITID")]
    public string Fstockunitid { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位实收数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKREALQTY")]
    public decimal Fstockrealqty { get; set; }

    /// <summary>
    /// 实收数量(库存辅单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECREALQTY")]
    public decimal Fsecrealqty { get; set; }
}
