using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 保养校验单
/// </summary>
[SugarTable("T_ENG_KEEPREPAIRCHECK")]
public class TEngKeeprepaircheck : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 作业内码
    /// </summary>
    [SugarColumn(ColumnName = "FJOBCLASSID")]
    public string Fjobclassid { get; set; } = string.Empty;

    /// <summary>
    /// 模治具内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOULDID")]
    public string Fmouldid { get; set; } = string.Empty;

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 保养人内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 状况描述
    /// </summary>
    [SugarColumn(ColumnName = "FCONDITION")]
    public string Fcondition { get; set; } = string.Empty;

    /// <summary>
    /// 保养地点
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 保养方法
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRSITUATIOR")]
    public string Frepairsituatior { get; set; } = string.Empty;

    /// <summary>
    /// 处理结果
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRRESULT")]
    public string Frepairresult { get; set; } = string.Empty;

    /// <summary>
    /// 检验状态（通过、不通过）
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTENANCESTATUS")]
    public string Fmaintenancestatus { get; set; } = string.Empty;

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
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
