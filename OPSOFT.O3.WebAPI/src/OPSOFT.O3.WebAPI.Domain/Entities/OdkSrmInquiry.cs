using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 询价单表头
/// </summary>
[SugarTable("ODK_SRM_Inquiry")]
public class OdkSrmInquiry : BaseEntity
{
    /// <summary>
    /// 询价单标题
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 职员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 询价单日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 询价方式(A:公开 B:邀请
    /// </summary>
    [SugarColumn(ColumnName = "FMETHOD")]
    public string Fmethod { get; set; } = string.Empty;

    /// <summary>
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE", IsNullable = true)]
    public bool? FISRELEASE { get; set; }

    /// <summary>
    /// 询价轮次
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERNUM", IsNullable = true)]
    public int? FTENDERNUM { get; set; }

    /// <summary>
    /// 交付方式
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYMODE", IsNullable = true)]
    public string FDELIVERYMODE { get; set; } = string.Empty;

    /// <summary>
    /// 显示排名
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWRANK", IsNullable = true)]
    public bool? FSHOWRANK { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS", IsNullable = true)]
    public int? FBILLSTATUS { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 物料组
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID", IsNullable = true)]
    public string FMATGROUPID { get; set; } = string.Empty;

    /// <summary>
    /// 报价截至日期
    /// </summary>
    [SugarColumn(ColumnName = "FDEADDATE", IsNullable = true)]
    public DateTime? FDEADDATE { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME", IsNullable = true)]
    public DateTime? FHASREADTIME { get; set; }

    /// <summary>
    /// 是否含运费
    /// </summary>
    [SugarColumn(ColumnName = "FISFARE", IsNullable = true)]
    public bool? FISFARE { get; set; }

    /// <summary>
    /// 显示最低价
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWMIN", IsNullable = true)]
    public bool? FSHOWMIN { get; set; }

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYSITE", IsNullable = true)]
    public string FDELIVERYSITE { get; set; } = string.Empty;

    /// <summary>
    /// 当前轮次
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENTNUM", IsNullable = true)]
    public int? FCURRENTNUM { get; set; }

    /// <summary>
    /// 交货方式
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYMETHOD", IsNullable = true)]
    public string FDELIVERYMETHOD { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD", IsNullable = true)]
    public int? FHASREAD { get; set; }

    /// <summary>
    /// 币种
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID", IsNullable = true)]
    public string FCURRENCYID { get; set; } = string.Empty;
}
