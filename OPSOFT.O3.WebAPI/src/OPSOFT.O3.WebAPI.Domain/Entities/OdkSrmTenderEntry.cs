using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 招标单-轮次信息
/// </summary>
[SugarTable("ODK_SRM_TenderEntry")]
public class OdkSrmTenderEntry : BaseEntity
{
    /// <summary>
    /// 轮次
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERNUM")]
    public int Ftendernum { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FBEGINTIME")]
    public DateTime? Fbegintime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERSTATUS")]
    public int Ftenderstatus { get; set; }

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
