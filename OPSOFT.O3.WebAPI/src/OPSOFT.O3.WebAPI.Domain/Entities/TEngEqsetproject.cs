using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备立项
/// </summary>
[SugarTable("T_ENG_EQSETPROJECT")]
public class TEngEqsetproject : BaseEntity
{
    /// <summary>
    /// 项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPARTID")]
    public string Fdepartid { get; set; } = string.Empty;

    /// <summary>
    /// 项目负责人-内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRINCIPAL")]
    public string Fprincipal { get; set; } = string.Empty;

    /// <summary>
    /// 项目经办人-内码
    /// </summary>
    [SugarColumn(ColumnName = "FPERSON")]
    public string Fperson { get; set; } = string.Empty;

    /// <summary>
    /// 项目内容
    /// </summary>
    [SugarColumn(ColumnName = "FPRCONTEXT")]
    public string Fprcontext { get; set; } = string.Empty;

    /// <summary>
    /// 协作部门-内码
    /// </summary>
    [SugarColumn(ColumnName = "FCOOPID")]
    public string Fcoopid { get; set; } = string.Empty;

    /// <summary>
    /// 申请理由
    /// </summary>
    [SugarColumn(ColumnName = "FREASON")]
    public string Freason { get; set; } = string.Empty;

    /// <summary>
    /// 预算
    /// </summary>
    [SugarColumn(ColumnName = "FBUDGET")]
    public decimal Fbudget { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKCONTEXT")]
    public string Fcheckcontext { get; set; } = string.Empty;

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
