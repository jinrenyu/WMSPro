using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商档案
/// </summary>
[SugarTable("ODK_SRM_Supplier")]
public class OdkSrmSupplier : BaseEntity
{
    /// <summary>
    /// 账户
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTGROUP")]
    public string Faccountgroup { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "FSHORTNAME")]
    public string Fshortname { get; set; } = string.Empty;

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public string Ftaxrate { get; set; } = string.Empty;

    /// <summary>
    /// 企业性质
    /// </summary>
    [SugarColumn(ColumnName = "FENTERPRISENATURE")]
    public string Fenterprisenature { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAMEEN")]
    public string Fnameen { get; set; } = string.Empty;

    /// <summary>
    /// 供应商类型
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLIERTYPE")]
    public string Fsuppliertype { get; set; } = string.Empty;

    /// <summary>
    /// MRO供应商
    /// </summary>
    [SugarColumn(ColumnName = "FISMRO")]
    public string Fismro { get; set; } = string.Empty;

    /// <summary>
    /// 客户编码
    /// </summary>
    [SugarColumn(ColumnName = "FCNUMBER")]
    public string Fcnumber { get; set; } = string.Empty;

    /// <summary>
    /// 税号
    /// </summary>
    [SugarColumn(ColumnName = "FTAXID")]
    public string Ftaxid { get; set; } = string.Empty;

    /// <summary>
    /// 合作内容
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMWORK")]
    public string Fteamwork { get; set; } = string.Empty;

    /// <summary>
    /// 公司类型
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPANYTYPE")]
    public string Fcompanytype { get; set; } = string.Empty;

    /// <summary>
    /// 审核采购组织
    /// </summary>
    [SugarColumn(ColumnName = "FAUDITPROCUREMENTORG")]
    public string Fauditprocurementorg { get; set; } = string.Empty;

    /// <summary>
    /// 同步ERP
    /// </summary>
    [SugarColumn(ColumnName = "FISERP")]
    public bool Fiserp { get; set; }

    /// <summary>
    /// 国家编码
    /// </summary>
    [SugarColumn(ColumnName = "FNATIONAL")]
    public string Fnational { get; set; } = string.Empty;

    /// <summary>
    /// 国家
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNTRY")]
    public string Fcountry { get; set; } = string.Empty;

    /// <summary>
    /// 城市
    /// </summary>
    [SugarColumn(ColumnName = "FCITY")]
    public string Fcity { get; set; } = string.Empty;

    /// <summary>
    /// 法人代表
    /// </summary>
    [SugarColumn(ColumnName = "FLEGAL")]
    public string Flegal { get; set; } = string.Empty;

    /// <summary>
    /// 企业营业执照
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSLICENSENO")]
    public string Fbusinesslicenseno { get; set; } = string.Empty;

    /// <summary>
    /// 公司地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 公司邮编
    /// </summary>
    [SugarColumn(ColumnName = "FZIPCODE")]
    public string Fzipcode { get; set; } = string.Empty;

    /// <summary>
    /// 公司电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE")]
    public string Fphone { get; set; } = string.Empty;

    /// <summary>
    /// 公司传真
    /// </summary>
    [SugarColumn(ColumnName = "FFAX")]
    public string Ffax { get; set; } = string.Empty;

    /// <summary>
    /// 销售员
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACT")]
    public string Fcontact { get; set; } = string.Empty;

    /// <summary>
    /// 销售员手机
    /// </summary>
    [SugarColumn(ColumnName = "FCOTEL")]
    public string Fcotel { get; set; } = string.Empty;

    /// <summary>
    /// 销售员邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL")]
    public string Femail { get; set; } = string.Empty;

    /// <summary>
    /// 公司简介
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPANYPROFILE")]
    public string Fcompanyprofile { get; set; } = string.Empty;

    /// <summary>
    /// 经营范围
    /// </summary>
    [SugarColumn(ColumnName = "FSCOPEOPERATION")]
    public string Fscopeoperation { get; set; } = string.Empty;

    /// <summary>
    /// 公司网址
    /// </summary>
    [SugarColumn(ColumnName = "FWEBSITE")]
    public string Fwebsite { get; set; } = string.Empty;

    /// <summary>
    /// 注册资本
    /// </summary>
    [SugarColumn(ColumnName = "FREGCAPTIAL")]
    public decimal Fregcaptial { get; set; }

    /// <summary>
    /// 实收资本
    /// </summary>
    [SugarColumn(ColumnName = "FCOLLECTIONCAPITAL")]
    public decimal Fcollectioncapital { get; set; }

    /// <summary>
    /// 成立日期
    /// </summary>
    [SugarColumn(ColumnName = "FESTABLISHMENTDATE")]
    public DateTime? Festablishmentdate { get; set; }

    /// <summary>
    /// 营业期限从
    /// </summary>
    [SugarColumn(ColumnName = "FLIMITSTART")]
    public DateTime? Flimitstart { get; set; }

    /// <summary>
    /// 营业期限至
    /// </summary>
    [SugarColumn(ColumnName = "FLIMITEND")]
    public DateTime? Flimitend { get; set; }

    /// <summary>
    /// 曾用名1
    /// </summary>
    [SugarColumn(ColumnName = "FBNAME1")]
    public string Fbname1 { get; set; } = string.Empty;

    /// <summary>
    /// 曾用名1时间从
    /// </summary>
    [SugarColumn(ColumnName = "FBNAMETIMES1")]
    public DateTime? Fbnametimes1 { get; set; }

    /// <summary>
    /// 曾用名1时间至
    /// </summary>
    [SugarColumn(ColumnName = "FBNAMETIMEE1")]
    public DateTime? Fbnametimee1 { get; set; }

    /// <summary>
    /// 曾用名2
    /// </summary>
    [SugarColumn(ColumnName = "FBNAME2")]
    public string Fbname2 { get; set; } = string.Empty;

    /// <summary>
    /// 曾用名2时间从
    /// </summary>
    [SugarColumn(ColumnName = "FBNAMETIMES2")]
    public DateTime? Fbnametimes2 { get; set; }

    /// <summary>
    /// 曾用名2时间至
    /// </summary>
    [SugarColumn(ColumnName = "FBNAMETIMEE2")]
    public DateTime? Fbnametimee2 { get; set; }

    /// <summary>
    /// 年平均营业额/万
    /// </summary>
    [SugarColumn(ColumnName = "FANNUALTURNOVER")]
    public decimal Fannualturnover { get; set; }

    /// <summary>
    /// 职工总人数
    /// </summary>
    [SugarColumn(ColumnName = "FWORKERTOTAL")]
    public int Fworkertotal { get; set; }

    /// <summary>
    /// 年生产能力
    /// </summary>
    [SugarColumn(ColumnName = "FTHROUGHPUT")]
    public decimal Fthroughput { get; set; }

    /// <summary>
    /// 主要生产设备说明
    /// </summary>
    [SugarColumn(ColumnName = "FRMQTY")]
    public string Frmqty { get; set; } = string.Empty;

    /// <summary>
    /// 主要产品
    /// </summary>
    [SugarColumn(ColumnName = "FIMPRODUCT")]
    public string Fimproduct { get; set; } = string.Empty;

    /// <summary>
    /// 主要原材料供应商
    /// </summary>
    [SugarColumn(ColumnName = "FIMMATERIALSUPPLIER")]
    public string Fimmaterialsupplier { get; set; } = string.Empty;

    /// <summary>
    /// 主要客户说明
    /// </summary>
    [SugarColumn(ColumnName = "FCSALESRATIO")]
    public string Fcsalesratio { get; set; } = string.Empty;

    /// <summary>
    /// 专业技术人员
    /// </summary>
    [SugarColumn(ColumnName = "FPTECHNICIAN")]
    public string Fptechnician { get; set; } = string.Empty;

    /// <summary>
    /// 检验人员
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKER")]
    public string Fchecker { get; set; } = string.Empty;

    /// <summary>
    /// 检验说明
    /// </summary>
    [SugarColumn(ColumnName = "FIMCHECKRMQTY")]
    public string Fimcheckrmqty { get; set; } = string.Empty;

    /// <summary>
    /// 淘汰状态（没用到）
    /// </summary>
    [SugarColumn(ColumnName = "FELIMINATESTATUS")]
    public bool Feliminatestatus { get; set; }

    /// <summary>
    /// 合格标志(没用到
    /// </summary>
    [SugarColumn(ColumnName = "FNYQUALIFIED")]
    public string Fnyqualified { get; set; } = string.Empty;
}
