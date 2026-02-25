using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购入库单
/// </summary>
[SugarTable("T_STK_INSTOCK")]
public class TStkInstock : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 录入类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 入库日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 收料部门
    /// </summary>
    [SugarColumn(ColumnName = "FMRDEPTID")]
    public string Fmrdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 业务员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 仓管员
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERID")]
    public string Fstockerid { get; set; } = string.Empty;

    /// <summary>
    /// 需求组织
    /// </summary>
    [SugarColumn(ColumnName = "FDEMANDORGID")]
    public string Fdemandorgid { get; set; } = string.Empty;

    /// <summary>
    /// 采购组织
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASEORGID")]
    public string Fpurchaseorgid { get; set; } = string.Empty;

    /// <summary>
    /// 采购部门
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASEDEPTID")]
    public string Fpurchasedeptid { get; set; } = string.Empty;

    /// <summary>
    /// 采购员
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASERID")]
    public string Fpurchaserid { get; set; } = string.Empty;

    /// <summary>
    /// 供货方
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYERID")]
    public string Fsupplyerid { get; set; } = string.Empty;

    /// <summary>
    /// 供货方地址
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYADDRESS")]
    public string Fsupplyaddress { get; set; } = string.Empty;

    /// <summary>
    /// 结算方
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEID")]
    public string Fsettleid { get; set; } = string.Empty;

    /// <summary>
    /// 收款方
    /// </summary>
    [SugarColumn(ColumnName = "FCHARGEID")]
    public string Fchargeid { get; set; } = string.Empty;

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 汇率类型
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGETYPEID")]
    public string Fexchangetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public string Fexchangerate { get; set; } = string.Empty;

    /// <summary>
    /// 供货方联系人
    /// </summary>
    [SugarColumn(ColumnName = "FPROVIDERCONTACTID")]
    public string Fprovidercontactid { get; set; } = string.Empty;

    /// <summary>
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEORGID")]
    public string Fsettleorgid { get; set; } = string.Empty;

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
    /// 同步单据
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

    /// <summary>
    /// ERP编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string Ferpno { get; set; } = string.Empty;

    /// <summary>
    /// ERP内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string Ferpid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
