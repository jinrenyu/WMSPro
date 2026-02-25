using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备维护任务
/// </summary>
[SugarTable("T_ENG_MTTASKISSUED")]
public class TEngMttaskissued : BaseEntity
{
    /// <summary>
    /// 任务类型
    /// </summary>
    [SugarColumn(ColumnName = "FTASKTYPEID")]
    public string Ftasktypeid { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 维护类别
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTTYPE")]
    public int Fmainttype { get; set; }

    /// <summary>
    /// 任务描述
    /// </summary>
    [SugarColumn(ColumnName = "FTASKDESCRIBE")]
    public string Ftaskdescribe { get; set; } = string.Empty;

    /// <summary>
    /// 费用类别
    /// </summary>
    [SugarColumn(ColumnName = "FPAYTYPEID")]
    public string Fpaytypeid { get; set; } = string.Empty;

    /// <summary>
    /// 提前提醒天数
    /// </summary>
    [SugarColumn(ColumnName = "FREMINDDAYS")]
    public int Freminddays { get; set; }

    /// <summary>
    /// 编制日期
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPILEDATE")]
    public DateTime? Fcompiledate { get; set; }

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
