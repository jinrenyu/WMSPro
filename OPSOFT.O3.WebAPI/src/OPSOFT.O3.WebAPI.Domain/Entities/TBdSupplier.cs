using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商
/// </summary>
[SugarTable("T_BD_SUPPLIER")]
public class TBdSupplier : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; }

    /// <summary>
    /// 供应商代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 供应商名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACT")]
    public string FContact { get; set; } = string.Empty;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE")]
    public string FPhone { get; set; } = string.Empty;

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string FAddress { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

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
    /// 工商登记号
    /// </summary>
    [SugarColumn(ColumnName = "FREGISTERCODE", IsNullable = true)]
    public string FREGISTERCODE { get; set; } = string.Empty;

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE", IsNullable = true)]
    public decimal? FTAXRATE { get; set; }

    /// <summary>
    /// 行业
    /// </summary>
    [SugarColumn(ColumnName = "FTRADE", IsNullable = true)]
    public string FTRADE { get; set; } = string.Empty;

    /// <summary>
    /// 省份
    /// </summary>
    [SugarColumn(ColumnName = "FPROVINCE", IsNullable = true)]
    public string FPROVINCE { get; set; } = string.Empty;

    /// <summary>
    /// 国家
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNTRY", IsNullable = true)]
    public string FCOUNTRY { get; set; } = string.Empty;

    /// <summary>
    /// 邮政编码
    /// </summary>
    [SugarColumn(ColumnName = "FZIP", IsNullable = true)]
    public string FZIP { get; set; } = string.Empty;

    /// <summary>
    /// 城市
    /// </summary>
    [SugarColumn(ColumnName = "FCITY", IsNullable = true)]
    public string FCITY { get; set; } = string.Empty;

    /// <summary>
    /// 生产经营许可证
    /// </summary>
    [SugarColumn(ColumnName = "FTENDPERMIT", IsNullable = true)]
    public string FTENDPERMIT { get; set; } = string.Empty;

    /// <summary>
    /// 账户
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNT", IsNullable = true)]
    public string FACCOUNT { get; set; } = string.Empty;

    /// <summary>
    /// 业务员名称
    /// </summary>
    [SugarColumn(ColumnName = "FEMPNAME", IsNullable = true)]
    public string FEMPNAME { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮件
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL", IsNullable = true)]
    public string FEMAIL { get; set; } = string.Empty;

    /// <summary>
    /// 传真
    /// </summary>
    [SugarColumn(ColumnName = "FFAX", IsNullable = true)]
    public string FFAX { get; set; } = string.Empty;

    /// <summary>
    /// 银行
    /// </summary>
    [SugarColumn(ColumnName = "FBANK", IsNullable = true)]
    public string FBANK { get; set; } = string.Empty;

    /// <summary>
    /// 地区
    /// </summary>
    [SugarColumn(ColumnName = "FPROVINCIAL", IsNullable = true)]
    public string FPROVINCIAL { get; set; } = string.Empty;

    /// <summary>
    /// 业务员编号
    /// </summary>
    [SugarColumn(ColumnName = "FEMPNUMBER", IsNullable = true)]
    public string FEMPNUMBER { get; set; } = string.Empty;

    /// <summary>
    /// 公司网址
    /// </summary>
    [SugarColumn(ColumnName = "FWEBSITE", IsNullable = true)]
    public string FWEBSITE { get; set; } = string.Empty;

    /// <summary>
    /// 供应商简称
    /// </summary>
    [SugarColumn(ColumnName = "FSHORTNAME", IsNullable = true)]
    public string FSHORTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 注册地址
    /// </summary>
    [SugarColumn(ColumnName = "FREGISTERADDRESS", IsNullable = true)]
    public string FREGISTERADDRESS { get; set; } = string.Empty;

    /// <summary>
    /// 业务员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID", IsNullable = true)]
    public string FEMPID { get; set; } = string.Empty;
}
