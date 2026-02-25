using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商同步状态
/// </summary>
[SugarTable("ODK_SRM_SupplierSynchronize")]
public class OdkSrmSupplierSynchronize : BaseEntity
{
    /// <summary>
    /// 供应商同步状态名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;
}
