using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售退货单
/// </summary>
[SugarTable("T_SAL_RETURNSTOCK")]
public class TSalReturnstock : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 销售组织
    /// </summary>
    [SugarColumn(ColumnName = "FSALEORGID")]
    public string Fsaleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 销售部门
    /// </summary>
    [SugarColumn(ColumnName = "FSALEDEPTID")]
    public string Fsaledeptid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 退货原因
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNREASON")]
    public string Freturnreason { get; set; } = string.Empty;

    /// <summary>
    /// 销售员
    /// </summary>
    [SugarColumn(ColumnName = "FSALESMANID")]
    public string Fsalesmanid { get; set; } = string.Empty;

    /// <summary>
    /// 第三方单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FTHIRDBILLNO")]
    public string Fthirdbillno { get; set; } = string.Empty;

    /// <summary>
    /// 第三方单据平台单号
    /// </summary>
    [SugarColumn(ColumnName = "FTHIRDBILLID")]
    public string Fthirdbillid { get; set; } = string.Empty;

    /// <summary>
    /// 第三方系统来源
    /// </summary>
    [SugarColumn(ColumnName = "FTHIRDSRCTYPE")]
    public string Fthirdsrctype { get; set; } = string.Empty;

    /// <summary>
    /// 库存部门
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKDEPTID")]
    public string Fstockdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 管易退货日期
    /// </summary>
    [SugarColumn(ColumnName = "FGYDATE")]
    public DateTime? Fgydate { get; set; }

    /// <summary>
    /// 退货客户
    /// </summary>
    [SugarColumn(ColumnName = "FRETCUSTID")]
    public string Fretcustid { get; set; } = string.Empty;

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FHEADLOCATIONID")]
    public string Fheadlocationid { get; set; } = string.Empty;

    /// <summary>
    /// 承运商
    /// </summary>
    [SugarColumn(ColumnName = "FCARRIERID")]
    public string Fcarrierid { get; set; } = string.Empty;

    /// <summary>
    /// 运输单号
    /// </summary>
    [SugarColumn(ColumnName = "FCARRIAGENO")]
    public string Fcarriageno { get; set; } = string.Empty;

    /// <summary>
    /// 仓管员
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERID")]
    public string Fstockerid { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FBILLAMOUNT")]
    public decimal Fbillamount { get; set; }

    /// <summary>
    /// 金额（本位币）
    /// </summary>
    [SugarColumn(ColumnName = "FBILLAMOUNT_LC")]
    public decimal FbillamountLc { get; set; }

    /// <summary>
    /// 汇率类型
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGETYPEID")]
    public string Fexchangetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public decimal Fexchangerate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
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
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// ERP内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string Ferpid { get; set; } = string.Empty;

    /// <summary>
    /// ERP编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string Ferpno { get; set; } = string.Empty;

    /// <summary>
    /// 是否同步ERP
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

    /// <summary>
    /// 录入类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

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
