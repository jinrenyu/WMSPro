using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料当前检验方案-表体
/// </summary>
[SugarTable("T_SFC_MAINSPlANINGENTRY")]
public class TSfcMAINSPlANINGENTRY : BaseEntity
{
    /// <summary>
    /// 严格度
    /// </summary>
    [SugarColumn(ColumnName = "FSTRINGENCY")]
    public int Fstringency { get; set; }

    /// <summary>
    /// 检验单审核时间
    /// </summary>
    [SugarColumn(ColumnName = "FINSVETIME")]
    public DateTime? Finsvetime { get; set; }

    /// <summary>
    /// 检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FINSRESULT")]
    public int Finsresult { get; set; }

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
