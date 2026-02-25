using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 蓝牙打印机配置表体_德佟
/// </summary>
[SugarTable("T_BAS_BTPRINTER_ENTRY_DT")]
public class TBasBtprinterEntryDt : BaseEntity
{
    /// <summary>
    /// 元素类型
    /// </summary>
    [SugarColumn(ColumnName = "FELEMENTTYPE")]
    public int Felementtype { get; set; }

    /// <summary>
    /// 宽
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public decimal Fwidth { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    [SugarColumn(ColumnName = "FHEIGHT")]
    public decimal Fheight { get; set; }

    /// <summary>
    /// X
    /// </summary>
    [SugarColumn(ColumnName = "FX")]
    public decimal Fx { get; set; }

    /// <summary>
    /// Y
    /// </summary>
    [SugarColumn(ColumnName = "FY")]
    public decimal Fy { get; set; }

    /// <summary>
    /// 旋转角度
    /// </summary>
    [SugarColumn(ColumnName = "FORIENTATION")]
    public decimal Forientation { get; set; }

    /// <summary>
    /// 绑定字段
    /// </summary>
    [SugarColumn(ColumnName = "FBINDINGFIELD")]
    public string Fbindingfield { get; set; } = string.Empty;

    /// <summary>
    /// 字体
    /// </summary>
    [SugarColumn(ColumnName = "FFONTSTYLE")]
    public int Ffontstyle { get; set; }

    /// <summary>
    /// 字体大小
    /// </summary>
    [SugarColumn(ColumnName = "FFONTHEIGHT")]
    public decimal Ffontheight { get; set; }

    /// <summary>
    /// 线宽
    /// </summary>
    [SugarColumn(ColumnName = "FLINEWIDTH")]
    public decimal Flinewidth { get; set; }

    /// <summary>
    /// 文本高度
    /// </summary>
    [SugarColumn(ColumnName = "FTEXTHEIGHT")]
    public decimal Ftextheight { get; set; }

    /// <summary>
    /// 自动返回
    /// </summary>
    [SugarColumn(ColumnName = "FAUTORETURN")]
    public bool Fautoreturn { get; set; }

    /// <summary>
    /// 是否启用默认值
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULTVALUE")]
    public bool Fisdefaultvalue { get; set; }

    /// <summary>
    /// 字段值
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDVALUE")]
    public string Ffieldvalue { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用文本截取
    /// </summary>
    [SugarColumn(ColumnName = "FISINTERCEPTTEXT")]
    public bool Fisintercepttext { get; set; }

    /// <summary>
    /// 文本截取长度
    /// </summary>
    [SugarColumn(ColumnName = "FINTERCEPTLENTH")]
    public int Finterceptlenth { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
