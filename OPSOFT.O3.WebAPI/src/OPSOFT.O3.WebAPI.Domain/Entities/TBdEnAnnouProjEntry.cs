using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 公告方案表体
/// </summary>
[SugarTable("T_BD_ENANNOUPROJENTRY")]
public class TBdEnannouprojentry : BaseEntity
{
    /// <summary>
    /// 人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPIDS")]
    public string Fempids { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
