using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工单派工管理
/// </summary>
[SugarTable("T_ENG_WORKORDERMANAGE")]
public class TEngWorkordermanage : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 维护任务内码
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTTASKID")]
    public string Fmainttaskid { get; set; } = string.Empty;

    /// <summary>
    /// 工单状态
    /// </summary>
    [SugarColumn(ColumnName = "FORDERSTATUS")]
    public int Forderstatus { get; set; }

    /// <summary>
    /// 派工状态
    /// </summary>
    [SugarColumn(ColumnName = "FPUBLISHSTATUS")]
    public bool Fpublishstatus { get; set; }

    /// <summary>
    /// 工单类型
    /// </summary>
    [SugarColumn(ColumnName = "FORDERTYPE")]
    public string Fordertype { get; set; } = string.Empty;

    /// <summary>
    /// 计划开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FPSTARTTIME")]
    public DateTime? Fpstarttime { get; set; }

    /// <summary>
    /// 实际开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FASTARTTIME")]
    public DateTime? Fastarttime { get; set; }

    /// <summary>
    /// 计划结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FPENDTIME")]
    public DateTime? Fpendtime { get; set; }

    /// <summary>
    /// 实际结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FAENDTIME")]
    public DateTime? Faendtime { get; set; }

    /// <summary>
    /// 计划维护时长
    /// </summary>
    [SugarColumn(ColumnName = "FPREPAIRTIME")]
    public decimal Fprepairtime { get; set; }

    /// <summary>
    /// 实际维护时长
    /// </summary>
    [SugarColumn(ColumnName = "FAREPAIRTIME")]
    public decimal Farepairtime { get; set; }

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEID")]
    public string Fmachineid { get; set; } = string.Empty;

    /// <summary>
    /// 工单来源
    /// </summary>
    [SugarColumn(ColumnName = "FORDERFROM")]
    public int Forderfrom { get; set; }

    /// <summary>
    /// 优先级
    /// </summary>
    [SugarColumn(ColumnName = "FPRIORITY")]
    public int Fpriority { get; set; }

    /// <summary>
    /// 自修
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRBYSELF")]
    public bool Frepairbyself { get; set; }

    /// <summary>
    /// 维修负责人
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRLEADER")]
    public string Frepairleader { get; set; } = string.Empty;

    /// <summary>
    /// 维修人
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRPERSON")]
    public string Frepairperson { get; set; } = string.Empty;

    /// <summary>
    /// 是否停机
    /// </summary>
    [SugarColumn(ColumnName = "FISSTOPMACHINE")]
    public bool Fisstopmachine { get; set; }

    /// <summary>
    /// 计划停机时间
    /// </summary>
    [SugarColumn(ColumnName = "FPSTOPTIME")]
    public decimal Fpstoptime { get; set; }

    /// <summary>
    /// 实际停机时长
    /// </summary>
    [SugarColumn(ColumnName = "FASTOPTIME")]
    public decimal Fastoptime { get; set; }

    /// <summary>
    /// 停机开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINESTOPSTART")]
    public DateTime? Fmachinestopstart { get; set; }

    /// <summary>
    /// 停机结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINESTOPEND")]
    public DateTime? Fmachinestopend { get; set; }

    /// <summary>
    /// 计划材料花费
    /// </summary>
    [SugarColumn(ColumnName = "FPMATERIALCOST")]
    public decimal Fpmaterialcost { get; set; }

    /// <summary>
    /// 实际材料花费
    /// </summary>
    [SugarColumn(ColumnName = "FAMATERIALCOST")]
    public decimal Famaterialcost { get; set; }

    /// <summary>
    /// 计划人工花费
    /// </summary>
    [SugarColumn(ColumnName = "FPMANUALCOST")]
    public decimal Fpmanualcost { get; set; }

    /// <summary>
    /// 实际人工花费
    /// </summary>
    [SugarColumn(ColumnName = "FAMANUALCOST")]
    public decimal Famanualcost { get; set; }

    /// <summary>
    /// 计划其他花费
    /// </summary>
    [SugarColumn(ColumnName = "FPOTHERCOST")]
    public decimal Fpothercost { get; set; }

    /// <summary>
    /// 实际其他花费
    /// </summary>
    [SugarColumn(ColumnName = "FAOTHERCOST")]
    public decimal Faothercost { get; set; }

    /// <summary>
    /// 总体评分
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALSCORE")]
    public decimal Ftotalscore { get; set; }

    /// <summary>
    /// 安全说明
    /// </summary>
    [SugarColumn(ColumnName = "FSAFETYINSTRUCTION")]
    public string Fsafetyinstruction { get; set; } = string.Empty;

    /// <summary>
    /// 情况简述
    /// </summary>
    [SugarColumn(ColumnName = "FCONDITIONRESUME")]
    public string Fconditionresume { get; set; } = string.Empty;

    /// <summary>
    /// 原因简述
    /// </summary>
    [SugarColumn(ColumnName = "FREASONRESUME")]
    public string Freasonresume { get; set; } = string.Empty;

    /// <summary>
    /// 措施简述
    /// </summary>
    [SugarColumn(ColumnName = "FMEASURERESUME")]
    public string Fmeasureresume { get; set; } = string.Empty;

    /// <summary>
    /// 预防对策
    /// </summary>
    [SugarColumn(ColumnName = "FPREVENTIVEMEASURE")]
    public string Fpreventivemeasure { get; set; } = string.Empty;

    /// <summary>
    /// 合同编号
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACTNUMBER")]
    public string Fcontractnumber { get; set; } = string.Empty;

    /// <summary>
    /// 合同名称
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACTNAME")]
    public string Fcontractname { get; set; } = string.Empty;

    /// <summary>
    /// 联系地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILE")]
    public string Fmobile { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPANYNAME")]
    public string Fcompanyname { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FLINKMAN")]
    public string Flinkman { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否结案
    /// </summary>
    [SugarColumn(ColumnName = "FISCASECLOSED")]
    public bool Fiscaseclosed { get; set; }

    /// <summary>
    /// 维护时间
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTDATE")]
    public DateTime? Fmaintdate { get; set; }

    /// <summary>
    /// 维护类别
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTTYPE")]
    public int Fmainttype { get; set; }

    /// <summary>
    /// 维护后状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTATUSAFTERMAINT")]
    public int Fstatusaftermaint { get; set; }

    /// <summary>
    /// 维修费用
    /// </summary>
    [SugarColumn(ColumnName = "FUPKEEPCOSTS")]
    public decimal Fupkeepcosts { get; set; }

    /// <summary>
    /// 标准方案内码
    /// </summary>
    [SugarColumn(ColumnName = "FPLANID")]
    public string Fplanid { get; set; } = string.Empty;

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
    /// 验收人
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTORID")]
    public string Facceptorid { get; set; } = string.Empty;

    /// <summary>
    /// 验收时间
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTTIME")]
    public DateTime? Faccepttime { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
