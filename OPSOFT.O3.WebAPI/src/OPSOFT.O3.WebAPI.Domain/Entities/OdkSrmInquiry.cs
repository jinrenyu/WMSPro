using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 询价单表头
/// </summary>
[SugarTable("ODK_SRM_Inquiry")]
public class OdkSrmInquiry : BaseEntity
{
    /// <summary>
    /// 询价单标题
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 职员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 询价单日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 询价方式(A:公开 B:邀请
    /// </summary>
    [SugarColumn(ColumnName = "FMETHOD")]
    public string Fmethod { get; set; } = string.Empty;
}
