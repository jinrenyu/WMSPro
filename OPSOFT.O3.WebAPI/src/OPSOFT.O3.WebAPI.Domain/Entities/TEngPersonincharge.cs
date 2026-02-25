using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修工单负责人
/// </summary>
[SugarTable("T_ENG_PERSONINCHARGE")]
public class TEngPersonincharge : BaseEntity
{
    /// <summary>
    /// 负责人内码
    /// </summary>
    [SugarColumn(ColumnName = "FPERSONID")]
    public string Fpersonid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
