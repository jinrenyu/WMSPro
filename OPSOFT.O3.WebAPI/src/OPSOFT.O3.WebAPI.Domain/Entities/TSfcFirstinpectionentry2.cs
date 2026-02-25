using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序首检表体-附件
/// </summary>
[SugarTable("T_SFC_FIRSTINPECTIONENTRY2")]
public class TSfcFirstinpectionentry2 : BaseEntity
{
    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

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
