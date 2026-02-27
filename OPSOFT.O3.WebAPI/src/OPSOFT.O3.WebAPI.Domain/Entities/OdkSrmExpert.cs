using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 专家库
/// </summary>
[SugarTable("ODK_SRM_Expert")]
public class OdkSrmExpert : BaseEntity
{
    /// <summary>
    /// 专家编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 专家类型（1：内部专家2:外部专家）
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 性别(1：男 2：女
    /// </summary>
    [SugarColumn(ColumnName = "FGENDER")]
    public string Fgender { get; set; } = string.Empty;

    /// <summary>
    /// 精通行业
    /// </summary>
    [SugarColumn(ColumnName = "FTRADE", IsNullable = true)]
    public string FTRADE { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS", IsNullable = true)]
    public string FADDRESS { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL", IsNullable = true)]
    public string FEMAIL { get; set; } = string.Empty;

    /// <summary>
    /// 证件类型
    /// </summary>
    [SugarColumn(ColumnName = "FIDTYPE", IsNullable = true)]
    public string FIDTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 专业
    /// </summary>
    [SugarColumn(ColumnName = "FMAJOR", IsNullable = true)]
    public string FMAJOR { get; set; } = string.Empty;

    /// <summary>
    /// 最高学历
    /// </summary>
    [SugarColumn(ColumnName = "FDEGREE", IsNullable = true)]
    public string FDEGREE { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人内码
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL", IsNullable = true)]
    public int? FCHECKLEVEL { get; set; }

    /// <summary>
    /// 出生年月日
    /// </summary>
    [SugarColumn(ColumnName = "FBIRTHDATE", IsNullable = true)]
    public DateTime? FBIRTHDATE { get; set; }

    /// <summary>
    /// 毕业院校
    /// </summary>
    [SugarColumn(ColumnName = "FSCHOOL", IsNullable = true)]
    public string FSCHOOL { get; set; } = string.Empty;

    /// <summary>
    /// 银行内码
    /// </summary>
    [SugarColumn(ColumnName = "FBANKID", IsNullable = true)]
    public string FBANKID { get; set; } = string.Empty;

    /// <summary>
    /// 登录名
    /// </summary>
    [SugarColumn(ColumnName = "FLOGINID", IsNullable = true)]
    public string FLOGINID { get; set; } = string.Empty;

    /// <summary>
    /// 移动电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE", IsNullable = true)]
    public string FPHONE { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }

    /// <summary>
    /// 是否在职
    /// </summary>
    [SugarColumn(ColumnName = "FISONJOB", IsNullable = true)]
    public bool? FISONJOB { get; set; }

    /// <summary>
    /// 工作单位
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPANY", IsNullable = true)]
    public string FCOMPANY { get; set; } = string.Empty;

    /// <summary>
    /// 职业资格等级
    /// </summary>
    [SugarColumn(ColumnName = "FPROGRADE", IsNullable = true)]
    public string FPROGRADE { get; set; } = string.Empty;

    /// <summary>
    /// 银行账户
    /// </summary>
    [SugarColumn(ColumnName = "FBANKACCOUNT", IsNullable = true)]
    public string FBANKACCOUNT { get; set; } = string.Empty;

    /// <summary>
    /// 从业年限
    /// </summary>
    [SugarColumn(ColumnName = "FEMPYEAR", IsNullable = true)]
    public int? FEMPYEAR { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 证件号码
    /// </summary>
    [SugarColumn(ColumnName = "FIDNO", IsNullable = true)]
    public string FIDNO { get; set; } = string.Empty;

    /// <summary>
    /// 职位
    /// </summary>
    [SugarColumn(ColumnName = "FPOSITION", IsNullable = true)]
    public string FPOSITION { get; set; } = string.Empty;
}
