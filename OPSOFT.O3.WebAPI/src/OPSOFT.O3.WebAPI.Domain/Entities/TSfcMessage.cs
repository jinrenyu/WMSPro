using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产消息
/// </summary>
[SugarTable("T_SFC_MESSAGE")]
public class TSfcMessage : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 主题
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 消息可见人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPIDS")]
    public string Fempids { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;
}
