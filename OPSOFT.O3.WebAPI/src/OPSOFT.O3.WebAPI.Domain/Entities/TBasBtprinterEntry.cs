using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 蓝牙打印机配置表体（元素类型）
/// </summary>
[SugarTable("T_BAS_BTPRINTER_ENTRY")]
public class TBasBtprinterEntry : BaseEntity
{
    /// <summary>
    /// 绑定字段
    /// </summary>
    [SugarColumn(ColumnName = "FBINDNAME")]
    public string Fbindname { get; set; } = string.Empty;

    /// <summary>
    /// X左侧开始位置
    /// </summary>
    [SugarColumn(ColumnName = "FXDATA")]
    public decimal Fxdata { get; set; }

    /// <summary>
    /// Y顶部开始位置
    /// </summary>
    [SugarColumn(ColumnName = "FYDATA")]
    public decimal Fydata { get; set; }

    /// <summary>
    /// 条码的高度
    /// </summary>
    [SugarColumn(ColumnName = "FHEIGHT")]
    public decimal Fheight { get; set; }

    /// <summary>
    /// 条码的宽度
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public decimal Fwidth { get; set; }

    /// <summary>
    /// 条码的旋转角度，顺时针0-360
    /// </summary>
    [SugarColumn(ColumnName = "FROTATION")]
    public decimal Frotation { get; set; }

    /// <summary>
    /// 窄条码线的高度
    /// </summary>
    [SugarColumn(ColumnName = "FNARROW")]
    public decimal Fnarrow { get; set; }

    /// <summary>
    /// 窄条码线的宽度
    /// </summary>
    [SugarColumn(ColumnName = "FWIDE")]
    public decimal Fwide { get; set; }

    /// <summary>
    /// 元素类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// X方向放大倍率
    /// </summary>
    [SugarColumn(ColumnName = "FXMULTIPLICATION")]
    public decimal Fxmultiplication { get; set; }

    /// <summary>
    /// Y方向放大倍率
    /// </summary>
    [SugarColumn(ColumnName = "FYMULTIPLICATION")]
    public decimal Fymultiplication { get; set; }

    /// <summary>
    /// 手动/自动编码
    /// </summary>
    [SugarColumn(ColumnName = "FMODE")]
    public bool Fmode { get; set; }

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
