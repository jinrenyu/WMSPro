using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 同步队列ERP中用
/// </summary>
[SugarTable("T_SYN_QUEUE_ERP")]
public class TSynQueueErp : BaseEntity
{
    /// <summary>
    /// 内码
    /// </summary>
    [SugarColumn(ColumnName = "FID")]
    public int Fid { get; set; }

    /// <summary>
    /// 单子编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;

    /// <summary>
    /// 单子状态
    /// </summary>
    [SugarColumn(ColumnName = "FDOCUMENTSTATUS")]
    public string Fdocumentstatus { get; set; } = string.Empty;

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
}
