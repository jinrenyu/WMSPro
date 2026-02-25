using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工艺技能纬度
/// </summary>
[SugarTable("T_WM_ROUTESKILLLAT")]
public class TWmRouteskilllat : BaseEntity
{
    /// <summary>
    /// 设备代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 设备名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 技能等级（小）
    /// </summary>
    [SugarColumn(ColumnName = "FMINSKILLGRADE")]
    public int Fminskillgrade { get; set; }

    /// <summary>
    /// 技能等级（大）
    /// </summary>
    [SugarColumn(ColumnName = "FMAXSKILLGRADE")]
    public int Fmaxskillgrade { get; set; }

    /// <summary>
    /// 技能描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 累计产量（下限）
    /// </summary>
    [SugarColumn(ColumnName = "FMINALLOUTPUT")]
    public decimal Fminalloutput { get; set; }

    /// <summary>
    /// 累计工时（下限）
    /// </summary>
    [SugarColumn(ColumnName = "FMINALLHOUR")]
    public decimal Fminallhour { get; set; }

    /// <summary>
    /// 工艺ID
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEID")]
    public string Frouteid { get; set; } = string.Empty;
}
