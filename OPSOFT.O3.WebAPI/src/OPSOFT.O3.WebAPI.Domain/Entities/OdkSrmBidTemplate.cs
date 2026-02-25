using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 评标模板
/// </summary>
[SugarTable("ODK_SRM_BidTemplate")]
public class OdkSrmBidTemplate : BaseEntity
{
    /// <summary>
    /// 标评模板编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 标评模板名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 招标类型(A:货物招标  B:服务招标  C:工程招标
    /// </summary>
    [SugarColumn(ColumnName = "FBIDTYPE")]
    public string Fbidtype { get; set; } = string.Empty;
}
