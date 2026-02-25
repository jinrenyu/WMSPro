using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单据编码规则表体档
/// </summary>
[SugarTable("SYS_LISTCODEENTRY")]
public class SysListCodeEntry : BaseEntity
{
    /// <summary>
    /// 规则代码
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSTYPEID")]
    public string Fclasstypeid { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;
}
