using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡表体的表体-资源资料
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY3")]
public class TSfcFlowcardentry3 : BaseEntity
{
    /// <summary>
    /// 工作中心类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 工作资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKRESOURCEID")]
    public string Fworkresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 派工单内码
    /// </summary>
    [SugarColumn(ColumnName = "FDISPATCHID")]
    public string Fdispatchid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
