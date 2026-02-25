using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 考勤打卡记录
/// </summary>
[SugarTable("T_BD_ATTENDANCE")]
public class TBdAttendance : BaseEntity
{
    /// <summary>
    /// 数据来源
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETYPE")]
    public string FSourcetype { get; set; } = string.Empty;

    /// <summary>
    /// 基准时间
    /// </summary>
    [SugarColumn(ColumnName = "FBASECHECKTIME")]
    public string FBasechecktime { get; set; } = string.Empty;

    /// <summary>
    /// 实际打卡时间
    /// </summary>
    [SugarColumn(ColumnName = "FUSERCHECKTIME")]
    public string FUserchecktime { get; set; } = string.Empty;

    /// <summary>
    /// 关联的审批ID
    /// </summary>
    [SugarColumn(ColumnName = "FAPPROVEID")]
    public string FApproveid { get; set; } = string.Empty;

    /// <summary>
    /// 位置结果
    /// </summary>
    [SugarColumn(ColumnName = "FLOCATIONRESULT")]
    public string FLocationresult { get; set; } = string.Empty;

    /// <summary>
    /// 打卡结果
    /// </summary>
    [SugarColumn(ColumnName = "FTIMERESULT")]
    public string FTimeresult { get; set; } = string.Empty;

    /// <summary>
    /// 考勤类型
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKTYPE")]
    public string FChecktype { get; set; } = string.Empty;

    /// <summary>
    /// 打卡人的UserID
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string FUserid { get; set; } = string.Empty;

    /// <summary>
    /// 打卡记录ID
    /// </summary>
    [SugarColumn(ColumnName = "FRECORDID")]
    public string FRecordid { get; set; } = string.Empty;

    /// <summary>
    /// 排班ID
    /// </summary>
    [SugarColumn(ColumnName = "FPLANID")]
    public string FPlanid { get; set; } = string.Empty;

    /// <summary>
    /// 考勤组ID
    /// </summary>
    [SugarColumn(ColumnName = "GROUPID")]
    public string Groupid { get; set; } = string.Empty;

    /// <summary>
    /// 唯一标识ID
    /// </summary>
    [SugarColumn(ColumnName = "ID")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string FBillno { get; set; } = string.Empty;
}
