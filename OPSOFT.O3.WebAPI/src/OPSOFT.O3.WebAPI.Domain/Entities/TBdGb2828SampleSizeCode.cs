using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// GB2828样本量字码
/// </summary>
[SugarTable("T_BD_GB2828SAMPLESIZECODE")]
public class TBdGb2828samplesizecode : BaseEntity
{
    /// <summary>
    /// 批量范围起始
    /// </summary>
    [SugarColumn(ColumnName = "FRANGESTART")]
    public decimal Frangestart { get; set; }

    /// <summary>
    /// 批量范围结束
    /// </summary>
    [SugarColumn(ColumnName = "FRANGEEND")]
    public decimal Frangeend { get; set; }

    /// <summary>
    /// 特殊检验水平S-1
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIALLEVELSONE")]
    public string Fspeciallevelsone { get; set; } = string.Empty;

    /// <summary>
    /// 特殊检验水平S-2
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIALLEVELSTWO")]
    public string Fspeciallevelstwo { get; set; } = string.Empty;

    /// <summary>
    /// 特殊检验水平S-3
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIALLEVELSTHR")]
    public string Fspeciallevelsthr { get; set; } = string.Empty;

    /// <summary>
    /// 特殊检验水平S-4
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIALLEVELSFOU")]
    public string Fspeciallevelsfou { get; set; } = string.Empty;

    /// <summary>
    /// 一般经检验水平Ⅰ
    /// </summary>
    [SugarColumn(ColumnName = "FGENERALLEVELONE")]
    public string Fgenerallevelone { get; set; } = string.Empty;

    /// <summary>
    /// 一般经检验水平Ⅱ
    /// </summary>
    [SugarColumn(ColumnName = "FGENERALLEVELTWO")]
    public string Fgeneralleveltwo { get; set; } = string.Empty;

    /// <summary>
    /// 一般经检验水平Ⅲ
    /// </summary>
    [SugarColumn(ColumnName = "FGENERALLEVELTHR")]
    public string Fgenerallevelthr { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

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
