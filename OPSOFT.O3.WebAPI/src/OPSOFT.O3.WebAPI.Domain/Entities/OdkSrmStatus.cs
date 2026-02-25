using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单据状态基本资料
/// </summary>
[SugarTable("ODK_SRM_Status")]
public class OdkSrmStatus : BaseEntity
{
    /// <summary>
    /// 状态名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;
}
