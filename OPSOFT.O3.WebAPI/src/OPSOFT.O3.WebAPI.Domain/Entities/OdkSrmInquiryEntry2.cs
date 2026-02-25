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
}
