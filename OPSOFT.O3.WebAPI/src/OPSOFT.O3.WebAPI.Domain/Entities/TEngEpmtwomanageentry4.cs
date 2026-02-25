using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修工单管理-实施结果确认
/// </summary>
[SugarTable("T_ENG_EPMTWOMANAGEENTRY4")]
public class TEngEpmtwomanageentry4 : BaseEntity
{
    /// <summary>
    /// 项目
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENTSANDREQUIREMENTS")]
    public string Fcontentsandrequirements { get; set; } = string.Empty;

    /// <summary>
    /// 状况
    /// </summary>
    [SugarColumn(ColumnName = "FCONDITION")]
    public string Fcondition { get; set; } = string.Empty;

    /// <summary>
    /// 评分
    /// </summary>
    [SugarColumn(ColumnName = "FSCORE")]
    public decimal Fscore { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [SugarColumn(ColumnName = "FEXPLAIN")]
    public string Fexplain { get; set; } = string.Empty;

    /// <summary>
    /// 是否完成
    /// </summary>
    [SugarColumn(ColumnName = "FISFINISHED")]
    public bool Fisfinished { get; set; }

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
