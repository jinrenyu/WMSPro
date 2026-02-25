using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档
/// </summary>
[SugarTable("T_BD_BARCODERS")]
public class TBdBarcoders : BaseEntity
{
    /// <summary>
    /// 总数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALQTY")]
    public decimal Ftotalqty { get; set; }

    /// <summary>
    /// 制造日期
    /// </summary>
    [SugarColumn(ColumnName = "FMFGDATE")]
    public DateTime? Fmfgdate { get; set; }

    /// <summary>
    /// 原始条码
    /// </summary>
    [SugarColumn(ColumnName = "FOBARCODE")]
    public string Fobarcode { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 条码状态
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODESTATUS")]
    public int Fbarcodestatus { get; set; }

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE")]
    public int Fbartype { get; set; }

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 是否箱码
    /// </summary>
    [SugarColumn(ColumnName = "FISBOX")]
    public bool Fisbox { get; set; }

    /// <summary>
    /// 箱码条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBOXCODETYPE")]
    public int Fboxcodetype { get; set; }

    /// <summary>
    /// 是否混装
    /// </summary>
    [SugarColumn(ColumnName = "FISMIX")]
    public bool Fismix { get; set; }

    /// <summary>
    /// 批次内码
    /// </summary>
    [SugarColumn(ColumnName = "FLOTID")]
    public string Flotid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUS")]
    public int Fstockstatus { get; set; }

    /// <summary>
    /// 质量(0=未检验 1=合格 2=不合格 3=待判定
    /// </summary>
    [SugarColumn(ColumnName = "FQUALITYSTATUS")]
    public int Fqualitystatus { get; set; }
}
