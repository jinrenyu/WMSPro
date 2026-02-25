using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 基础资料-会议室
/// </summary>
[SugarTable("T_IAS_MEETINGROOM")]
public class TIasMeetingroom : BaseEntity
{
    /// <summary>
    /// 会议室代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 会议室名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 会议室地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 容纳人数
    /// </summary>
    [SugarColumn(ColumnName = "FGALLERYFUL")]
    public int Fgalleryful { get; set; }

    /// <summary>
    /// 是否有投影仪
    /// </summary>
    [SugarColumn(ColumnName = "FISPROJECTOR")]
    public bool Fisprojector { get; set; }

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public string Fchecklevel { get; set; } = string.Empty;

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
