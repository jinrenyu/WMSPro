using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产叫料
/// </summary>
[SugarTable("T_PRD_CALL")]
public class TPrdCall : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public string Fmoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ")]
    public int Fmoentryseq { get; set; }

    /// <summary>
    /// 生产订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKRESOURCEID")]
    public string Fworkresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 叫料时间
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 工序
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

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
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 响应状态
    /// </summary>
    [SugarColumn(ColumnName = "FRESPONSE")]
    public int Fresponse { get; set; }

    /// <summary>
    /// 拣货任务内码
    /// </summary>
    [SugarColumn(ColumnName = "FPLANTASKID")]
    public string Fplantaskid { get; set; } = string.Empty;

    /// <summary>
    /// 承运人
    /// </summary>
    [SugarColumn(ColumnName = "FCARRIER")]
    public string Fcarrier { get; set; } = string.Empty;

    /// <summary>
    /// 送达时间
    /// </summary>
    [SugarColumn(ColumnName = "FSERVICETIME")]
    public DateTime? Fservicetime { get; set; }

    /// <summary>
    /// 关闭时间
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSETIME")]
    public DateTime? Fclosetime { get; set; }

    /// <summary>
    /// 是否发料
    /// </summary>
    [SugarColumn(ColumnName = "FISISSUE")]
    public bool Fisissue { get; set; }

    /// <summary>
    /// 响应时间
    /// </summary>
    [SugarColumn(ColumnName = "FRESPONSETIME")]
    public DateTime? Fresponsetime { get; set; }

    /// <summary>
    /// 配送时间
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYTIME")]
    public DateTime? Fdeliverytime { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
