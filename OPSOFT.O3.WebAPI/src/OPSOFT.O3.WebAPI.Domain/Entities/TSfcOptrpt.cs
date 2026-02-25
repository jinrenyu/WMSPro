using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序汇报-车间汇报
/// </summary>
[SugarTable("T_SFC_OPTRPT")]
public class TSfcOptrpt : BaseEntity
{
    /// <summary>
    /// 生产组织
    /// </summary>
    [SugarColumn(ColumnName = "FPRDORGID")]
    public string Fprdorgid { get; set; } = string.Empty;

    /// <summary>
    /// 加工车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 加工产线
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTLINEID")]
    public string Fproductlineid { get; set; } = string.Empty;

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 组织受托加工
    /// </summary>
    [SugarColumn(ColumnName = "FISENTRUST")]
    public bool Fisentrust { get; set; }

    /// <summary>
    /// 箱号
    /// </summary>
    [SugarColumn(ColumnName = "FBOXNUMBER")]
    public string Fboxnumber { get; set; } = string.Empty;

    /// <summary>
    /// 单据生成方式
    /// </summary>
    [SugarColumn(ColumnName = "FBILLGENTYPE")]
    public string Fbillgentype { get; set; } = string.Empty;

    /// <summary>
    /// 汇报数量（总）
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALRPTQTY")]
    public decimal Ftotalrptqty { get; set; }

    /// <summary>
    /// 电脑地址
    /// </summary>
    [SugarColumn(ColumnName = "PCID")]
    public string Pcid { get; set; } = string.Empty;

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
