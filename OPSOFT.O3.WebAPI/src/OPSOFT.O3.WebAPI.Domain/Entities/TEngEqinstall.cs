using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备安装
/// </summary>
[SugarTable("T_ENG_EQINSTALL")]
public class TEngEqinstall : BaseEntity
{
    /// <summary>
    /// 开箱验收单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID")]
    public string Fsourceinterid { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 安装位置
    /// </summary>
    [SugarColumn(ColumnName = "FPLACE")]
    public string Fplace { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 设备型号
    /// </summary>
    [SugarColumn(ColumnName = "FEQMODEL")]
    public string Feqmodel { get; set; } = string.Empty;

    /// <summary>
    /// 设备规格
    /// </summary>
    [SugarColumn(ColumnName = "FEQSIZE")]
    public string Feqsize { get; set; } = string.Empty;

    /// <summary>
    /// 出厂编号
    /// </summary>
    [SugarColumn(ColumnName = "FFANUMBER")]
    public string Ffanumber { get; set; } = string.Empty;

    /// <summary>
    /// 出厂日期
    /// </summary>
    [SugarColumn(ColumnName = "FFADATE")]
    public DateTime? Ffadate { get; set; }

    /// <summary>
    /// 外形尺寸
    /// </summary>
    [SugarColumn(ColumnName = "FBODIMENSION")]
    public string Fbodimension { get; set; } = string.Empty;

    /// <summary>
    /// 重量(T
    /// </summary>
    [SugarColumn(ColumnName = "FWEIGHT")]
    public decimal Fweight { get; set; }
}
