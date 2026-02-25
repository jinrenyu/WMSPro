using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 业务员类型数据
/// </summary>
[SugarTable("T_BD_OPERATOR")]
public class TBdOperator : BaseEntity
{
    /// <summary>
    /// 业务员代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 业务员名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;
}
