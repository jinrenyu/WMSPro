using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修工单派工管理
/// </summary>
[SugarTable("T_ENG_EPMTWOMANAGE")]
public class TEngEpmtwomanage : BaseEntity
{
    /// <summary>
    /// 验收时间
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTTIME")]
    public DateTime? Faccepttime { get; set; }

    /// <summary>
    /// 维护任务内码
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTTASKID")]
    public string Fmainttaskid { get; set; } = string.Empty;

    /// <summary>
    /// 工单类型
    /// </summary>
    [SugarColumn(ColumnName = "FORDERTYPE")]
    public string Fordertype { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEID")]
    public string Fmachineid { get; set; } = string.Empty;

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
    /// 维护商内码
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTAINERID")]
    public string Fmaintainerid { get; set; } = string.Empty;

    /// <summary>
    /// 合同内码
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACTID")]
    public string Fcontractid { get; set; } = string.Empty;

    /// <summary>
    /// 停机时间
    /// </summary>
    [SugarColumn(ColumnName = "FDOWNTIME")]
    public string Fdowntime { get; set; } = string.Empty;

    /// <summary>
    /// 操作人内码
    /// </summary>
    [SugarColumn(ColumnName = "FOPERATORID")]
    public string Foperatorid { get; set; } = string.Empty;

    /// <summary>
    /// 验收人内码
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTORID")]
    public string Facceptorid { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位编号
    /// </summary>
    [SugarColumn(ColumnName = "FAPNUMBER")]
    public string Fapnumber { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 报修故障类别内码
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTTYPEID")]
    public string Ffaulttypeid { get; set; } = string.Empty;

    /// <summary>
    /// 故障程度
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTDEGREE")]
    public string Ffaultdegree { get; set; } = string.Empty;

    /// <summary>
    /// 情况编号
    /// </summary>
    [SugarColumn(ColumnName = "FCASENUMBER")]
    public string Fcasenumber { get; set; } = string.Empty;

    /// <summary>
    /// 原因编号
    /// </summary>
    [SugarColumn(ColumnName = "FCAUSENUMBER")]
    public string Fcausenumber { get; set; } = string.Empty;

    /// <summary>
    /// 措施编号
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPNUMBER")]
    public string Fstepnumber { get; set; } = string.Empty;

    /// <summary>
    /// 停机损失
    /// </summary>
    [SugarColumn(ColumnName = "FDOWNTIMELOSS")]
    public string Fdowntimeloss { get; set; } = string.Empty;

    /// <summary>
    /// 维护类别
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTTYPE")]
    public string Fmainttype { get; set; } = string.Empty;

    /// <summary>
    /// 事故类型
    /// </summary>
    [SugarColumn(ColumnName = "FACCIDENTTYPE")]
    public string Faccidenttype { get; set; } = string.Empty;

    /// <summary>
    /// 合同编号
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACTNO")]
    public string Fcontractno { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACTS")]
    public string Fcontacts { get; set; } = string.Empty;

    /// <summary>
    /// 公司电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE")]
    public string Fphone { get; set; } = string.Empty;

    /// <summary>
    /// 公司内码
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPID")]
    public string Fcompid { get; set; } = string.Empty;

    /// <summary>
    /// 公司编号
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPANYNUMBER")]
    public string Fcompanynumber { get; set; } = string.Empty;

    /// <summary>
    /// 维修申请单内码
    /// </summary>
    [SugarColumn(ColumnName = "FAPPLYID")]
    public string Fapplyid { get; set; } = string.Empty;

    /// <summary>
    /// 原因简述
    /// </summary>
    [SugarColumn(ColumnName = "FREASONRESUME")]
    public string Freasonresume { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 维修处理情况
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRCONTEXT")]
    public string Frepaircontext { get; set; } = string.Empty;

    /// <summary>
    /// 工单来源
    /// </summary>
    [SugarColumn(ColumnName = "FORDERFROM")]
    public string Forderfrom { get; set; } = string.Empty;

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
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 减产名称
    /// </summary>
    [SugarColumn(ColumnName = "FCROPFAILURENAME")]
    public string Fcropfailurename { get; set; } = string.Empty;

    /// <summary>
    /// 负责人意见
    /// </summary>
    [SugarColumn(ColumnName = "FPRINCIPALCONTEXT")]
    public string Fprincipalcontext { get; set; } = string.Empty;

    /// <summary>
    /// 事故经过
    /// </summary>
    [SugarColumn(ColumnName = "FAFTER")]
    public string Fafter { get; set; } = string.Empty;

    /// <summary>
    /// 验收意见
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTORCONTEXT")]
    public string Facceptorcontext { get; set; } = string.Empty;

    /// <summary>
    /// 预防措施
    /// </summary>
    [SugarColumn(ColumnName = "FPRECAUTION")]
    public string Fprecaution { get; set; } = string.Empty;

    /// <summary>
    /// 设备建议
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTOPINION")]
    public string Fequipmentopinion { get; set; } = string.Empty;

    /// <summary>
    /// 领导意见
    /// </summary>
    [SugarColumn(ColumnName = "FLEADERCONTEXT")]
    public string Fleadercontext { get; set; } = string.Empty;

    /// <summary>
    /// 事故原因
    /// </summary>
    [SugarColumn(ColumnName = "FACCIDENTCAUSE")]
    public string Faccidentcause { get; set; } = string.Empty;

    /// <summary>
    /// 安全说明
    /// </summary>
    [SugarColumn(ColumnName = "FSECURENOTE")]
    public string Fsecurenote { get; set; } = string.Empty;

    /// <summary>
    /// 情况简述
    /// </summary>
    [SugarColumn(ColumnName = "FCASESKETCH")]
    public string Fcasesketch { get; set; } = string.Empty;

    /// <summary>
    /// 详细情况
    /// </summary>
    [SugarColumn(ColumnName = "FMINUTECASE")]
    public string Fminutecase { get; set; } = string.Empty;

    /// <summary>
    /// 原因简述
    /// </summary>
    [SugarColumn(ColumnName = "FCAUSESKETCH")]
    public string Fcausesketch { get; set; } = string.Empty;

    /// <summary>
    /// 详细原因
    /// </summary>
    [SugarColumn(ColumnName = "FMINUTECAUSE")]
    public string Fminutecause { get; set; } = string.Empty;

    /// <summary>
    /// 措施简述
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPSKETCH")]
    public string Fstepsketch { get; set; } = string.Empty;

    /// <summary>
    /// 详细措施
    /// </summary>
    [SugarColumn(ColumnName = "FMINUTESTEP")]
    public string Fminutestep { get; set; } = string.Empty;

    /// <summary>
    /// 维修进度
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTAINPLAN")]
    public string Fmaintainplan { get; set; } = string.Empty;

    /// <summary>
    /// 事故损失
    /// </summary>
    [SugarColumn(ColumnName = "FACCIDENTLOSS")]
    public string Faccidentloss { get; set; } = string.Empty;

    /// <summary>
    /// 合同名称
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACTNAME")]
    public string Fcontractname { get; set; } = string.Empty;

    /// <summary>
    /// 地点
    /// </summary>
    [SugarColumn(ColumnName = "FLOCATION")]
    public string Flocation { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPANYNAME")]
    public string Fcompanyname { get; set; } = string.Empty;

    /// <summary>
    /// 合同备注
    /// </summary>
    [SugarColumn(ColumnName = "FCONTRACTNOTE")]
    public string Fcontractnote { get; set; } = string.Empty;

    /// <summary>
    /// 计划维修时长
    /// </summary>
    [SugarColumn(ColumnName = "FPREPAIRTIME")]
    public decimal Fprepairtime { get; set; }

    /// <summary>
    /// 实际维修时长
    /// </summary>
    [SugarColumn(ColumnName = "FAREPAIRTIME")]
    public decimal Farepairtime { get; set; }

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
    /// 总分
    /// </summary>
    [SugarColumn(ColumnName = "FTOTOLSCORE")]
    public decimal Ftotolscore { get; set; }

    /// <summary>
    /// 事故日期
    /// </summary>
    [SugarColumn(ColumnName = "FACCIDENTDATE")]
    public DateTime? Faccidentdate { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FAPPLYFORDATE")]
    public DateTime? Fapplyfordate { get; set; }

    /// <summary>
    /// 保修到期日
    /// </summary>
    [SugarColumn(ColumnName = "FDUEDATE")]
    public DateTime? Fduedate { get; set; }

    /// <summary>
    /// 维修日期
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTDATE")]
    public DateTime? Fmaintdate { get; set; }

    /// <summary>
    /// 计划开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FPSTARTTIME")]
    public DateTime? Fpstarttime { get; set; }

    /// <summary>
    /// 实际开始时长
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
    /// 计划停机开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FPMACHINESTOPSTART")]
    public DateTime? Fpmachinestopstart { get; set; }

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
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 派工状态
    /// </summary>
    [SugarColumn(ColumnName = "FPUBLISHSTATUS")]
    public bool Fpublishstatus { get; set; }

    /// <summary>
    /// 是否停机
    /// </summary>
    [SugarColumn(ColumnName = "FISSTOPMACHINE")]
    public bool Fisstopmachine { get; set; }

    /// <summary>
    /// 是否消除
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTCLEARANCE")]
    public bool Ffaultclearance { get; set; }

    /// <summary>
    /// 自修
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRBYSELF")]
    public bool Frepairbyself { get; set; }

    /// <summary>
    /// 委外
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIROUT")]
    public bool Frepairout { get; set; }

    /// <summary>
    /// 验收只读
    /// </summary>
    [SugarColumn(ColumnName = "FREADONLY")]
    public bool Freadonly { get; set; }

    /// <summary>
    /// 工单状态
    /// </summary>
    [SugarColumn(ColumnName = "FORDERSTATUS")]
    public int Forderstatus { get; set; }

    /// <summary>
    /// 优先级
    /// </summary>
    [SugarColumn(ColumnName = "FPRIORITY")]
    public int Fpriority { get; set; }

    /// <summary>
    /// 停工天数
    /// </summary>
    [SugarColumn(ColumnName = "FSHUTDOWNDAYS")]
    public int Fshutdowndays { get; set; }

    /// <summary>
    /// 维护后状态（0=正常;1=维修中;2=维护中）
    /// </summary>
    [SugarColumn(ColumnName = "FSTATUSAFTERMAINT")]
    public int Fstatusaftermaint { get; set; }

    /// <summary>
    /// 报修人
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRAPPLICANTID")]
    public string Frepairapplicantid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
