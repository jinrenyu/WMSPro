using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 受托加工收料通知单
/// </summary>
[SugarTable("T_STK_OEMRECEIVE")]
public class TStkOemreceive : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 采购员
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASERID")]
    public string Fpurchaserid { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

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
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 采购部门
    /// </summary>
    [SugarColumn(ColumnName = "FPURDEPTID")]
    public string Fpurdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 收料部门
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEDEPTID")]
    public string Freceivedeptid { get; set; } = string.Empty;

    /// <summary>
    /// 收料员
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVERID")]
    public string Freceiverid { get; set; } = string.Empty;

    /// <summary>
    /// 客户
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTID")]
    public string Fcustid { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 主管
    /// </summary>
    [SugarColumn(ColumnName = "FMANAGERID")]
    public string Fmanagerid { get; set; } = string.Empty;

    /// <summary>
    /// 需求组织
    /// </summary>
    [SugarColumn(ColumnName = "FDEMANDORGID")]
    public string Fdemandorgid { get; set; } = string.Empty;

    /// <summary>
    /// 采购组织
    /// </summary>
    [SugarColumn(ColumnName = "FPURORGID")]
    public string Fpurorgid { get; set; } = string.Empty;

    /// <summary>
    /// 送货单号
    /// </summary>
    [SugarColumn(ColumnName = "FSENDBILLNO")]
    public string Fsendbillno { get; set; } = string.Empty;

    /// <summary>
    /// 供货方
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYERID")]
    public string Fsupplyerid { get; set; } = string.Empty;

    /// <summary>
    /// 供货方联系人
    /// </summary>
    [SugarColumn(ColumnName = "FPROVIDERCONTACTID")]
    public string Fprovidercontactid { get; set; } = string.Empty;

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
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEORGID")]
    public string Fsettleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 结算币别
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECURRID")]
    public string Fsettlecurrid { get; set; } = string.Empty;

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
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FFETCHADDRESS")]
    public string Ffetchaddress { get; set; } = string.Empty;

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
