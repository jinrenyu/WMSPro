using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 巡检任务表体
/// </summary>
[SugarTable("T_SFC_INSPECTIONTASKENTRY")]
public class TSfcInspectiontaskentry : BaseEntity
{
    /// <summary>
    /// 检验内容ID
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKCONTENTID")]
    public string Fcheckcontentid { get; set; } = string.Empty;

    /// <summary>
    /// 检测方式 0=目检;1=机检
    /// </summary>
    [SugarColumn(ColumnName = "FTESTTYPE")]
    public int Ftesttype { get; set; }

    /// <summary>
    /// 结果值类型
    /// </summary>
    [SugarColumn(ColumnName = "FVALUETYPE")]
    public int Fvaluetype { get; set; }

    /// <summary>
    /// 结果值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE")]
    public string Fvalue { get; set; } = string.Empty;

    /// <summary>
    /// 检查结果  0=初始值，1=合格，2=不合格
    /// </summary>
    [SugarColumn(ColumnName = "FRESULT")]
    public int Fresult { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    [SugarColumn(ColumnName = "FPICTURE")]
    public byte[]? Fpicture { get; set; }

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
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
