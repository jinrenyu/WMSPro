using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 企业新闻公告接收人
/// </summary>
[SugarTable("SYS_ENANNOUENTRY")]
public class SysEnAnnouEntry : BaseEntity
{
    /// <summary>
    /// 接收对象类别(1=内部组织;2=用户;3=客户;4=厂商
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 接收对象内码或帐号
    /// </summary>
    [SugarColumn(ColumnName = "FPERSONID", IsNullable = true)]
    public string FPERSONID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }
}
