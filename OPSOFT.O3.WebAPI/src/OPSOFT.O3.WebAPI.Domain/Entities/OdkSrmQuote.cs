using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 报价单
/// </summary>
[SugarTable("ODK_SRM_Quote")]
public class OdkSrmQuote : BaseEntity
{
    /// <summary>
    /// 报价日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 报价人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 询价单内码
    /// </summary>
    [SugarColumn(ColumnName = "FINQUIRYID")]
    public string Finquiryid { get; set; } = string.Empty;

    /// <summary>
    /// 询价单供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPDETAILID")]
    public string Fsupdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

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
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

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
