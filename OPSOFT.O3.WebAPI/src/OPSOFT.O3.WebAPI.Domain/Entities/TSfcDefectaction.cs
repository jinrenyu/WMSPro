using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 线上SN不良处理措施
/// </summary>
[SugarTable("T_SFC_DEFECTACTION")]
public class TSfcDefectaction : BaseEntity
{
    /// <summary>
    /// SN条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 不良发生工序
    /// </summary>
    [SugarColumn(ColumnName = "FHAPPENPROCESSID")]
    public string Fhappenprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 不良处理措施
    /// </summary>
    [SugarColumn(ColumnName = "FACTION")]
    public int Faction { get; set; }

    /// <summary>
    /// 投入工序
    /// </summary>
    [SugarColumn(ColumnName = "FFEEDPROCESSID")]
    public string Ffeedprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 原因代码
    /// </summary>
    [SugarColumn(ColumnName = "FREASONCODE")]
    public string Freasoncode { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 发生时间
    /// </summary>
    [SugarColumn(ColumnName = "FTIME")]
    public DateTime? Ftime { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    [SugarColumn(ColumnName = "FBSTATUS")]
    public decimal Fbstatus { get; set; }

    /// <summary>
    /// 处理人
    /// </summary>
    [SugarColumn(ColumnName = "EMPID")]
    public string Empid { get; set; } = string.Empty;
}
