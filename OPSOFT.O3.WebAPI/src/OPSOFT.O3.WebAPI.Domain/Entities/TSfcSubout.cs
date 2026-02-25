using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序委外发出
/// </summary>
[SugarTable("T_SFC_SUBOUT")]
public class TSfcSubout : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATA")]
    public DateTime? Fdata { get; set; }

    /// <summary>
    /// 发出员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 车削价格
    /// </summary>
    [SugarColumn(ColumnName = "FTURNINGPRICE")]
    public decimal Fturningprice { get; set; }

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
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 加工费
    /// </summary>
    [SugarColumn(ColumnName = "FFEEAMOUNT")]
    public decimal Ffeeamount { get; set; }

    /// <summary>
    /// 委外发出数量
    /// </summary>
    [SugarColumn(ColumnName = "FOUTQTY")]
    public decimal Foutqty { get; set; }

    /// <summary>
    /// 委外加工门卫放行单打印次数
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTTIME")]
    public int Fprinttime { get; set; }

    /// <summary>
    /// 委外加工签核单打印次数
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTTIMEA")]
    public int Fprinttimea { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 计划接收日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANREDATE")]
    public DateTime? Fplanredate { get; set; }

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
    /// 扫描条码来源
    /// </summary>
    [SugarColumn(ColumnName = "FSCANTYPE")]
    public string Fscantype { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
