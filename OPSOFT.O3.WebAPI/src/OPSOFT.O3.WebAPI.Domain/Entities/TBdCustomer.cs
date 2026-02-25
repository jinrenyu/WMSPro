using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 客户
/// </summary>
[SugarTable("T_BD_CUSTOMER")]
public class TBdCustomer : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 客户编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 客户名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "FSHORTNAME")]
    public string FShortName { get; set; } = string.Empty;

    /// <summary>
    /// 销售员
    /// </summary>
    [SugarColumn(ColumnName = "FSELLER")]
    public string FSeller { get; set; } = string.Empty;

    /// <summary>
    /// 销售部门
    /// </summary>
    [SugarColumn(ColumnName = "FSALDEPTID")]
    public string FSalDeptId { get; set; } = string.Empty;

    /// <summary>
    /// 结算币别
    /// </summary>
    [SugarColumn(ColumnName = "FTRADINGCURRID")]
    public string FTradingCurrId { get; set; } = string.Empty;

    /// <summary>
    /// 国家
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNTRY")]
    public string FCountry { get; set; } = string.Empty;

    /// <summary>
    /// 地区
    /// </summary>
    [SugarColumn(ColumnName = "FPROVINCIAL")]
    public string FProvincial { get; set; } = string.Empty;

    /// <summary>
    /// 通讯地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string FAddress { get; set; } = string.Empty;

    /// <summary>
    /// 邮政编码
    /// </summary>
    [SugarColumn(ColumnName = "FZIP")]
    public string FZip { get; set; } = string.Empty;

    /// <summary>
    /// 公司网址
    /// </summary>
    [SugarColumn(ColumnName = "FWEBSITE")]
    public string FWebSite { get; set; } = string.Empty;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FTEL")]
    public string FTel { get; set; } = string.Empty;

    /// <summary>
    /// 传真
    /// </summary>
    [SugarColumn(ColumnName = "FFAX")]
    public string FFax { get; set; } = string.Empty;

    /// <summary>
    /// 纳税登记号
    /// </summary>
    [SugarColumn(ColumnName = "FTAXREGISTERCODE")]
    public string FTaxRegisterCode { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 英文简称
    /// </summary>
    [SugarColumn(ColumnName = "FNAMEEN")]
    public string FNameEn { get; set; } = string.Empty;

    /// <summary>
    /// 城市
    /// </summary>
    [SugarColumn(ColumnName = "FCITY")]
    public string FCity { get; set; } = string.Empty;

    /// <summary>
    /// 英文地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESSEN")]
    public string FAddressEn { get; set; } = string.Empty;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE")]
    public string FPhone { get; set; } = string.Empty;

    /// <summary>
    /// 银行
    /// </summary>
    [SugarColumn(ColumnName = "FBANK")]
    public string FBank { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL")]
    public string FEmail { get; set; } = string.Empty;

    /// <summary>
    /// 账户
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNT")]
    public string FAccount { get; set; } = string.Empty;

    /// <summary>
    /// 法人
    /// </summary>
    [SugarColumn(ColumnName = "FLEGALPERSON")]
    public string FLegalPerson { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACT")]
    public string FContact { get; set; } = string.Empty;

    /// <summary>
    /// 省份
    /// </summary>
    [SugarColumn(ColumnName = "FPROVICE")]
    public string FProvice { get; set; } = string.Empty;

    /// <summary>
    /// 业务员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string FEmpId { get; set; } = string.Empty;
}
