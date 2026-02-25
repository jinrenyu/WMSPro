using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外订单
/// </summary>
[SugarTable("T_SUB_REQORDER")]
public class TSubReqorder : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 计划类别
    /// </summary>
    [SugarColumn(ColumnName = "FPLANCATEGORY")]
    public string Fplancategory { get; set; } = string.Empty;

    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FDOCUMENTSTATUS")]
    public string Fdocumentstatus { get; set; } = string.Empty;

    /// <summary>
    /// 业务员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYPLACE")]
    public string Fdeliveryplace { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 汇率类型
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATETYPE")]
    public string Fexchangeratetype { get; set; } = string.Empty;

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public decimal Fexchangerate { get; set; }

    /// <summary>
    /// 委外类型
    /// </summary>
    [SugarColumn(ColumnName = "FINVSTYLE")]
    public string Finvstyle { get; set; } = string.Empty;

    /// <summary>
    /// 主管内码
    /// </summary>
    [SugarColumn(ColumnName = "FMANGERID")]
    public string Fmangerid { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 是否同步数据
    /// </summary>
    [SugarColumn(ColumnName = "FISSYN")]
    public bool Fissyn { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
    /// 结算方式
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLESTYLE")]
    public string Fsettlestyle { get; set; } = string.Empty;

    /// <summary>
    /// 结算日期
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEDATE")]
    public DateTime? Fsettledate { get; set; }

    /// <summary>
    /// 结算币别
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECURRID")]
    public string Fsettlecurrid { get; set; } = string.Empty;

    /// <summary>
    /// 计划组
    /// </summary>
    [SugarColumn(ColumnName = "FWORKGROUPID")]
    public string Fworkgroupid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
