using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 岗位要求矩阵
/// </summary>
[SugarTable("T_WM_JOBREQUIREMAT")]
public class TWmJobrequiremat : BaseEntity
{
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
    /// 岗位内码
    /// </summary>
    [SugarColumn(ColumnName = "FPOSITIONID")]
    public string Fpositionid { get; set; } = string.Empty;

    /// <summary>
    /// 工艺技能下限
    /// </summary>
    [SugarColumn(ColumnName = "FMINROUTEGRADE")]
    public int Fminroutegrade { get; set; }

    /// <summary>
    /// 设备技能下限
    /// </summary>
    [SugarColumn(ColumnName = "FMINEQUIPMENTGRADE")]
    public int Fminequipmentgrade { get; set; }

    /// <summary>
    /// 熟练度产量下限
    /// </summary>
    [SugarColumn(ColumnName = "FMINOUTPUT")]
    public int Fminoutput { get; set; }

    /// <summary>
    /// 熟练度时间下限
    /// </summary>
    [SugarColumn(ColumnName = "FMINHOUR")]
    public decimal Fminhour { get; set; }

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
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;
}
