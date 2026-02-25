using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 同步队列
/// </summary>
[SugarTable("T_SYN_QUEUE")]
public class TSynQueue : BaseEntity
{
    /// <summary>
    /// 产生时间
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 表单类别
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCNO")]
    public string Fsrcno { get; set; } = string.Empty;

    /// <summary>
    /// 单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 已同步次数
    /// </summary>
    [SugarColumn(ColumnName = "FSYNCOUNT")]
    public int Fsyncount { get; set; }

    /// <summary>
    /// 同步未成功日志
    /// </summary>
    [SugarColumn(ColumnName = "FSYNNOTE")]
    public string Fsynnote { get; set; } = string.Empty;

    /// <summary>
    /// 最大同步次数
    /// </summary>
    [SugarColumn(ColumnName = "FMAXSYNCOUNT")]
    public int Fmaxsyncount { get; set; }

    /// <summary>
    /// 是否实时同步
    /// </summary>
    [SugarColumn(ColumnName = "FISNOW")]
    public bool Fisnow { get; set; }

    /// <summary>
    /// 自增码
    /// </summary>
    [SugarColumn(ColumnName = "FID")]
    public string Fid { get; set; } = string.Empty;

    /// <summary>
    /// UID
    /// </summary>
    [SugarColumn(ColumnName = "FUID")]
    public string Fuid { get; set; } = string.Empty;
}
