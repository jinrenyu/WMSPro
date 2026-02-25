using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 度目设备
/// </summary>
[SugarTable("T_BD_DUMUINFO")]
public class TBdDumuinfo : BaseEntity
{
    /// <summary>
    /// 设备IP
    /// </summary>
    [SugarColumn(ColumnName = "FIP")]
    public string Fip { get; set; } = string.Empty;

    /// <summary>
    /// 设备URL
    /// </summary>
    [SugarColumn(ColumnName = "FURL")]
    public string Furl { get; set; } = string.Empty;

    /// <summary>
    /// 设备密码
    /// </summary>
    [SugarColumn(ColumnName = "FPASSWORD")]
    public string Fpassword { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

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
}
