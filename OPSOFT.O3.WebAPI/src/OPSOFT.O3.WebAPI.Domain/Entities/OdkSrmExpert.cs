using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 专家库
/// </summary>
[SugarTable("ODK_SRM_Expert")]
public class OdkSrmExpert : BaseEntity
{
    /// <summary>
    /// 专家编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 专家类型（1：内部专家2:外部专家）
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 性别(1：男 2：女
    /// </summary>
    [SugarColumn(ColumnName = "FGENDER")]
    public string Fgender { get; set; } = string.Empty;
}
