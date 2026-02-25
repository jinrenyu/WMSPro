using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验规则方案-规则明细
/// </summary>
[SugarTable("T_QMS_RULEDETAILS")]
public class TQmsRuledetails : BaseEntity
{
    /// <summary>
    /// 阶段描述
    /// </summary>
    [SugarColumn(ColumnName = "FSTAGEDESCRIPTION")]
    public string Fstagedescription { get; set; } = string.Empty;

    /// <summary>
    /// 合格批数
    /// </summary>
    [SugarColumn(ColumnName = "FQUALIFIEDNUMBER")]
    public string Fqualifiednumber { get; set; } = string.Empty;

    /// <summary>
    /// 合格新阶段
    /// </summary>
    [SugarColumn(ColumnName = "FQUALIFIEDNEWSTAGE")]
    public string Fqualifiednewstage { get; set; } = string.Empty;

    /// <summary>
    /// 不合格批底数
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUALIFIEDBASENUMBER")]
    public string Funqualifiedbasenumber { get; set; } = string.Empty;

    /// <summary>
    /// 不合格批数
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUALIFIEDNUMBER")]
    public string Funqualifiednumber { get; set; } = string.Empty;

    /// <summary>
    /// 不合格新阶段
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUALIFIEDNEWSTAGE")]
    public string Funqualifiednewstage { get; set; } = string.Empty;

    /// <summary>
    /// 初始阶段
    /// </summary>
    [SugarColumn(ColumnName = "FINITIALSTAGE")]
    public string Finitialstage { get; set; } = string.Empty;

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
