using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序汇报表-表体
/// </summary>
[SugarTable("T_SFC_REPORTENTRY")]
public class TSfcReportentry : BaseEntity
{
    /// <summary>
    /// 表体单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID")]
    public string Fresourcesid { get; set; } = string.Empty;

    /// <summary>
    /// 班组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID")]
    public string Fteamid { get; set; } = string.Empty;

    /// <summary>
    /// 班次内码
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTID")]
    public string Fshiftid { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSDTIME")]
    public DateTime? Fsdtime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FEDTIME")]
    public DateTime? Fedtime { get; set; }

    /// <summary>
    /// 累计用时(秒
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALSEC")]
    public decimal Ftotalsec { get; set; }

    /// <summary>
    /// 非正常累计用时(秒)
    /// </summary>
    [SugarColumn(ColumnName = "FNOWORKINGHOURSEC", IsNullable = true)]
    public decimal? FNOWORKINGHOURSEC { get; set; }

    /// <summary>
    /// 停工时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTOPDTIME", IsNullable = true)]
    public DateTime? FSTOPDTIME { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY", IsNullable = true)]
    public decimal? FQUAQTY { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODETYPE", IsNullable = true)]
    public int? FBARCODETYPE { get; set; }

    /// <summary>
    /// 已检验 0=未检验 1=已检验
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECK", IsNullable = true)]
    public bool? FISCHECK { get; set; }

    /// <summary>
    /// 不良品数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY", IsNullable = true)]
    public decimal? FBADQTY { get; set; }

    /// <summary>
    /// 产生方式 0=汇报产生 1=检验产生
    /// </summary>
    [SugarColumn(ColumnName = "FCREATETYPE", IsNullable = true)]
    public int? FCREATETYPE { get; set; }

    /// <summary>
    /// 检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKQTY", IsNullable = true)]
    public decimal? FCHECKQTY { get; set; }

    /// <summary>
    /// 汇报类型
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTTYPE", IsNullable = true)]
    public int? FREPORTTYPE { get; set; }

    /// <summary>
    /// 汇报数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 时间段类型
    /// </summary>
    [SugarColumn(ColumnName = "FDATATYPE", IsNullable = true)]
    public string FDATATYPE { get; set; } = string.Empty;

    /// <summary>
    /// 正常累计用时(秒)
    /// </summary>
    [SugarColumn(ColumnName = "FWORKINGHOURSEC", IsNullable = true)]
    public decimal? FWORKINGHOURSEC { get; set; }

    /// <summary>
    /// 汇报条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE", IsNullable = true)]
    public string FBARCODE { get; set; } = string.Empty;

    /// <summary>
    /// 计件工资数量
    /// </summary>
    [SugarColumn(ColumnName = "FPAYQTY", IsNullable = true)]
    public decimal? FPAYQTY { get; set; }
}
