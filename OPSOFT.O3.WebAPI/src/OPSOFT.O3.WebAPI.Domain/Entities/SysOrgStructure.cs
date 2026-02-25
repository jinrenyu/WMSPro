using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 组织架构
/// </summary>
[SugarTable("SYS_ORGSTRUCTURE")]
public class SysOrgStructure : BaseEntity
{
    /// <summary>
    /// 所属组织
    /// </summary>
    [SugarColumn(ColumnName = "FPARAID")]
    public string Fparaid { get; set; } = string.Empty;

    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(ColumnName = "FMANAGER")]
    public string Fmanager { get; set; } = string.Empty;

    /// <summary>
    /// 负责人电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE")]
    public string Fphone { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
