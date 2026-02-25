using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外价格
/// </summary>
[SugarTable("T_BD_OUTPRICE")]
public class TBdOutprice : BaseEntity
{
    /// <summary>
    /// 代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 订货量（从）
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTQTY")]
    public decimal Fstartqty { get; set; }

    /// <summary>
    /// 订货量（到）
    /// </summary>
    [SugarColumn(ColumnName = "FENDQTY")]
    public decimal Fendqty { get; set; }

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 核定价格
    /// </summary>
    [SugarColumn(ColumnName = "FAPPROVEDPRICE")]
    public decimal Fapprovedprice { get; set; }

    /// <summary>
    /// 车削价格
    /// </summary>
    [SugarColumn(ColumnName = "FTURNINGPRICE")]
    public decimal Fturningprice { get; set; }

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

    /// <summary>
    /// 折扣率
    /// </summary>
    [SugarColumn(ColumnName = "FDISCOUNT")]
    public decimal Fdiscount { get; set; }

    /// <summary>
    /// 订货提前期
    /// </summary>
    [SugarColumn(ColumnName = "FLEADTIME")]
    public int Fleadtime { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FQUOTETIME")]
    public DateTime? Fquotetime { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FFAILUREDATE")]
    public DateTime? Ffailuredate { get; set; }

    /// <summary>
    /// 单价类型
    /// </summary>
    [SugarColumn(ColumnName = "FPTYPE")]
    public int Fptype { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
}
