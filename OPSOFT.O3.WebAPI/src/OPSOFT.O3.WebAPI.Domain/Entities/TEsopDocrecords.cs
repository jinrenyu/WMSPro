using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP文档操作记录
/// </summary>
[SugarTable("T_ESOP_DOCRECORDS")]
public class TEsopDocrecords : BaseEntity
{
    /// <summary>
    /// 文件内码
    /// </summary>
    [SugarColumn(ColumnName = "FDOCID")]
    public string Fdocid { get; set; } = string.Empty;

    /// <summary>
    /// 工位
    /// </summary>
    [SugarColumn(ColumnName = "FSTATIONID")]
    public string Fstationid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
