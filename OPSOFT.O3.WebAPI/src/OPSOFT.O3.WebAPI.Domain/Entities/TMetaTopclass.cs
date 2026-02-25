using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 业务领域
/// </summary>
[SugarTable("T_META_TOPCLASS")]
public class TMetaTopclass : BaseEntity
{
    /// <summary>
    /// 显示顺序
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public int Fseq { get; set; }

    /// <summary>
    /// 可见性
    /// </summary>
    [SugarColumn(ColumnName = "FVISIBLE")]
    public int Fvisible { get; set; }

    /// <summary>
    /// 系统预设
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 提示
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLTIPS")]
    public string Ftooltips { get; set; } = string.Empty;
}
