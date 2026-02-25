using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购订单
/// </summary>
[SugarTable("T_PUR_POORDER")]
public class TPurPoOrder : BaseEntity
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
    /// 采购员内码
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASERID")]
    public string Fpurchaserid { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 采购日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 采购部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASEDEPTID")]
    public string Fpurchasedeptid { get; set; } = string.Empty;

    /// <summary>
    /// 指定供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FASSIGNSUPPLIERID")]
    public string Fassignsupplierid { get; set; } = string.Empty;

    /// <summary>
    /// 结算货币内码
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECURRID")]
    public string Fsettlecurrid { get; set; } = string.Empty;

    /// <summary>
    /// ERP已经删除
    /// </summary>
    [SugarColumn(ColumnName = "FERPDEL")]
    public bool Ferpdel { get; set; }

    /// <summary>
    /// 供货方内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROVIDERID")]
    public string Fproviderid { get; set; } = string.Empty;

    /// <summary>
    /// 供货方联系人
    /// </summary>
    [SugarColumn(ColumnName = "FPROVIDERCONTACT")]
    public string Fprovidercontact { get; set; } = string.Empty;

    /// <summary>
    /// 职务
    /// </summary>
    [SugarColumn(ColumnName = "FPROVIDERJOB")]
    public string Fproviderjob { get; set; } = string.Empty;

    /// <summary>
    /// 供货方地址
    /// </summary>
    [SugarColumn(ColumnName = "FPROVIDERADDRESS")]
    public string Fprovideraddress { get; set; } = string.Empty;

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
    /// 付款条件
    /// </summary>
    [SugarColumn(ColumnName = "FPAYCONDITIONID")]
    public string Fpayconditionid { get; set; } = string.Empty;

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
    /// 变更原因
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGEREASON")]
    public string Fchangereason { get; set; } = string.Empty;

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
    /// 价外税
    /// </summary>
    [SugarColumn(ColumnName = "FISPRICEEXCLUDETAX")]
    public bool Fispriceexcludetax { get; set; }

    /// <summary>
    /// 含税
    /// </summary>
    [SugarColumn(ColumnName = "FISINCLUDEDTAX")]
    public bool Fisincludedtax { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIBE")]
    public string Fdescribe { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
