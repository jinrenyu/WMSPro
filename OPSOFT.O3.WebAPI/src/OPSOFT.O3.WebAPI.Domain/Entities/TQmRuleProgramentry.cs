using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验规则方案-规则明细
/// </summary>
[SugarTable("T_QM_RULE_PROGRAMENTRY")]
public class TQmRuleProgramentry : BaseEntity
{
    /// <summary>
    /// 阶段描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIBE")]
    public string Fdescribe { get; set; } = string.Empty;

    /// <summary>
    /// 合格批数
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public int Fquaqty { get; set; }

    /// <summary>
    /// 合格新阶段
    /// </summary>
    [SugarColumn(ColumnName = "FQUASTAGE")]
    public string Fquastage { get; set; } = string.Empty;

    /// <summary>
    /// 不合格批底数
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUABASEQTY")]
    public int Funquabaseqty { get; set; }

    /// <summary>
    /// 不合格批数
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUAQTY")]
    public int Funquaqty { get; set; }

    /// <summary>
    /// 不合格新阶段
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUASTAGE")]
    public string Funquastage { get; set; } = string.Empty;

    /// <summary>
    /// 初始阶段
    /// </summary>
    [SugarColumn(ColumnName = "FISINITIAL")]
    public bool Fisinitial { get; set; }

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
