using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 招标单
/// </summary>
[SugarTable("ODK_SRM_Tender")]
public class OdkSrmTender : BaseEntity
{
    /// <summary>
    /// 项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 招标人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 招标书建立日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 招标公司
    /// </summary>
    [SugarColumn(ColumnName = "FIDCOMPANY")]
    public string Fidcompany { get; set; } = string.Empty;

    /// <summary>
    /// 招标方式(A:公开招标  B:邀请招标
    /// </summary>
    [SugarColumn(ColumnName = "FTDMETHOD")]
    public string Ftdmethod { get; set; } = string.Empty;

    /// <summary>
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE", IsNullable = true)]
    public bool? FISRELEASE { get; set; }

    /// <summary>
    /// 招标轮次
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERNUM", IsNullable = true)]
    public int? FTENDERNUM { get; set; }

    /// <summary>
    /// 评审文件提交截止日期
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSDEADDATE", IsNullable = true)]
    public DateTime? FASSESSDEADDATE { get; set; }

    /// <summary>
    /// 货币
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID", IsNullable = true)]
    public string FCURRENCYID { get; set; } = string.Empty;

    /// <summary>
    /// 开标地点
    /// </summary>
    [SugarColumn(ColumnName = "FOPENSITE", IsNullable = true)]
    public string FOPENSITE { get; set; } = string.Empty;

    /// <summary>
    /// 评审完成
    /// </summary>
    [SugarColumn(ColumnName = "FISASSESSFINISH", IsNullable = true)]
    public bool? FISASSESSFINISH { get; set; }

    /// <summary>
    /// 资审要求
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSDEMAND", IsNullable = true)]
    public string FASSESSDEMAND { get; set; } = string.Empty;

    /// <summary>
    /// 是否在线评标
    /// </summary>
    [SugarColumn(ColumnName = "FISEVALUATE", IsNullable = true)]
    public bool? FISEVALUATE { get; set; }

    /// <summary>
    /// 显示排名
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWRANK", IsNullable = true)]
    public bool? FSHOWRANK { get; set; }

    /// <summary>
    /// 付款套件说明
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 投标截止时间
    /// </summary>
    [SugarColumn(ColumnName = "FBIDDEADDATE", IsNullable = true)]
    public DateTime? FBIDDEADDATE { get; set; }

    /// <summary>
    /// 资审时间从
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSBDATE", IsNullable = true)]
    public DateTime? FASSESSBDATE { get; set; }

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS", IsNullable = true)]
    public int? FBILLSTATUS { get; set; }

    /// <summary>
    /// 物料组
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID", IsNullable = true)]
    public string FMATGROUPID { get; set; } = string.Empty;

    /// <summary>
    /// 项目地点
    /// </summary>
    [SugarColumn(ColumnName = "FPROJSITE", IsNullable = true)]
    public string FPROJSITE { get; set; } = string.Empty;

    /// <summary>
    /// 评标模板
    /// </summary>
    [SugarColumn(ColumnName = "FTEMPLATEID", IsNullable = true)]
    public string FTEMPLATEID { get; set; } = string.Empty;

    /// <summary>
    /// 缴纳截止时间
    /// </summary>
    [SugarColumn(ColumnName = "FBONDENDTIME", IsNullable = true)]
    public DateTime? FBONDENDTIME { get; set; }

    /// <summary>
    /// 是否开标
    /// </summary>
    [SugarColumn(ColumnName = "FISOPEN", IsNullable = true)]
    public bool? FISOPEN { get; set; }

    /// <summary>
    /// 开标时间
    /// </summary>
    [SugarColumn(ColumnName = "FOPENDATE", IsNullable = true)]
    public DateTime? FOPENDATE { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME", IsNullable = true)]
    public DateTime? FHASREADTIME { get; set; }

    /// <summary>
    /// 是否资格评审
    /// </summary>
    [SugarColumn(ColumnName = "FISASSESS", IsNullable = true)]
    public bool? FISASSESS { get; set; }

    /// <summary>
    /// 显示最低价
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWMIN", IsNullable = true)]
    public bool? FSHOWMIN { get; set; }

    /// <summary>
    /// 保证金备注
    /// </summary>
    [SugarColumn(ColumnName = "FBONDDESC", IsNullable = true)]
    public string FBONDDESC { get; set; } = string.Empty;

    /// <summary>
    /// 资审附件
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSAPPENDIX", IsNullable = true)]
    public string FASSESSAPPENDIX { get; set; } = string.Empty;

    /// <summary>
    /// 是否缴纳保证金
    /// </summary>
    [SugarColumn(ColumnName = "FISBOND", IsNullable = true)]
    public bool? FISBOND { get; set; }

    /// <summary>
    /// 资审时间至
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSEDATE", IsNullable = true)]
    public DateTime? FASSESSEDATE { get; set; }

    /// <summary>
    /// 资格评审说明
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSTEXT", IsNullable = true)]
    public string FASSESSTEXT { get; set; } = string.Empty;

    /// <summary>
    /// 保证金金额
    /// </summary>
    [SugarColumn(ColumnName = "FBOND", IsNullable = true)]
    public decimal? FBOND { get; set; }

    /// <summary>
    /// 当前轮次
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENTNUM", IsNullable = true)]
    public int? FCURRENTNUM { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD", IsNullable = true)]
    public bool? FHASREAD { get; set; }

    /// <summary>
    /// 招标类型(A:货物招标  B:服务招标  C:工程招标)
    /// </summary>
    [SugarColumn(ColumnName = "FTDTYPE", IsNullable = true)]
    public string FTDTYPE { get; set; } = string.Empty;
}
