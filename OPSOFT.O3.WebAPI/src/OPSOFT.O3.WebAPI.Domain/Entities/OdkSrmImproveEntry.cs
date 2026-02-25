using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 绩效改善单表体-采购方
/// </summary>
[SugarTable("ODK_SRM_ImproveEntry")]
public class OdkSrmImproveEntry : BaseEntity
{
    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

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
