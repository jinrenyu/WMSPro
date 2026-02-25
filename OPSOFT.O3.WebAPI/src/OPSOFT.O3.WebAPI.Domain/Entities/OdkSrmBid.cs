using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 投标单
/// </summary>
[SugarTable("ODK_SRM_Bid")]
public class OdkSrmBid : BaseEntity
{
    /// <summary>
    /// 投标单建立日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 投标人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 招标单内码
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERINTERID")]
    public string Ftenderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 物料组
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID")]
    public string Fmatgroupid { get; set; } = string.Empty;

    /// <summary>
    /// 投标摘要
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 付款条件说明
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
    public string Fhasreadtime { get; set; } = string.Empty;

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
