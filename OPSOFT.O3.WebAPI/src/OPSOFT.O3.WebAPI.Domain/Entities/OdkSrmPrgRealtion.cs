using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 程式关系表
/// </summary>
[SugarTable("ODK_SRM_PrgRealtion")]
public class OdkSrmPrgRealtion : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 写入功能编号
    /// </summary>
    [SugarColumn(ColumnName = "FPRGKEY")]
    public string Fprgkey { get; set; } = string.Empty;

    /// <summary>
    /// 功能按钮
    /// </summary>
    [SugarColumn(ColumnName = "FFUNCTION")]
    public string Ffunction { get; set; } = string.Empty;

    /// <summary>
    /// 读取功能编号
    /// </summary>
    [SugarColumn(ColumnName = "FREADPRGKEY")]
    public string Freadprgkey { get; set; } = string.Empty;

    /// <summary>
    /// 读取单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPE")]
    public string Fbilltype { get; set; } = string.Empty;

    /// <summary>
    /// 可看类型
    /// </summary>
    [SugarColumn(ColumnName = "FVISIBLTYPE")]
    public string Fvisibltype { get; set; } = string.Empty;

    /// <summary>
    /// 说明
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
}
