using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 任务单工序出入站
/// </summary>
[SugarTable("T_BD_WIPTRANSACTION")]
public class TBdWiptransaction : BaseEntity
{
    /// <summary>
    /// 任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FICMOID")]
    public string Ficmoid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTTIME")]
    public decimal Fcrafttime { get; set; }

    /// <summary>
    /// 开工数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTQTY")]
    public decimal Fstartqty { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTYPEID")]
    public int Fworktypeid { get; set; }

    /// <summary>
    /// 良品数
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不良品数
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FVENDERID")]
    public string Fvenderid { get; set; } = string.Empty;

    /// <summary>
    /// 委外发出数量
    /// </summary>
    [SugarColumn(ColumnName = "FOUTQTY")]
    public decimal Foutqty { get; set; }

    /// <summary>
    /// 委外加工费
    /// </summary>
    [SugarColumn(ColumnName = "FCOST")]
    public decimal Fcost { get; set; }

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
