using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 序列号导入记录
/// </summary>
[SugarTable("T_BD_SNIMPORT")]
public class TBdSnimport : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 导入结果
    /// </summary>
    [SugarColumn(ColumnName = "FRESULT")]
    public string Fresult { get; set; } = string.Empty;

    /// <summary>
    /// 已入库条码更新库存
    /// </summary>
    [SugarColumn(ColumnName = "FISADDQTY")]
    public bool Fisaddqty { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
