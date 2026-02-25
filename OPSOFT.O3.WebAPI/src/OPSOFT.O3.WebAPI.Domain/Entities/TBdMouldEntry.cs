using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP模板表体
/// </summary>
[SugarTable("T_BD_MOULDENTRY")]
public class TBdMouldentry : BaseEntity
{
    /// <summary>
    /// 工位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTATIONID")]
    public string Fstationid { get; set; } = string.Empty;

    /// <summary>
    /// 文档表内码
    /// </summary>
    [SugarColumn(ColumnName = "FDOCINTERID")]
    public string Fdocinterid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 页码
    /// </summary>
    [SugarColumn(ColumnName = "FPAGE")]
    public int Fpage { get; set; }

    /// <summary>
    /// 客户端显示分屏位置
    /// </summary>
    [SugarColumn(ColumnName = "FPLAYSTATION")]
    public int Fplaystation { get; set; }

    /// <summary>
    /// 文档是否已经删除
    /// </summary>
    [SugarColumn(ColumnName = "FDCODELETED")]
    public bool Fdcodeleted { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
