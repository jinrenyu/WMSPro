using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 派工表
/// </summary>
[SugarTable("T_SFC_DISPATCH")]
public class TSfcDispatch : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡编号
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDBILLNO")]
    public string Fflowcardbillno { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// (此工序)数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 工单（任务单）内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 工单（任务单）编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 工单（任务单）明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 单位标准工时(分
    /// </summary>
    [SugarColumn(ColumnName = "FSTADTIME")]
    public decimal Fstadtime { get; set; }

    /// <summary>
    /// 本次 派工数量
    /// </summary>
    [SugarColumn(ColumnName = "FTHISCOUNT", IsNullable = true)]
    public decimal? FTHISCOUNT { get; set; }

    /// <summary>
    /// 计划完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFINISHDATE", IsNullable = true)]
    public DateTime? FPLANFINISHDATE { get; set; }

    /// <summary>
    /// 计划生产数量
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFAUXQTY", IsNullable = true)]
    public decimal? FPLANFAUXQTY { get; set; }

    /// <summary>
    /// 时间范围(时)
    /// </summary>
    [SugarColumn(ColumnName = "FTIMERANGE", IsNullable = true)]
    public decimal? FTIMERANGE { get; set; }

    /// <summary>
    /// 处理数量
    /// </summary>
    [SugarColumn(ColumnName = "FSCHEDULEDQTY", IsNullable = true)]
    public decimal? FSCHEDULEDQTY { get; set; }

    /// <summary>
    /// 移转批量
    /// </summary>
    [SugarColumn(ColumnName = "FTRQTY", IsNullable = true)]
    public decimal? FTRQTY { get; set; }

    /// <summary>
    /// 标准换型时间(分)
    /// </summary>
    [SugarColumn(ColumnName = "FRETIME", IsNullable = true)]
    public decimal? FRETIME { get; set; }

    /// <summary>
    /// 派工开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTDATE", IsNullable = true)]
    public DateTime? FSTARTDATE { get; set; }

    /// <summary>
    /// 本次 开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTQTYDATE", IsNullable = true)]
    public DateTime? FSTARTQTYDATE { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 产品工艺表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROROUDETAILID", IsNullable = true)]
    public string FPROROUDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 本次 汇报数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTQTY", IsNullable = true)]
    public decimal? FREPORTQTY { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    [SugarColumn(ColumnName = "FOASTATUS", IsNullable = true)]
    public string FOASTATUS { get; set; } = string.Empty;

    /// <summary>
    /// 本次 汇报日期
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTQTYDATE", IsNullable = true)]
    public DateTime? FREPORTQTYDATE { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 优先级
    /// </summary>
    [SugarColumn(ColumnName = "FPRIOROTY", IsNullable = true)]
    public string FPRIOROTY { get; set; } = string.Empty;

    /// <summary>
    /// 数据是否属于排程
    /// </summary>
    [SugarColumn(ColumnName = "FISAPS", IsNullable = true)]
    public bool? FISAPS { get; set; }

    /// <summary>
    /// 工序长度（分）
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSLENGTH", IsNullable = true)]
    public decimal? FPROCESSLENGTH { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL", IsNullable = true)]
    public bool? FCHECKLEVEL { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID", IsNullable = true)]
    public string FRESOURCESID { get; set; } = string.Empty;

    /// <summary>
    /// 派工结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE", IsNullable = true)]
    public DateTime? FENDDATE { get; set; }

    /// <summary>
    /// 移转时间长度(分)
    /// </summary>
    [SugarColumn(ColumnName = "FTRLENGTH", IsNullable = true)]
    public decimal? FTRLENGTH { get; set; }

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE", IsNullable = true)]
    public string FCODE { get; set; } = string.Empty;

    /// <summary>
    /// 加工班组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID", IsNullable = true)]
    public string FTEAMID { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 计划开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE", IsNullable = true)]
    public DateTime? FPLANSTARTDATE { get; set; }

    /// <summary>
    /// 本次 接收数量
    /// </summary>
    [SugarColumn(ColumnName = "FRECQTY", IsNullable = true)]
    public decimal? FRECQTY { get; set; }

    /// <summary>
    /// 等待时间
    /// </summary>
    [SugarColumn(ColumnName = "FWAITTIME", IsNullable = true)]
    public decimal? FWAITTIME { get; set; }

    /// <summary>
    /// 派工状态
    /// </summary>
    [SugarColumn(ColumnName = "FDISPATCHSTATUS", IsNullable = true)]
    public bool? FDISPATCHSTATUS { get; set; }

    /// <summary>
    /// 本次 开工数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTQTY", IsNullable = true)]
    public decimal? FSTARTQTY { get; set; }

    /// <summary>
    /// 打印派工卡次数
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTCOUNT", IsNullable = true)]
    public decimal? FPRINTCOUNT { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }

    /// <summary>
    /// 排程所需的内码
    /// </summary>
    [SugarColumn(ColumnName = "FSCHEDULINGDETAILID", IsNullable = true)]
    public string FSCHEDULINGDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID", IsNullable = true)]
    public string FWORKCENTERID { get; set; } = string.Empty;

    /// <summary>
    /// 本次 接收日期
    /// </summary>
    [SugarColumn(ColumnName = "FRECQTYDATE", IsNullable = true)]
    public DateTime? FRECQTYDATE { get; set; }

    /// <summary>
    /// 锁定状态
    /// </summary>
    [SugarColumn(ColumnName = "FISSCHEDULED", IsNullable = true)]
    public bool? FISSCHEDULED { get; set; }

    /// <summary>
    /// 准备时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANTIME", IsNullable = true)]
    public decimal? FPLANTIME { get; set; }

    /// <summary>
    /// 已派工数量
    /// </summary>
    [SugarColumn(ColumnName = "FALREADYCOUNT", IsNullable = true)]
    public decimal? FALREADYCOUNT { get; set; }

    /// <summary>
    /// 审批结果
    /// </summary>
    [SugarColumn(ColumnName = "FOARESULT", IsNullable = true)]
    public string FOARESULT { get; set; } = string.Empty;

    /// <summary>
    /// 关闭状态
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSED", IsNullable = true)]
    public bool? FCLOSED { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 操作员内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID", IsNullable = true)]
    public string FEMPID { get; set; } = string.Empty;

    /// <summary>
    /// 是否是排程临时数据
    /// </summary>
    [SugarColumn(ColumnName = "FISTEMP", IsNullable = true)]
    public bool? FISTEMP { get; set; }
}
