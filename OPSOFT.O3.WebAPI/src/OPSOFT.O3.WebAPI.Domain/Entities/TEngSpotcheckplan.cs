using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 点检方案
/// </summary>
[SugarTable("T_ENG_SPOTCHECKPLAN")]
public class TEngSpotcheckplan : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEID")]
    public string Fmachineid { get; set; } = string.Empty;

    /// <summary>
    /// 是否委外
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIROUT")]
    public bool Frepairout { get; set; }

    /// <summary>
    /// 不良是否创建维修单
    /// </summary>
    [SugarColumn(ColumnName = "FISCREATEREPAIR")]
    public bool Fiscreaterepair { get; set; }

    /// <summary>
    /// 是否显示上下限值
    /// </summary>
    [SugarColumn(ColumnName = "FISSHOWLIMIT")]
    public bool Fisshowlimit { get; set; }

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 任务名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
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

    /// <summary>
    /// 任务类别
    /// </summary>
    [SugarColumn(ColumnName = "FTASKTYPEID")]
    public string Ftasktypeid { get; set; } = string.Empty;
}
