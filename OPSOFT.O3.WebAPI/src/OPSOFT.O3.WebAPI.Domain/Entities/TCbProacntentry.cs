using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 产品组-产品信息
/// </summary>
[SugarTable("T_CB_PROACNTENTRY")]
public class TCbProacntentry : BaseEntity
{
    /// <summary>
    /// 产品ID
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTTYPE")]
    public string Fproducttype { get; set; } = string.Empty;

    /// <summary>
    /// 分配类型
    /// </summary>
    [SugarColumn(ColumnName = "FALLOCATETYPE")]
    public string Fallocatetype { get; set; } = string.Empty;

    /// <summary>
    /// 分配权重
    /// </summary>
    [SugarColumn(ColumnName = "FALLOCATEWEIGHT")]
    public string Fallocateweight { get; set; } = string.Empty;

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
