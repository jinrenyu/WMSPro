using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// Web版汇报操作日志
/// </summary>
[SugarTable("T_PRD_OPERATELOG")]
public class TPrdOperatelog : BaseEntity
{
    /// <summary>
    /// Mac地址
    /// </summary>
    [SugarColumn(ColumnName = "FPHONEID")]
    public string Fphoneid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID")]
    public string Freportid { get; set; } = string.Empty;

    /// <summary>
    /// 操作人内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "FOPERATETIME")]
    public DateTime? Foperatetime { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;
}
