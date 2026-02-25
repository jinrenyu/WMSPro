using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 会议管理-执行反馈
/// </summary>
[SugarTable("T_IAS_MEETINGENTRY4")]
public class TIasMeetingentry4 : BaseEntity
{
    /// <summary>
    /// 反馈编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;

    /// <summary>
    /// 序号
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public string Fseq { get; set; } = string.Empty;

    /// <summary>
    /// 议题内容
    /// </summary>
    [SugarColumn(ColumnName = "FREMARK")]
    public string Fremark { get; set; } = string.Empty;

    /// <summary>
    /// 议题类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 第一责任人
    /// </summary>
    [SugarColumn(ColumnName = "FTRANSACTOR")]
    public string Ftransactor { get; set; } = string.Empty;

    /// <summary>
    /// 二、三责任人
    /// </summary>
    [SugarColumn(ColumnName = "FDESIGNATED")]
    public string Fdesignated { get; set; } = string.Empty;

    /// <summary>
    /// 跟踪人
    /// </summary>
    [SugarColumn(ColumnName = "FTRACKING")]
    public string Ftracking { get; set; } = string.Empty;

    /// <summary>
    /// 预计完成日期
    /// </summary>
    [SugarColumn(ColumnName = "FPREPLANDATE")]
    public DateTime? Fpreplandate { get; set; }

    /// <summary>
    /// 预计完成日期更新
    /// </summary>
    [SugarColumn(ColumnName = "FPREPLANUPDATE")]
    public DateTime? Fpreplanupdate { get; set; }

    /// <summary>
    /// 预计完成日期更新说明
    /// </summary>
    [SugarColumn(ColumnName = "FPREUPDATENOTE")]
    public string Fpreupdatenote { get; set; } = string.Empty;

    /// <summary>
    /// 预计完成日期延迟是否有影响
    /// </summary>
    [SugarColumn(ColumnName = "FISIMPACT")]
    public bool Fisimpact { get; set; }

    /// <summary>
    /// 预计完成日期更新修改时间
    /// </summary>
    [SugarColumn(ColumnName = "FPREPLANUPDATEMODIFYTIME")]
    public DateTime? Fpreplanupdatemodifytime { get; set; }

    /// <summary>
    /// 最后异动时间戳
    /// </summary>
    [SugarColumn(ColumnName = "FMODIFYTIME")]
    public string Fmodifytime { get; set; } = string.Empty;

    /// <summary>
    /// 提醒提前期
    /// </summary>
    [SugarColumn(ColumnName = "FREMINDLT")]
    public int Fremindlt { get; set; }

    /// <summary>
    /// 议题实际关闭日期
    /// </summary>
    [SugarColumn(ColumnName = "FMEETCLOSEDATE")]
    public DateTime? Fmeetclosedate { get; set; }

    /// <summary>
    /// 议题实际关闭人
    /// </summary>
    [SugarColumn(ColumnName = "FMEETCLOSEPERSON")]
    public string Fmeetcloseperson { get; set; } = string.Empty;

    /// <summary>
    /// 议题组织者实际关闭日期
    /// </summary>
    [SugarColumn(ColumnName = "FORGCLOSEDATE")]
    public DateTime? Forgclosedate { get; set; }

    /// <summary>
    /// 议题执行反馈情况
    /// </summary>
    [SugarColumn(ColumnName = "FMEETACTCONDITION")]
    public string Fmeetactcondition { get; set; } = string.Empty;

    /// <summary>
    /// 议题状态
    /// </summary>
    [SugarColumn(ColumnName = "FMEETSTATUS")]
    public int Fmeetstatus { get; set; }

    /// <summary>
    /// 关联行
    /// </summary>
    [SugarColumn(ColumnName = "FRELATEROW")]
    public string Frelaterow { get; set; } = string.Empty;

    /// <summary>
    /// 议题备注
    /// </summary>
    [SugarColumn(ColumnName = "FMEETNOTE")]
    public string Fmeetnote { get; set; } = string.Empty;

    /// <summary>
    /// 行动方案执行反馈附件
    /// </summary>
    [SugarColumn(ColumnName = "FACTATTACHMENT")]
    public string Factattachment { get; set; } = string.Empty;

    /// <summary>
    /// 关联行状态
    /// </summary>
    [SugarColumn(ColumnName = "FRELATESTATUS")]
    public string Frelatestatus { get; set; } = string.Empty;

    /// <summary>
    /// 提醒提前期
    /// </summary>
    [SugarColumn(ColumnName = "FLEADTIME")]
    public int Fleadtime { get; set; }

    /// <summary>
    /// 议题最后提交日期
    /// </summary>
    [SugarColumn(ColumnName = "FFINALMEETDATE")]
    public DateTime? Ffinalmeetdate { get; set; }

    /// <summary>
    /// 议题最后提交人
    /// </summary>
    [SugarColumn(ColumnName = "FFINALMEETPERSON")]
    public string Ffinalmeetperson { get; set; } = string.Empty;

    /// <summary>
    /// 议题最后保存日期
    /// </summary>
    [SugarColumn(ColumnName = "FFINALMEETSAVEDATE")]
    public DateTime? Ffinalmeetsavedate { get; set; }

    /// <summary>
    /// 议题最后保存人
    /// </summary>
    [SugarColumn(ColumnName = "FFINALMEETSAVEPERSON")]
    public string Ffinalmeetsaveperson { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
