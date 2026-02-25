using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售出库单
/// </summary>
[SugarTable("T_SAL_OUTSTOCK")]
public class TSalOutstock : BaseEntity
{
    /// <summary>
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEORGID")]
    public string Fsettleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FBILLAMOUNT")]
    public decimal Fbillamount { get; set; }

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 运输单号
    /// </summary>
    [SugarColumn(ColumnName = "FCARRIAGENO")]
    public string Fcarriageno { get; set; } = string.Empty;

    /// <summary>
    /// 承运商
    /// </summary>
    [SugarColumn(ColumnName = "FCARRIERID")]
    public string Fcarrierid { get; set; } = string.Empty;

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
    /// 客户
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMERID")]
    public string Fcustomerid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 发货部门
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDEPTID")]
    public string Fdeliverydeptid { get; set; } = string.Empty;

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
    /// ERP单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string Ferpid { get; set; } = string.Empty;

    /// <summary>
    /// ERP单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string Ferpno { get; set; } = string.Empty;

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public decimal Fexchangerate { get; set; }

    /// <summary>
    /// 汇率类型
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGETYPEID")]
    public string Fexchangetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 管易发货日期
    /// </summary>
    [SugarColumn(ColumnName = "FGYDATE")]
    public DateTime? Fgydate { get; set; }

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FHEADLOCATIONID")]
    public string Fheadlocationid { get; set; } = string.Empty;

    /// <summary>
    /// 收货人姓名
    /// </summary>
    [SugarColumn(ColumnName = "FLINKMAN")]
    public string Flinkman { get; set; } = string.Empty;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FLINKPHONE")]
    public string Flinkphone { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 付款方
    /// </summary>
    [SugarColumn(ColumnName = "FPAYERID")]
    public string Fpayerid { get; set; } = string.Empty;

    /// <summary>
    /// 收货方联系人
    /// </summary>
    [SugarColumn(ColumnName = "FRECCONTACTID")]
    public string Freccontactid { get; set; } = string.Empty;

    /// <summary>
    /// 收货方地址
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEADDRESS")]
    public string Freceiveaddress { get; set; } = string.Empty;

    /// <summary>
    /// 收货方
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVERID")]
    public string Freceiverid { get; set; } = string.Empty;

    /// <summary>
    /// 销售部门
    /// </summary>
    [SugarColumn(ColumnName = "FSALEDEPTID")]
    public string Fsaledeptid { get; set; } = string.Empty;

    /// <summary>
    /// 销售组织
    /// </summary>
    [SugarColumn(ColumnName = "FSALEORGID")]
    public string Fsaleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 销售员
    /// </summary>
    [SugarColumn(ColumnName = "FSALESMANID")]
    public string Fsalesmanid { get; set; } = string.Empty;

    /// <summary>
    /// 结算方
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEID")]
    public string Fsettleid { get; set; } = string.Empty;

    /// <summary>
    /// 结算方式
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLETYPEID")]
    public string Fsettletypeid { get; set; } = string.Empty;

    /// <summary>
    /// 仓管员
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERID")]
    public string Fstockerid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 是否同步
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

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
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 录入类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

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
