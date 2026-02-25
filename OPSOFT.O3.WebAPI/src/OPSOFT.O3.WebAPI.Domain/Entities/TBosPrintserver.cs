using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 打印机配置表
/// </summary>
[SugarTable("T_BOS_PRINTSERVER")]
public class TBosPrintserver : BaseEntity
{
    /// <summary>
    /// 模块编码--程序代号
    /// </summary>
    [SugarColumn(ColumnName = "FMODELKEY")]
    public string Fmodelkey { get; set; } = string.Empty;

    /// <summary>
    /// 打印机ip
    /// </summary>
    [SugarColumn(ColumnName = "FIPIF")]
    public string Fipif { get; set; } = string.Empty;

    /// <summary>
    /// 端口号
    /// </summary>
    [SugarColumn(ColumnName = "FPORTNUMBER")]
    public string Fportnumber { get; set; } = string.Empty;

    /// <summary>
    /// 打印机名称
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTNAME")]
    public string Fprintname { get; set; } = string.Empty;

    /// <summary>
    /// 模板
    /// </summary>
    [SugarColumn(ColumnName = "FMOULD")]
    public string Fmould { get; set; } = string.Empty;

    /// <summary>
    /// 设备号
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENT")]
    public string Fequipment { get; set; } = string.Empty;

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
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;
}
