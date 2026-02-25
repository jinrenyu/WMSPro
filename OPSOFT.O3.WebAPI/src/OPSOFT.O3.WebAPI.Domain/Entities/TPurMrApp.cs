using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 退料申请单
/// </summary>
[SugarTable("T_PUR_MRAPP")]
public class TPurMrApp : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 同步单据
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

    /// <summary>
    /// ERP编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string Ferpno { get; set; } = string.Empty;

    /// <summary>
    /// ERP内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string Ferpid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 申请部门
    /// </summary>
    [SugarColumn(ColumnName = "FAPPDEPTID")]
    public string Fappdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 价目表
    /// </summary>
    [SugarColumn(ColumnName = "FPRICELISTID")]
    public string Fpricelistid { get; set; } = string.Empty;

    /// <summary>
    /// 需求组织
    /// </summary>
    [SugarColumn(ColumnName = "FREQUIREORGID")]
    public string Frequireorgid { get; set; } = string.Empty;

    /// <summary>
    /// 退料类型
    /// </summary>
    [SugarColumn(ColumnName = "FRMTYPE")]
    public string Frmtype { get; set; } = string.Empty;

    /// <summary>
    /// 退料方式
    /// </summary>
    [SugarColumn(ColumnName = "FRMMODE")]
    public string Frmmode { get; set; } = string.Empty;

    /// <summary>
    /// 补料方式
    /// </summary>
    [SugarColumn(ColumnName = "FREPLENISHMODE")]
    public string Freplenishmode { get; set; } = string.Empty;

    /// <summary>
    /// 退料地点
    /// </summary>
    [SugarColumn(ColumnName = "FRMLOC")]
    public string Frmloc { get; set; } = string.Empty;

    /// <summary>
    /// 退料原因
    /// </summary>
    [SugarColumn(ColumnName = "FRMREASON")]
    public string Frmreason { get; set; } = string.Empty;

    /// <summary>
    /// 采购组织
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASEORGID")]
    public string Fpurchaseorgid { get; set; } = string.Empty;

    /// <summary>
    /// 结算组织
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLETYPEID")]
    public string Fsettletypeid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

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
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FBILLAMOUNT")]
    public string Fbillamount { get; set; } = string.Empty;

    /// <summary>
    /// 价外税
    /// </summary>
    [SugarColumn(ColumnName = "FISPRICEEXCLUDETAX")]
    public bool Fispriceexcludetax { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 对应组织
    /// </summary>
    [SugarColumn(ColumnName = "FCORRESPONDORGID")]
    public string Fcorrespondorgid { get; set; } = string.Empty;

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
    /// 退料组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKORGID")]
    public string Fstockorgid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

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
