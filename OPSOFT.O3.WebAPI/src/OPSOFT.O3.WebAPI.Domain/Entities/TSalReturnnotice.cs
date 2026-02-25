using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 退货通知单
/// </summary>
[SugarTable("T_SAL_RETURNNOTICE")]
public class TSalReturnnotice : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 退货客户
    /// </summary>
    [SugarColumn(ColumnName = "FRETCUSTID")]
    public string Fretcustid { get; set; } = string.Empty;

    /// <summary>
    /// 库存组织
    /// </summary>
    [SugarColumn(ColumnName = "FRETORGID")]
    public string Fretorgid { get; set; } = string.Empty;

    /// <summary>
    /// 退货原因
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNREASON")]
    public string Freturnreason { get; set; } = string.Empty;

    /// <summary>
    /// 销售组织
    /// </summary>
    [SugarColumn(ColumnName = "FSALEORGID")]
    public string Fsaleorgid { get; set; } = string.Empty;

    /// <summary>
    /// 库存部门
    /// </summary>
    [SugarColumn(ColumnName = "FRETDEPTID")]
    public string Fretdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FHEADLOCID")]
    public string Fheadlocid { get; set; } = string.Empty;

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
    /// 结算币别
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECURRID")]
    public string Fsettlecurrid { get; set; } = string.Empty;

    /// <summary>
    /// 汇率类型
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGETYPEID")]
    public string Fexchangetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public decimal Fexchangerate { get; set; }

    /// <summary>
    /// 仓管员
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERID")]
    public string Fstockerid { get; set; } = string.Empty;

    /// <summary>
    /// 销售部门
    /// </summary>
    [SugarColumn(ColumnName = "FSALEDEPTID")]
    public string Fsaledeptid { get; set; } = string.Empty;

    /// <summary>
    /// 销售员
    /// </summary>
    [SugarColumn(ColumnName = "FSALESMANID")]
    public string Fsalesmanid { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 是否门卫放行
    /// </summary>
    [SugarColumn(ColumnName = "FPASS")]
    public bool Fpass { get; set; }

    /// <summary>
    /// 放行时间
    /// </summary>
    [SugarColumn(ColumnName = "FPASSTIME")]
    public DateTime? Fpasstime { get; set; }

    /// <summary>
    /// 放行人员
    /// </summary>
    [SugarColumn(ColumnName = "FPASSUSER")]
    public string Fpassuser { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 审批状态
    /// </summary>
    [SugarColumn(ColumnName = "FOASTATUS")]
    public string Foastatus { get; set; } = string.Empty;

    /// <summary>
    /// 审批结果
    /// </summary>
    [SugarColumn(ColumnName = "FOARESULT")]
    public string Foaresult { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
