using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 受托加工领料单
/// </summary>
[SugarTable("T_STK_OEMPICK")]
public class TStkOempick : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPE")]
    public string Fbilltype { get; set; } = string.Empty;

    /// <summary>
    /// 录入类型
    /// </summary>
    [SugarColumn(ColumnName = "FINPUTTYPE")]
    public int Finputtype { get; set; }

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

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
    /// 生产组织
    /// </summary>
    [SugarColumn(ColumnName = "FPRDORGID")]
    public string Fprdorgid { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 领料部门
    /// </summary>
    [SugarColumn(ColumnName = "FPICKDEPTID")]
    public string Fpickdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 倒冲来源
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETYPE")]
    public string Fsourcetype { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public int Fsrcformid { get; set; }

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 领料人
    /// </summary>
    [SugarColumn(ColumnName = "FPICKERID")]
    public string Fpickerid { get; set; } = string.Empty;

    /// <summary>
    /// 仓管员
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERID")]
    public string Fstockerid { get; set; } = string.Empty;

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
    /// 是否同步
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

    /// <summary>
    /// ERP单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string Ferpno { get; set; } = string.Empty;

    /// <summary>
    /// ERP单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string Ferpid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
