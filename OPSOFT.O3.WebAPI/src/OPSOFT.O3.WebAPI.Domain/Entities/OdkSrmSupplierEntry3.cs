using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商上传附件信息
/// </summary>
[SugarTable("ODK_SRM_SupplierEntry3")]
public class OdkSrmSupplierEntry3 : BaseEntity
{
    /// <summary>
    /// 文件名
    /// </summary>
    [SugarColumn(ColumnName = "FATTCHMENTNAME")]
    public string Fattchmentname { get; set; } = string.Empty;

    /// <summary>
    /// 附件类型
    /// </summary>
    [SugarColumn(ColumnName = "FATTCHMENTTYPE")]
    public string Fattchmenttype { get; set; } = string.Empty;

    /// <summary>
    /// 操作
    /// </summary>
    [SugarColumn(ColumnName = "FATTCHMENTOP")]
    public string Fattchmentop { get; set; } = string.Empty;

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
