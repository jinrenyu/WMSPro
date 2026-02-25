using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 用电监测PDA
/// </summary>
[SugarTable("T_EC_ELECTRICITYPDA")]
public class TEcElectricitypda : BaseEntity
{
    /// <summary>
    /// 设备id
    /// </summary>
    [SugarColumn(ColumnName = "FID")]
    public string Fid { get; set; } = string.Empty;

    /// <summary>
    /// MODELNBR
    /// </summary>
    [SugarColumn(ColumnName = "FMODELNBR")]
    public string Fmodelnbr { get; set; } = string.Empty;

    /// <summary>
    /// FMODELNAME
    /// </summary>
    [SugarColumn(ColumnName = "FMODELNAME")]
    public string Fmodelname { get; set; } = string.Empty;

    /// <summary>
    /// FMODELTYPE
    /// </summary>
    [SugarColumn(ColumnName = "FMODELTYPE")]
    public decimal Fmodeltype { get; set; }

    /// <summary>
    /// FFULLPATH
    /// </summary>
    [SugarColumn(ColumnName = "FFULLPATH")]
    public string Ffullpath { get; set; } = string.Empty;

    /// <summary>
    /// FFULLNAME
    /// </summary>
    [SugarColumn(ColumnName = "FFULLNAME")]
    public string Ffullname { get; set; } = string.Empty;

    /// <summary>
    /// FTODAYTOTALBATTERY
    /// </summary>
    [SugarColumn(ColumnName = "FTODAYTOTALBATTERY")]
    public decimal Ftodaytotalbattery { get; set; }

    /// <summary>
    /// FYESTERDATTOTALBATTERY
    /// </summary>
    [SugarColumn(ColumnName = "FYESTERDATTOTALBATTERY")]
    public decimal Fyesterdattotalbattery { get; set; }

    /// <summary>
    /// FPOINTVALUE
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTVALUEP")]
    public decimal Fpointvaluep { get; set; }

    /// <summary>
    /// FPOINTNBR
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTNBRP")]
    public string Fpointnbrp { get; set; } = string.Empty;

    /// <summary>
    /// FPOINTNAME
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTNAMEP")]
    public string Fpointnamep { get; set; } = string.Empty;

    /// <summary>
    /// FUNIT
    /// </summary>
    [SugarColumn(ColumnName = "FUNITP")]
    public string Funitp { get; set; } = string.Empty;

    /// <summary>
    /// FPOINTVALUE
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTVALUEU")]
    public decimal Fpointvalueu { get; set; }

    /// <summary>
    /// FPOINTNBR
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTNBRU")]
    public string Fpointnbru { get; set; } = string.Empty;

    /// <summary>
    /// FPOINTNAME
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTNAMEU")]
    public string Fpointnameu { get; set; } = string.Empty;

    /// <summary>
    /// FUNIT
    /// </summary>
    [SugarColumn(ColumnName = "FUNITU")]
    public string Funitu { get; set; } = string.Empty;

    /// <summary>
    /// FPOINTVALUE
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTVALUEI")]
    public decimal Fpointvaluei { get; set; }

    /// <summary>
    /// FPOINTNBR
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTNBRI")]
    public string Fpointnbri { get; set; } = string.Empty;

    /// <summary>
    /// FPOINTNAME
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTNAMEI")]
    public string Fpointnamei { get; set; } = string.Empty;

    /// <summary>
    /// FUNIT
    /// </summary>
    [SugarColumn(ColumnName = "FUNITI")]
    public string Funiti { get; set; } = string.Empty;

    /// <summary>
    /// FREALBATTERY
    /// </summary>
    [SugarColumn(ColumnName = "FREALBATTERY")]
    public decimal Frealbattery { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
