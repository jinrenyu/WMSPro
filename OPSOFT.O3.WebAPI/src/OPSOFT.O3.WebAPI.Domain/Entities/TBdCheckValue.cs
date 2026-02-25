using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验值
/// </summary>
[SugarTable("T_BD_CHECKVALUE")]
public class TBdCheckvalue : BaseEntity
{
    /// <summary>
    /// 检验值编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE")]
    public string Fvalue { get; set; } = string.Empty;
}
