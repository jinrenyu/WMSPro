using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 虚拟线边仓物料转移
/// </summary>
[SugarTable("T_PRD_WORKSHOPVIRTUALSTOCKTRANS")]
public class TPrdWorkshopvirtualstocktrans : BaseEntity
{
    /// <summary>
    /// 类别编号
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETRANTYPE")]
    public string Fsourcetrantype { get; set; } = string.Empty;

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBILLNO")]
    public string Fsourcebillno { get; set; } = string.Empty;

    /// <summary>
    /// 出入库单类型
    /// </summary>
    [SugarColumn(ColumnName = "FTRANTYPE")]
    public int Ftrantype { get; set; }

    /// <summary>
    /// 目标仓库
    /// </summary>
    [SugarColumn(ColumnName = "FTARGETSTOCKID")]
    public string Ftargetstockid { get; set; } = string.Empty;

    /// <summary>
    /// 目标仓位
    /// </summary>
    [SugarColumn(ColumnName = "FTARGETSTOCKLOCID")]
    public string Ftargetstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 车间内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 是否导入
    /// </summary>
    [SugarColumn(ColumnName = "FIMPORT")]
    public int Fimport { get; set; }

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
    /// 任务单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FICMODETAILID")]
    public string Ficmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 生产用料清单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMENTRYID")]
    public string Fppbomentryid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
