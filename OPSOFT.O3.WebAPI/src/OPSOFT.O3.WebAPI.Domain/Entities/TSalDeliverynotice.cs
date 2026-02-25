using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 发货通知单
/// </summary>
[SugarTable("T_SAL_DELIVERYNOTICE")]
public class TSalDeliverynotice : BaseEntity
{
    /// <summary>
    /// 仓管员
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERID")]
    public string Fstockerid { get; set; } = string.Empty;

    /// <summary>
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEORGID")]
    public string Fsettleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 结算方
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEID")]
    public string Fsettleid { get; set; } = string.Empty;

    /// <summary>
    /// 结算币别
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECURRID")]
    public string Fsettlecurrid { get; set; } = string.Empty;

    /// <summary>
    /// 销售主管
    /// </summary>
    [SugarColumn(ColumnName = "FMANGERID")]
    public string Fmangerid { get; set; } = string.Empty;

    /// <summary>
    /// 销售员
    /// </summary>
    [SugarColumn(ColumnName = "FSALESMANID")]
    public string Fsalesmanid { get; set; } = string.Empty;

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
    /// 收货方
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVERID")]
    public string Freceiverid { get; set; } = string.Empty;

    /// <summary>
    /// 收货方地址
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEADDRESS")]
    public string Freceiveaddress { get; set; } = string.Empty;

    /// <summary>
    /// 收货方联系人
    /// </summary>
    [SugarColumn(ColumnName = "FRECCONTACTID")]
    public string Freccontactid { get; set; } = string.Empty;

    /// <summary>
    /// 付款方
    /// </summary>
    [SugarColumn(ColumnName = "FPAYERID")]
    public string Fpayerid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 交货地址
    /// </summary>
    [SugarColumn(ColumnName = "FHEADLOCID")]
    public string Fheadlocid { get; set; } = string.Empty;

    /// <summary>
    /// 交货方式
    /// </summary>
    [SugarColumn(ColumnName = "FHEADDELIVERYWAY")]
    public string Fheaddeliveryway { get; set; } = string.Empty;

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
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FDOCUMENTSTATUS")]
    public string Fdocumentstatus { get; set; } = string.Empty;

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
    /// 发货部门
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDEPTID")]
    public string Fdeliverydeptid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 客户
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMERID")]
    public string Fcustomerid { get; set; } = string.Empty;

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
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 是否含税
    /// </summary>
    [SugarColumn(ColumnName = "FISINCLUDEDTAX")]
    public bool Fisincludedtax { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 是否生成检验单
    /// </summary>
    [SugarColumn(ColumnName = "FISADDTESTLIST")]
    public bool Fisaddtestlist { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    [SugarColumn(ColumnName = "FOASTATUS")]
    public string Foastatus { get; set; } = string.Empty;

    /// <summary>
    /// 审批结果
    /// </summary>
    [SugarColumn(ColumnName = "FOARESULT")]
    public string Foaresult { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
