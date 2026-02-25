using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 招标单-供应商信息
/// </summary>
[SugarTable("ODK_SRM_TenderEntry2")]
public class OdkSrmTenderEntry2 : BaseEntity
{
    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FSUPSTATUS")]
    public int Fsupstatus { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACTNAME")]
    public string Fcontactname { get; set; } = string.Empty;

    /// <summary>
    /// 联系方式
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACTPHONE")]
    public string Fcontactphone { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL")]
    public string Femail { get; set; } = string.Empty;

    /// <summary>
    /// 拒绝理由
    /// </summary>
    [SugarColumn(ColumnName = "FREFUREASON")]
    public string Frefureason { get; set; } = string.Empty;

    /// <summary>
    /// 是否缴纳
    /// </summary>
    [SugarColumn(ColumnName = "FISPAY")]
    public bool Fispay { get; set; }

    /// <summary>
    /// 缴纳通过
    /// </summary>
    [SugarColumn(ColumnName = "FPAYPASS")]
    public bool Fpaypass { get; set; }

    /// <summary>
    /// 是否申请
    /// </summary>
    [SugarColumn(ColumnName = "FISAPPLY")]
    public bool Fisapply { get; set; }

    /// <summary>
    /// 预审申请说明
    /// </summary>
    [SugarColumn(ColumnName = "FAPPLYDOC")]
    public string Fapplydoc { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FAPPLYDATE")]
    public DateTime? Fapplydate { get; set; }

    /// <summary>
    /// 申请人
    /// </summary>
    [SugarColumn(ColumnName = "FAPPNAME")]
    public string Fappname { get; set; } = string.Empty;

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

    /// <summary>
    /// 评审通过
    /// </summary>
    [SugarColumn(ColumnName = "FISPASS")]
    public bool Fispass { get; set; }

    /// <summary>
    /// 评审意见
    /// </summary>
    [SugarColumn(ColumnName = "FASSESSRESULT")]
    public string Fassessresult { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
