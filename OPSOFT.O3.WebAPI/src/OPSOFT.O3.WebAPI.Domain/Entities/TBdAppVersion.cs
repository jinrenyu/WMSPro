using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// App版本信息
/// </summary>
[SugarTable("T_BD_APPVERSION")]
public class TBdAppversion : BaseEntity
{
    /// <summary>
    /// app版本号
    /// </summary>
    [SugarColumn(ColumnName = "FVERSIONCODE")]
    public string Fversioncode { get; set; } = string.Empty;

    /// <summary>
    /// app版本更新描述
    /// </summary>
    [SugarColumn(ColumnName = "FVERSIONUPDATEDESC")]
    public string Fversionupdatedesc { get; set; } = string.Empty;

    /// <summary>
    /// app对应版本下载地址
    /// </summary>
    [SugarColumn(ColumnName = "FVERSIONDOWNLOADURL")]
    public string Fversiondownloadurl { get; set; } = string.Empty;

    /// <summary>
    /// app类型
    /// </summary>
    [SugarColumn(ColumnName = "FAPPTYPE")]
    public int Fapptype { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
