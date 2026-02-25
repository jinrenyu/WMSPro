using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 培训记录表体
/// </summary>
[SugarTable("T_WM_TRAINRECORDENTRY")]
public class TWmTrainrecordentry : BaseEntity
{
    /// <summary>
    /// 学员代码
    /// </summary>
    [SugarColumn(ColumnName = "FPERSONID")]
    public string Fpersonid { get; set; } = string.Empty;

    /// <summary>
    /// 考核成绩
    /// </summary>
    [SugarColumn(ColumnName = "FEVASCORE")]
    public decimal Fevascore { get; set; }

    /// <summary>
    /// 是否通过
    /// </summary>
    [SugarColumn(ColumnName = "FPASSYN")]
    public bool Fpassyn { get; set; }

    /// <summary>
    /// 考核评价
    /// </summary>
    [SugarColumn(ColumnName = "FEVACONTENT")]
    public string Fevacontent { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
