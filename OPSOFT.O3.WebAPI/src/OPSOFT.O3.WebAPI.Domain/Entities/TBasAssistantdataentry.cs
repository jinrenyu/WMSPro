using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 辅助资料
/// </summary>
[SugarTable("T_BAS_ASSISTANTDATAENTRY")]
public class TBasAssistantdataentry : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 辅助资料代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 辅助资料名称
    /// </summary>
    [SugarColumn(ColumnName = "FDATAVALUE")]
    public string Fdatavalue { get; set; } = string.Empty;

    /// <summary>
    /// 辅助资料类别
    /// </summary>
    [SugarColumn(ColumnName = "FID")]
    public string Fid { get; set; } = string.Empty;

    /// <summary>
    /// 上级资料
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTID")]
    public string Fparentid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 显示顺序
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public int Fseq { get; set; }

    /// <summary>
    /// 系统预设
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// 使用组织
    /// </summary>
    [SugarColumn(ColumnName = "FUSEORGID")]
    public string Fuseorgid { get; set; } = string.Empty;

    /// <summary>
    /// 审核人（IApprovable）
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期（IApprovable）
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; }

    /// <summary>
    /// 是否自建
    /// </summary>
    [SugarColumn(ColumnName = "FISBUILDSELF")]
    public bool Fisbuildself { get; set; }
}
