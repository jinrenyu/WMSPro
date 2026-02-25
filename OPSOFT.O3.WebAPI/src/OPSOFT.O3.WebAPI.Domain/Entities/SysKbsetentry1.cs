using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 看板配置表-面板信息
/// </summary>
[SugarTable("SYS_KBSETENTRY1")]
public class SysKbsetentry1 : BaseEntity
{
    /// <summary>
    /// 面板所在行(从零开始
    /// </summary>
    [SugarColumn(ColumnName = "FROW")]
    public int Frow { get; set; }
}
