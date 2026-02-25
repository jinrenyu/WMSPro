using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 员工
/// </summary>
[SugarTable("T_HR_EMPINFO")]
public class THrEmpinfo : BaseEntity
{
    /// <summary>
    /// 员工代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 员工姓名
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 员工描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 学历
    /// </summary>
    [SugarColumn(ColumnName = "FEDUCATION")]
    public string Feducation { get; set; } = string.Empty;

    /// <summary>
    /// 性别
    /// </summary>
    [SugarColumn(ColumnName = "FSEX")]
    public int Fsex { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(ColumnName = "FBIRTHDATE")]
    public DateTime? Fbirthdate { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYDATE")]
    public DateTime? Fentrydate { get; set; }

    /// <summary>
    /// 离职日期
    /// </summary>
    [SugarColumn(ColumnName = "FDEPARTUREDATE")]
    public DateTime? Fdeparturedate { get; set; }

    /// <summary>
    /// 离职
    /// </summary>
    [SugarColumn(ColumnName = "FISDEPARTURE")]
    public bool Fisdeparture { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FSALDEPTID")]
    public string Fsaldeptid { get; set; } = string.Empty;

    /// <summary>
    /// 工资系数
    /// </summary>
    [SugarColumn(ColumnName = "FCOEFFICIENT")]
    public decimal Fcoefficient { get; set; }

    /// <summary>
    /// QQ
    /// </summary>
    [SugarColumn(ColumnName = "FQQ")]
    public string Fqq { get; set; } = string.Empty;

    /// <summary>
    /// 微信
    /// </summary>
    [SugarColumn(ColumnName = "FWECHAT")]
    public string Fwechat { get; set; } = string.Empty;

    /// <summary>
    /// 紧急联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FEMERGENCYTEL")]
    public string Femergencytel { get; set; } = string.Empty;

    /// <summary>
    /// 紧急联系人
    /// </summary>
    [SugarColumn(ColumnName = "EMERGENCY")]
    public string Emergency { get; set; } = string.Empty;

    /// <summary>
    /// 联系地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 移动电话
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILE")]
    public string Fmobile { get; set; } = string.Empty;

    /// <summary>
    /// 固定电话
    /// </summary>
    [SugarColumn(ColumnName = "FTEL")]
    public string Ftel { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL")]
    public string Femail { get; set; } = string.Empty;

    /// <summary>
    /// 使用组织
    /// </summary>
    [SugarColumn(ColumnName = "FUSEORGID")]
    public string Fuseorgid { get; set; } = string.Empty;

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
    /// 调岗日期
    /// </summary>
    [SugarColumn(ColumnName = "FTRANSFERDATE")]
    public DateTime? Ftransferdate { get; set; }

    /// <summary>
    /// 职务等级
    /// </summary>
    [SugarColumn(ColumnName = "FLEVEL")]
    public decimal Flevel { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    [SugarColumn(ColumnName = "FDUTYID")]
    public string Fdutyid { get; set; } = string.Empty;

    /// <summary>
    /// 图片
    /// </summary>
    [SugarColumn(ColumnName = "FPICTURE")]
    public byte[]? Fpicture { get; set; }

    /// <summary>
    /// 成本中心
    /// </summary>
    [SugarColumn(ColumnName = "FCOSTCENTERID")]
    public string Fcostcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 员工任岗内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTAFFID")]
    public string Fstaffid { get; set; } = string.Empty;

    /// <summary>
    /// 钉钉中的UserID
    /// </summary>
    [SugarColumn(ColumnName = "FDDUSERID")]
    public string Fdduserid { get; set; } = string.Empty;
}
