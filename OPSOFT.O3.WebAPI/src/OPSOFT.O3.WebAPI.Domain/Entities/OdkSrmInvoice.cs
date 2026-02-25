using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 预制发票表头
/// </summary>
[SugarTable("ODK_SRM_Invoice")]
public class OdkSrmInvoice : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FROB")]
    public string Frob { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 货币内码
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 结算未税总金额
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEUNTAXAMOUNT")]
    public decimal Fsettleuntaxamount { get; set; }

    /// <summary>
    /// 结算含税总金额
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLETAXAMOUNT")]
    public decimal Fsettletaxamount { get; set; }

    /// <summary>
    /// 总税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT")]
    public decimal Ftaxamount { get; set; }

    /// <summary>
    /// 税控发票号码
    /// </summary>
    [SugarColumn(ColumnName = "FTAXINVOICE")]
    public string Ftaxinvoice { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD")]
    public int Fhasread { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME")]
    public DateTime? Fhasreadtime { get; set; }

    /// <summary>
    /// 审核意见
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYNOTE")]
    public string Fverifynote { get; set; } = string.Empty;

    /// <summary>
    /// 复审意见
    /// </summary>
    [SugarColumn(ColumnName = "FREVERIFYNOTE")]
    public string Freverifynote { get; set; } = string.Empty;

    /// <summary>
    /// 拒绝原因
    /// </summary>
    [SugarColumn(ColumnName = "FREFUREASON")]
    public string Frefureason { get; set; } = string.Empty;

    /// <summary>
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE")]
    public bool Fisrelease { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
