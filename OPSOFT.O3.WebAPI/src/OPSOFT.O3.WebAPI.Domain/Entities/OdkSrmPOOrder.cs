using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购订单
/// </summary>
[SugarTable("ODK_SRM_POOrder")]
public class OdkSrmPOOrder : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 采购员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 物料组
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID")]
    public string Fmatgroupid { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 收货电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE")]
    public string Fphone { get; set; } = string.Empty;

    /// <summary>
    /// 收货方式
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYMETHOD")]
    public string Fdeliverymethod { get; set; } = string.Empty;

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYSITE")]
    public string Fdeliverysite { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETRANTYPE")]
    public int Fsourcetrantype { get; set; }

    /// <summary>
    /// 是否加急
    /// </summary>
    [SugarColumn(ColumnName = "FISURGENT")]
    public bool Fisurgent { get; set; }

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
    public bool Fhasread { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME")]
    public DateTime? Fhasreadtime { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

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
