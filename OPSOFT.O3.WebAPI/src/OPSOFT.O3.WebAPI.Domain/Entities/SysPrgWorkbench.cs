using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 我的收藏
/// </summary>
[SugarTable("SYS_PRGWORKBENCH")]
public class SysPrgWorkbench : BaseEntity
{
    /// <summary>
    /// 用户帐号
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 明细功能ID
    /// </summary>
    [SugarColumn(ColumnName = "FID")]
    public string Fid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
