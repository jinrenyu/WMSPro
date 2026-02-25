using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 培训课程
/// </summary>
[SugarTable("T_WM_TRAINCOURCE")]
public class TWmTraincource : BaseEntity
{
    /// <summary>
    /// 课程代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 课程名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 课时
    /// </summary>
    [SugarColumn(ColumnName = "FHOUR")]
    public decimal Fhour { get; set; }

    /// <summary>
    /// 所属组织内码
    /// </summary>
    [SugarColumn(ColumnName = "FORGSTRUCTUREID")]
    public string Forgstructureid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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

    /// <summary>
    /// 课程目标
    /// </summary>
    [SugarColumn(ColumnName = "FTARGET")]
    public string Ftarget { get; set; } = string.Empty;

    /// <summary>
    /// 培养对象
    /// </summary>
    [SugarColumn(ColumnName = "FTRAINTARGET")]
    public string Ftraintarget { get; set; } = string.Empty;

    /// <summary>
    /// 培训机构
    /// </summary>
    [SugarColumn(ColumnName = "FTRAINCHANNEID")]
    public string Ftrainchanneid { get; set; } = string.Empty;

    /// <summary>
    /// 课程内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 类别编号
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;
}
