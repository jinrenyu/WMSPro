using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修申请
/// </summary>
[SugarTable("T_ENG_EPMAINTAINAPPLY")]
public class TEngEpmaintainapply : BaseEntity
{
    /// <summary>
    /// 优先级
    /// </summary>
    [SugarColumn(ColumnName = "FLEVEL")]
    public int Flevel { get; set; }

    /// <summary>
    /// 申请人内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROPOSERID")]
    public string Fproposerid { get; set; } = string.Empty;

    /// <summary>
    /// 班次内码
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSESID")]
    public string Fclassesid { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 问题等级
    /// </summary>
    [SugarColumn(ColumnName = "FISSUEGRADE")]
    public int Fissuegrade { get; set; }

    /// <summary>
    /// 保修时间
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRTIME")]
    public DateTime? Frepairtime { get; set; }

    /// <summary>
    /// 是否停机
    /// </summary>
    [SugarColumn(ColumnName = "FISSTOPMACHINE")]
    public bool Fisstopmachine { get; set; }

    /// <summary>
    /// 停机时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTOPTIME")]
    public DateTime? Fstoptime { get; set; }

    /// <summary>
    /// 问题
    /// </summary>
    [SugarColumn(ColumnName = "FISSUE")]
    public string Fissue { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 消除
    /// </summary>
    [SugarColumn(ColumnName = "FELIMINATE")]
    public bool Feliminate { get; set; }

    /// <summary>
    /// 消除时间
    /// </summary>
    [SugarColumn(ColumnName = "FELIMINATETIME")]
    public DateTime? Feliminatetime { get; set; }

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
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEID")]
    public string Fmachineid { get; set; } = string.Empty;

    /// <summary>
    /// 处理人内码
    /// </summary>
    [SugarColumn(ColumnName = "FASSIGNEDTOID")]
    public string Fassignedtoid { get; set; } = string.Empty;

    /// <summary>
    /// 审批意见
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKCONTEXT")]
    public string Fcheckcontext { get; set; } = string.Empty;

    /// <summary>
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTTIME")]
    public decimal Fcrafttime { get; set; }

    /// <summary>
    /// 完成
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPLETE")]
    public bool Fcomplete { get; set; }

    /// <summary>
    /// 申请部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FAPDIVISIONID")]
    public string Fapdivisionid { get; set; } = string.Empty;

    /// <summary>
    /// 申请来源
    /// </summary>
    [SugarColumn(ColumnName = "FAPFORM")]
    public string Fapform { get; set; } = string.Empty;

    /// <summary>
    /// 发生时间
    /// </summary>
    [SugarColumn(ColumnName = "FORTIME")]
    public DateTime? Fortime { get; set; }

    /// <summary>
    /// 处理情况
    /// </summary>
    [SugarColumn(ColumnName = "FHINFORMATION")]
    public string Fhinformation { get; set; } = string.Empty;

    /// <summary>
    /// 维修工单
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTENORDER")]
    public string Fmaintenorder { get; set; } = string.Empty;

    /// <summary>
    /// 计划开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FPSTARTTIME")]
    public DateTime? Fpstarttime { get; set; }

    /// <summary>
    /// 计划结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FPENDTIME")]
    public DateTime? Fpendtime { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
