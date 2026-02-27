using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源生产状态变更履历
/// </summary>
[SugarTable("T_SFC_RESOURCEWORKSTATEHIS")]
public class TSfcResourceworkstatehis : BaseEntity
{
    /// <summary>
    /// 任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡号
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 数据状态
    /// </summary>
    [SugarColumn(ColumnName = "FDATASTATUS")]
    public string Fdatastatus { get; set; } = string.Empty;

    /// <summary>
    /// 呼叫类别
    /// </summary>
    [SugarColumn(ColumnName = "FCALLSTATUS")]
    public string Fcallstatus { get; set; } = string.Empty;

    /// <summary>
    /// 当前状态开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTATUSTIME")]
    public DateTime? Fstatustime { get; set; }

    /// <summary>
    /// 汇报数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTQTY")]
    public decimal Freportqty { get; set; }

    /// <summary>
    /// 计划生产数量
    /// </summary>
    [SugarColumn(ColumnName = "FPLANMAKEQTY")]
    public decimal Fplanmakeqty { get; set; }

    /// <summary>
    /// 当前生产率(数量/小时
    /// </summary>
    [SugarColumn(ColumnName = "ACTUALRATIO")]
    public decimal Actualratio { get; set; }

    /// <summary>
    /// 标准生产率
    /// </summary>
    [SugarColumn(ColumnName = "QUOTEDRATIO", IsNullable = true)]
    public decimal? QUOTEDRATIO { get; set; }

    /// <summary>
    /// 总共时间
    /// </summary>
    [SugarColumn(ColumnName = "TOTALMINUTE", IsNullable = true)]
    public decimal? TOTALMINUTE { get; set; }

    /// <summary>
    /// 汇报开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTSTARTTIME", IsNullable = true)]
    public DateTime? FREPORTSTARTTIME { get; set; }

    /// <summary>
    /// 汇报行内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID", IsNullable = true)]
    public string FREPORTDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 汇报设备ID
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPUTERID", IsNullable = true)]
    public string FCOMPUTERID { get; set; } = string.Empty;

    /// <summary>
    /// 扫描条码信息
    /// </summary>
    [SugarColumn(ColumnName = "FINPUTBARCODE", IsNullable = true)]
    public string FINPUTBARCODE { get; set; } = string.Empty;

    /// <summary>
    /// 汇报内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID", IsNullable = true)]
    public string FREPORTID { get; set; } = string.Empty;

    /// <summary>
    /// 累计本次完工数量
    /// </summary>
    [SugarColumn(ColumnName = "OUTPUTQTY", IsNullable = true)]
    public decimal? OUTPUTQTY { get; set; }

    /// <summary>
    /// 是否有产生换型临界预警
    /// </summary>
    [SugarColumn(ColumnName = "FISHB", IsNullable = true)]
    public bool? FISHB { get; set; }

    /// <summary>
    /// 扫描条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FINPUTBARTYPE", IsNullable = true)]
    public int? FINPUTBARTYPE { get; set; }

    /// <summary>
    /// 是否有产生换型准备预警
    /// </summary>
    [SugarColumn(ColumnName = "FISHA", IsNullable = true)]
    public bool? FISHA { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID", IsNullable = true)]
    public string FRESOURCEID { get; set; } = string.Empty;

    /// <summary>
    /// 废品率
    /// </summary>
    [SugarColumn(ColumnName = "FTT", IsNullable = true)]
    public decimal? FTT { get; set; }

    /// <summary>
    /// 产能利用率
    /// </summary>
    [SugarColumn(ColumnName = "TEEP", IsNullable = true)]
    public decimal? TEEP { get; set; }

    /// <summary>
    /// 设备综合效率
    /// </summary>
    [SugarColumn(ColumnName = "OEE", IsNullable = true)]
    public decimal? OEE { get; set; }
}
