using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 培训记录
/// </summary>
[SugarTable("T_WM_TRAINRECORD")]
public class TWmTrainrecord : BaseEntity
{
    /// <summary>
    /// 培训记录日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 培训机构
    /// </summary>
    [SugarColumn(ColumnName = "FTRAINCHANNEL")]
    public string Ftrainchannel { get; set; } = string.Empty;

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
    /// 培训类型内码
    /// </summary>
    [SugarColumn(ColumnName = "FTRAINTYPEID")]
    public string Ftraintypeid { get; set; } = string.Empty;

    /// <summary>
    /// 课程内码
    /// </summary>
    [SugarColumn(ColumnName = "FTRAINCOURSEID")]
    public string Ftraincourseid { get; set; } = string.Empty;

    /// <summary>
    /// 培训开始日期
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTDATE")]
    public DateTime? Fstartdate { get; set; }

    /// <summary>
    /// 培训结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE")]
    public DateTime? Fenddate { get; set; }

    /// <summary>
    /// 考核方式
    /// </summary>
    [SugarColumn(ColumnName = "FEVAMODE")]
    public string Fevamode { get; set; } = string.Empty;

    /// <summary>
    /// 培训证书内码
    /// </summary>
    [SugarColumn(ColumnName = "FCERTIFICATEID")]
    public string Fcertificateid { get; set; } = string.Empty;

    /// <summary>
    /// 培训记录编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
