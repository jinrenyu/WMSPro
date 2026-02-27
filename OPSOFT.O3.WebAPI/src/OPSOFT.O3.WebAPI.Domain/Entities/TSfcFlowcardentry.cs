using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡表体-工序
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY")]
public class TSfcFlowcardentry : BaseEntity
{
    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 工序序号
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSSEQ")]
    public string Fprocessseq { get; set; } = string.Empty;

    /// <summary>
    /// 工序状态
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSSTATUS")]
    public int Fprocessstatus { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 流转数量
    /// </summary>
    [SugarColumn(ColumnName = "FREMQTY")]
    public decimal Fremqty { get; set; }

    /// <summary>
    /// 单位转换率
    /// </summary>
    [SugarColumn(ColumnName = "FUNITCONVERT")]
    public decimal Funitconvert { get; set; }

    /// <summary>
    /// 计划开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE")]
    public DateTime? Fplanstartdate { get; set; }

    /// <summary>
    /// 计划完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFINISHDATE")]
    public DateTime? Fplanfinishdate { get; set; }

    /// <summary>
    /// 实际开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALSTARTDATE")]
    public DateTime? Factualstartdate { get; set; }

    /// <summary>
    /// 实际完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALFINISHDATE")]
    public DateTime? Factualfinishdate { get; set; }

    /// <summary>
    /// 关闭状态
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSED")]
    public bool Fclosed { get; set; }

    /// <summary>
    /// 是否挂起
    /// </summary>
    [SugarColumn(ColumnName = "FHANGUP")]
    public bool Fhangup { get; set; }

    /// <summary>
    /// 是否已确认
    /// </summary>
    [SugarColumn(ColumnName = "FISSCHEDULED")]
    public bool Fisscheduled { get; set; }

    /// <summary>
    /// 完工数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALFINISHQTY")]
    public decimal Ftotalfinishqty { get; set; }

    /// <summary>
    /// 合格数
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不良品数
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FQCTYPE")]
    public string Fqctype { get; set; } = string.Empty;

    /// <summary>
    /// 条码打印方式
    /// </summary>
    [SugarColumn(ColumnName = "FPRSEQBAR")]
    public int Fprseqbar { get; set; }

    /// <summary>
    /// 标准换型时间(分
    /// </summary>
    [SugarColumn(ColumnName = "FRETIME")]
    public decimal Fretime { get; set; }

    /// <summary>
    /// 是否必须开工
    /// </summary>
    [SugarColumn(ColumnName = "FISSTART", IsNullable = true)]
    public bool? FISSTART { get; set; }

    /// <summary>
    /// 单位计时工资
    /// </summary>
    [SugarColumn(ColumnName = "FSTADTCOST", IsNullable = true)]
    public decimal? FSTADTCOST { get; set; }

    /// <summary>
    /// 工序汇报系数(汇报配比)
    /// </summary>
    [SugarColumn(ColumnName = "FCOEFFICIENT", IsNullable = true)]
    public decimal? FCOEFFICIENT { get; set; }

    /// <summary>
    /// 标准包装数
    /// </summary>
    [SugarColumn(ColumnName = "FBOXNUM", IsNullable = true)]
    public decimal? FBOXNUM { get; set; }

    /// <summary>
    /// 是否必须投料
    /// </summary>
    [SugarColumn(ColumnName = "FISFEED", IsNullable = true)]
    public bool? FISFEED { get; set; }

    /// <summary>
    /// 允许委外
    /// </summary>
    [SugarColumn(ColumnName = "FISOUTSOURCE", IsNullable = true)]
    public int? FISOUTSOURCE { get; set; }

    /// <summary>
    /// 超产比例
    /// </summary>
    [SugarColumn(ColumnName = "FOVERRATIO", IsNullable = true)]
    public decimal? FOVERRATIO { get; set; }

    /// <summary>
    /// 在制品投料
    /// </summary>
    [SugarColumn(ColumnName = "FISPCFEED", IsNullable = true)]
    public bool? FISPCFEED { get; set; }

    /// <summary>
    /// 控制计划数量
    /// </summary>
    [SugarColumn(ColumnName = "FISCONTROLPLANQTY", IsNullable = true)]
    public int? FISCONTROLPLANQTY { get; set; }

    /// <summary>
    /// 所属序列
    /// </summary>
    [SugarColumn(ColumnName = "FBELONGSEQ", IsNullable = true)]
    public string FBELONGSEQ { get; set; } = string.Empty;

    /// <summary>
    /// 破坏数量
    /// </summary>
    [SugarColumn(ColumnName = "FDESTORYQTY", IsNullable = true)]
    public decimal? FDESTORYQTY { get; set; }

    /// <summary>
    /// 无配置时是否允许不输工作中心
    /// </summary>
    [SugarColumn(ColumnName = "FISALLOWCENTEREMPTY", IsNullable = true)]
    public bool? FISALLOWCENTEREMPTY { get; set; }

    /// <summary>
    /// 工序首检内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSTESTID", IsNullable = true)]
    public string FPROCESSTESTID { get; set; } = string.Empty;

    /// <summary>
    /// 加工批量
    /// </summary>
    [SugarColumn(ColumnName = "FWORKQTY", IsNullable = true)]
    public decimal? FWORKQTY { get; set; }

    /// <summary>
    /// 巡检周期
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONCYCLE", IsNullable = true)]
    public decimal? FINSPECTIONCYCLE { get; set; }

    /// <summary>
    /// 是否必须汇报
    /// </summary>
    [SugarColumn(ColumnName = "FISREPORT", IsNullable = true)]
    public bool? FISREPORT { get; set; }

    /// <summary>
    /// 是否启用序列号追踪（预装配-绿地用）
    /// </summary>
    [SugarColumn(ColumnName = "FISSNTRACK", IsNullable = true)]
    public bool? FISSNTRACK { get; set; }

    /// <summary>
    /// 汇报日期是否允许在将来
    /// </summary>
    [SugarColumn(ColumnName = "FISALLOWFUTURETIME", IsNullable = true)]
    public int? FISALLOWFUTURETIME { get; set; }

    /// <summary>
    /// 可重复汇报
    /// </summary>
    [SugarColumn(ColumnName = "FISREPASS", IsNullable = true)]
    public bool? FISREPASS { get; set; }

    /// <summary>
    /// 是否计时
    /// </summary>
    [SugarColumn(ColumnName = "FISTIMINGWORK", IsNullable = true)]
    public bool? FISTIMINGWORK { get; set; }

    /// <summary>
    /// 巡检方案
    /// </summary>
    [SugarColumn(ColumnName = "FPQCSCHEMEID", IsNullable = true)]
    public string FPQCSCHEMEID { get; set; } = string.Empty;

    /// <summary>
    /// 启用清场校验
    /// </summary>
    [SugarColumn(ColumnName = "FISSITECLR", IsNullable = true)]
    public bool? FISSITECLR { get; set; }

    /// <summary>
    /// 是否必须接收
    /// </summary>
    [SugarColumn(ColumnName = "FISRECEIVE", IsNullable = true)]
    public bool? FISRECEIVE { get; set; }

    /// <summary>
    /// 无配置时是否允许不输资源
    /// </summary>
    [SugarColumn(ColumnName = "FISALLOWRESOURCEEMPTY", IsNullable = true)]
    public bool? FISALLOWRESOURCEEMPTY { get; set; }

    /// <summary>
    /// 是否计件
    /// </summary>
    [SugarColumn(ColumnName = "FISPIECEWORK", IsNullable = true)]
    public bool? FISPIECEWORK { get; set; }

    /// <summary>
    /// 控制转移数量
    /// </summary>
    [SugarColumn(ColumnName = "FISCONTROLTRANQTY", IsNullable = true)]
    public bool? FISCONTROLTRANQTY { get; set; }

    /// <summary>
    /// 是否委外检
    /// </summary>
    [SugarColumn(ColumnName = "FISENDQC", IsNullable = true)]
    public bool? FISENDQC { get; set; }

    /// <summary>
    /// 加工时间(分)
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTIME", IsNullable = true)]
    public decimal? FWORKTIME { get; set; }

    /// <summary>
    /// 是否已派工
    /// </summary>
    [SugarColumn(ColumnName = "FISDISPACHED", IsNullable = true)]
    public bool? FISDISPACHED { get; set; }

    /// <summary>
    /// 准备时间(分)
    /// </summary>
    [SugarColumn(ColumnName = "FPLANTIME", IsNullable = true)]
    public decimal? FPLANTIME { get; set; }

    /// <summary>
    /// 单位工资工时
    /// </summary>
    [SugarColumn(ColumnName = "FTCOSTTIME", IsNullable = true)]
    public decimal? FTCOSTTIME { get; set; }

    /// <summary>
    /// 是否首件检
    /// </summary>
    [SugarColumn(ColumnName = "FISHEADQC", IsNullable = true)]
    public bool? FISHEADQC { get; set; }

    /// <summary>
    /// 质检不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSBADQTY", IsNullable = true)]
    public decimal? FINSBADQTY { get; set; }

    /// <summary>
    /// 汇报控制参数
    /// </summary>
    [SugarColumn(ColumnName = "FRPTCTRLPARA", IsNullable = true)]
    public string FRPTCTRLPARA { get; set; } = string.Empty;

    /// <summary>
    /// 启用巡检
    /// </summary>
    [SugarColumn(ColumnName = "FISPQC", IsNullable = true)]
    public bool? FISPQC { get; set; }

    /// <summary>
    /// 委外检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FENDQCSCHEMEID", IsNullable = true)]
    public string FENDQCSCHEMEID { get; set; } = string.Empty;

    /// <summary>
    /// 工序关闭条件
    /// </summary>
    [SugarColumn(ColumnName = "FISCLOSESTATUS", IsNullable = true)]
    public int? FISCLOSESTATUS { get; set; }

    /// <summary>
    /// 工艺技能
    /// </summary>
    [SugarColumn(ColumnName = "FSKILLID", IsNullable = true)]
    public string FSKILLID { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用工步
    /// </summary>
    [SugarColumn(ColumnName = "FISPROCESSSTEP", IsNullable = true)]
    public bool? FISPROCESSSTEP { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 过程检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FCOURSEQCSCHEMEID", IsNullable = true)]
    public string FCOURSEQCSCHEMEID { get; set; } = string.Empty;

    /// <summary>
    /// 开工模式
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTMODE", IsNullable = true)]
    public int? FSTARTMODE { get; set; }

    /// <summary>
    /// 是否受过程检卡控
    /// </summary>
    [SugarColumn(ColumnName = "FISCONCOURSEQC", IsNullable = true)]
    public bool? FISCONCOURSEQC { get; set; }

    /// <summary>
    /// 维修资源
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID", IsNullable = true)]
    public string FRESOURCEID { get; set; } = string.Empty;

    /// <summary>
    /// 移转批量
    /// </summary>
    [SugarColumn(ColumnName = "FTRQTY", IsNullable = true)]
    public decimal? FTRQTY { get; set; }

    /// <summary>
    /// 是否一次性汇报
    /// </summary>
    [SugarColumn(ColumnName = "FISONETIMEREPORT", IsNullable = true)]
    public bool? FISONETIMEREPORT { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 序列号管理模式
    /// </summary>
    [SugarColumn(ColumnName = "FSNMODE", IsNullable = true)]
    public int? FSNMODE { get; set; }

    /// <summary>
    /// 是否排计划
    /// </summary>
    [SugarColumn(ColumnName = "FISPLAN", IsNullable = true)]
    public bool? FISPLAN { get; set; }

    /// <summary>
    /// 启用维修回线复检
    /// </summary>
    [SugarColumn(ColumnName = "FISRECHECK", IsNullable = true)]
    public bool? FISRECHECK { get; set; }

    /// <summary>
    /// 是否受巡检卡控
    /// </summary>
    [SugarColumn(ColumnName = "FISCONSPQC", IsNullable = true)]
    public bool? FISCONSPQC { get; set; }

    /// <summary>
    /// 清场方案
    /// </summary>
    [SugarColumn(ColumnName = "FSITECLRSCHEMEID", IsNullable = true)]
    public string FSITECLRSCHEMEID { get; set; } = string.Empty;

    /// <summary>
    /// 是否过程检
    /// </summary>
    [SugarColumn(ColumnName = "FISCOURSEQC", IsNullable = true)]
    public bool? FISCOURSEQC { get; set; }

    /// <summary>
    /// 首检方案
    /// </summary>
    [SugarColumn(ColumnName = "FHEADQCSCHEMEID", IsNullable = true)]
    public string FHEADQCSCHEMEID { get; set; } = string.Empty;

    /// <summary>
    /// 是否必须上模
    /// </summary>
    [SugarColumn(ColumnName = "FISUPPERMOLD", IsNullable = true)]
    public bool? FISUPPERMOLD { get; set; }

    /// <summary>
    /// 是否控制顺序
    /// </summary>
    [SugarColumn(ColumnName = "FISCONTROLORDER", IsNullable = true)]
    public bool? FISCONTROLORDER { get; set; }

    /// <summary>
    /// 是否启用序列号管理（打码--绿地用）
    /// </summary>
    [SugarColumn(ColumnName = "FISSNMANAGE", IsNullable = true)]
    public bool? FISSNMANAGE { get; set; }

    /// <summary>
    /// 成本中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FCOSTCENTERID", IsNullable = true)]
    public string FCOSTCENTERID { get; set; } = string.Empty;

    /// <summary>
    /// 投料模式
    /// </summary>
    [SugarColumn(ColumnName = "FFEEDMODE", IsNullable = true)]
    public int? FFEEDMODE { get; set; }

    /// <summary>
    /// 是否受首检卡控
    /// </summary>
    [SugarColumn(ColumnName = "FISCONHEADQC", IsNullable = true)]
    public bool? FISCONHEADQC { get; set; }

    /// <summary>
    /// 是否是灭菌工序
    /// </summary>
    [SugarColumn(ColumnName = "FISSTERILIZATION", IsNullable = true)]
    public bool? FISSTERILIZATION { get; set; }

    /// <summary>
    /// 返工数量
    /// </summary>
    [SugarColumn(ColumnName = "FREFLUXQTY", IsNullable = true)]
    public decimal? FREFLUXQTY { get; set; }

    /// <summary>
    /// 汇报时间
    /// </summary>
    [SugarColumn(ColumnName = "FISCONTROLTIME", IsNullable = true)]
    public int? FISCONTROLTIME { get; set; }

    /// <summary>
    /// 是否打印不良品条码
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTBAD", IsNullable = true)]
    public bool? FPRINTBAD { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 等待时间(分)
    /// </summary>
    [SugarColumn(ColumnName = "FWAITTIME", IsNullable = true)]
    public decimal? FWAITTIME { get; set; }

    /// <summary>
    /// 生产点检方案
    /// </summary>
    [SugarColumn(ColumnName = "FPSCSCHEMEID", IsNullable = true)]
    public string FPSCSCHEMEID { get; set; } = string.Empty;

    /// <summary>
    /// 启用生产点检
    /// </summary>
    [SugarColumn(ColumnName = "FISPSC", IsNullable = true)]
    public bool? FISPSC { get; set; }

    /// <summary>
    /// 汇报条码打印模式
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTMODE", IsNullable = true)]
    public int? FPRINTMODE { get; set; }

    /// <summary>
    /// 汇报模式
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTMODE", IsNullable = true)]
    public int? FREPORTMODE { get; set; }

    /// <summary>
    /// 单位标准工时(分)
    /// </summary>
    [SugarColumn(ColumnName = "FSTADTIME", IsNullable = true)]
    public decimal? FSTADTIME { get; set; }

    /// <summary>
    /// 单位计件工资
    /// </summary>
    [SugarColumn(ColumnName = "FSTADPCOST", IsNullable = true)]
    public decimal? FSTADPCOST { get; set; }
}
