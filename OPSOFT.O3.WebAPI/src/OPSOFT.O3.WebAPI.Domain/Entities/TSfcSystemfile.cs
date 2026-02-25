using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统文件管理表
/// </summary>
[SugarTable("T_SFC_SYSTEMFILE")]
public class TSfcSystemfile : BaseEntity
{
    /// <summary>
    /// 单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FBILLID")]
    public string Fbillid { get; set; } = string.Empty;

    /// <summary>
    /// 系统文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FFILENAME")]
    public string Ffilename { get; set; } = string.Empty;

    /// <summary>
    /// 名称路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

    /// <summary>
    /// 版本描述
    /// </summary>
    [SugarColumn(ColumnName = "FVERDESC")]
    public string Fverdesc { get; set; } = string.Empty;

    /// <summary>
    /// 关键信息描述
    /// </summary>
    [SugarColumn(ColumnName = "FREMK")]
    public string Fremk { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FEFFECTIVEDATE")]
    public DateTime? Feffectivedate { get; set; }

    /// <summary>
    /// 关闭日期
    /// </summary>
    [SugarColumn(ColumnName = "FEXPIRYDATE")]
    public DateTime? Fexpirydate { get; set; }

    /// <summary>
    /// 文件类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 下载次数
    /// </summary>
    [SugarColumn(ColumnName = "FLOADCOUNT")]
    public int Floadcount { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
