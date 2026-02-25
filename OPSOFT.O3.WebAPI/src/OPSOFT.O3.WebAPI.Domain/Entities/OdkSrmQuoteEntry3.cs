using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 报价单表体2_附件信息
/// </summary>
[SugarTable("ODK_SRM_QuoteEntry3")]
public class OdkSrmQuoteEntry3 : BaseEntity
{
    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

    /// <summary>
    /// 附件类型
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDTYPE")]
    public string Fappendtype { get; set; } = string.Empty;

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
