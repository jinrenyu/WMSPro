using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 询价单表体3-供应商信息
/// </summary>
[SugarTable("ODK_SRM_InquiryEntry2")]
public class OdkSrmInquiryEntry2 : BaseEntity
{
    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 状态(0:初始 1:参与 2:拒绝
    /// </summary>
    [SugarColumn(ColumnName = "FSUPSTATUS")]
    public int Fsupstatus { get; set; }

    /// <summary>
    /// 电子邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL", IsNullable = true)]
    public string FEMAIL { get; set; } = string.Empty;

    /// <summary>
    /// 联系方式
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACTPHONE", IsNullable = true)]
    public string FCONTACTPHONE { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACTNAME", IsNullable = true)]
    public string FCONTACTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 拒绝理由
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMENT", IsNullable = true)]
    public string FCOMMENT { get; set; } = string.Empty;

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID", IsNullable = true)]
    public string FBODYID { get; set; } = string.Empty;
}
