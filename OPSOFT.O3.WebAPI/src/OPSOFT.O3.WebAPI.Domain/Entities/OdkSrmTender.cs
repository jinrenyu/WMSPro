using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 招标单
/// </summary>
[SugarTable("ODK_SRM_Tender")]
public class OdkSrmTender : BaseEntity
{
    /// <summary>
    /// 项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 招标人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 招标书建立日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 招标公司
    /// </summary>
    [SugarColumn(ColumnName = "FIDCOMPANY")]
    public string Fidcompany { get; set; } = string.Empty;

    /// <summary>
    /// 招标方式(A:公开招标  B:邀请招标
    /// </summary>
    [SugarColumn(ColumnName = "FTDMETHOD")]
    public string Ftdmethod { get; set; } = string.Empty;
}
