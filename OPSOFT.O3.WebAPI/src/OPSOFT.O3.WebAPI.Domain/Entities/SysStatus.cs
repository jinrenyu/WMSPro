using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单据状态基本资料
/// </summary>
[SugarTable("SYS_STATUS")]
public class SysStatus : BaseEntity
{
    /// <summary>
    /// 状态代码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public int Fitemid { get; set; }

    /// <summary>
    /// 状态名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统默认
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }
}
