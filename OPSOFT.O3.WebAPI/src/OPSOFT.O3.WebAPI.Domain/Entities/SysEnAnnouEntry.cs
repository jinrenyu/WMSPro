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
}
