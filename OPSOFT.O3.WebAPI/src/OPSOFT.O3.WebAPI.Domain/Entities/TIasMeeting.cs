using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 会议管理-表头
/// </summary>
[SugarTable("T_IAS_MEETING")]
public class TIasMeeting : BaseEntity
{
    /// <summary>
    /// 会议室内码
    /// </summary>
    [SugarColumn(ColumnName = "FMEETROOMID")]
    public string Fmeetroomid { get; set; } = string.Empty;

    /// <summary>
    /// 会议预计开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FBEGINPLANTIME")]
    public DateTime? Fbeginplantime { get; set; }

    /// <summary>
    /// 会议预计结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDPLANTIME")]
    public DateTime? Fendplantime { get; set; }

    /// <summary>
    /// 会议状态
    /// </summary>
    [SugarColumn(ColumnName = "FMEETSTATUS")]
    public string Fmeetstatus { get; set; } = string.Empty;

    /// <summary>
    /// 会议组织者
    /// </summary>
    [SugarColumn(ColumnName = "FMEETINGORGANIZER")]
    public string Fmeetingorganizer { get; set; } = string.Empty;

    /// <summary>
    /// 会议实际开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FBEGINACTUALTIME")]
    public DateTime? Fbeginactualtime { get; set; }

    /// <summary>
    /// 会议实际结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDACTUALTIME")]
    public DateTime? Fendactualtime { get; set; }

    /// <summary>
    /// 是否会议布置
    /// </summary>
    [SugarColumn(ColumnName = "FISSUBMET")]
    public bool Fissubmet { get; set; }

    /// <summary>
    /// 是否周期性会议
    /// </summary>
    [SugarColumn(ColumnName = "FISWEEKMET")]
    public bool Fisweekmet { get; set; }

    /// <summary>
    /// 会议主题
    /// </summary>
    [SugarColumn(ColumnName = "FSUBJECT")]
    public string Fsubject { get; set; } = string.Empty;

    /// <summary>
    /// EPart
    /// </summary>
    [SugarColumn(ColumnName = "EPART")]
    public string Epart { get; set; } = string.Empty;

    /// <summary>
    /// 图号
    /// </summary>
    [SugarColumn(ColumnName = "FDRAWINGNUMBER")]
    public string Fdrawingnumber { get; set; } = string.Empty;

    /// <summary>
    /// 物料号MPart
    /// </summary>
    [SugarColumn(ColumnName = "FMPART")]
    public string Fmpart { get; set; } = string.Empty;

    /// <summary>
    /// NBO号
    /// </summary>
    [SugarColumn(ColumnName = "FNBO")]
    public string Fnbo { get; set; } = string.Empty;

    /// <summary>
    /// 会议纪要首次提交日期
    /// </summary>
    [SugarColumn(ColumnName = "FFIRSTSUBMITDATE")]
    public DateTime? Ffirstsubmitdate { get; set; }

    /// <summary>
    /// 会议记录员
    /// </summary>
    [SugarColumn(ColumnName = "FRECORDER")]
    public string Frecorder { get; set; } = string.Empty;

    /// <summary>
    /// 会议纪要最后提交日期
    /// </summary>
    [SugarColumn(ColumnName = "FLASTSUBMITDATE")]
    public DateTime? Flastsubmitdate { get; set; }

    /// <summary>
    /// 会议所有事项的预计最终完成日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRCOMPLETEDATE")]
    public DateTime? Fprcompletedate { get; set; }

    /// <summary>
    /// 会议关闭日期
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSEDDATE")]
    public DateTime? Fcloseddate { get; set; }

    /// <summary>
    /// 会议提交次数
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITTIME")]
    public int Fsubmittime { get; set; }

    /// <summary>
    /// 提交日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITDATE")]
    public DateTime? Fsubmitdate { get; set; }

    /// <summary>
    /// 会议提交人员
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITID")]
    public string Fsubmitid { get; set; } = string.Empty;

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public int Fchecklevel { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 单据类型：
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 关联任务
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEDETAILID")]
    public string Fsourcedetailid { get; set; } = string.Empty;

    /// <summary>
    /// D:天;W:周;M:月
    /// </summary>
    [SugarColumn(ColumnName = "FINTERVAL")]
    public string Finterval { get; set; } = string.Empty;

    /// <summary>
    /// 会议周期
    /// </summary>
    [SugarColumn(ColumnName = "FMEETINGCYCLE")]
    public string Fmeetingcycle { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
