using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP临时数据表
/// </summary>
[SugarTable("T_BD_ESOPTEMPDATA")]
public class TBdEsoptempdata : BaseEntity
{
    /// <summary>
    /// 工位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTATIONID")]
    public string Fstationid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
