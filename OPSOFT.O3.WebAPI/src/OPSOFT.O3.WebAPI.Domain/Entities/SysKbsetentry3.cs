using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 看板配置表-图表信息-系列明细
/// </summary>
[SugarTable("SYS_KBSETENTRY3")]
public class SysKbsetentry3 : BaseEntity
{
    /// <summary>
    /// 面板内码
    /// </summary>
    [SugarColumn(ColumnName = "FPANELID")]
    public string Fpanelid { get; set; } = string.Empty;

    /// <summary>
    /// 系列标题
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTION")]
    public string Fcaption { get; set; } = string.Empty;

    /// <summary>
    /// X坐标字段
    /// </summary>
    [SugarColumn(ColumnName = "FXCATEGORY")]
    public string Fxcategory { get; set; } = string.Empty;

    /// <summary>
    /// Y坐标字段
    /// </summary>
    [SugarColumn(ColumnName = "FYVALUE")]
    public string Fyvalue { get; set; } = string.Empty;

    /// <summary>
    /// Y坐标最大值
    /// </summary>
    [SugarColumn(ColumnName = "FYMAX")]
    public decimal Fymax { get; set; }

    /// <summary>
    /// Y坐标最小值
    /// </summary>
    [SugarColumn(ColumnName = "FYMIN")]
    public decimal Fymin { get; set; }

    /// <summary>
    /// Y坐标格式
    /// </summary>
    [SugarColumn(ColumnName = "FYFORMAT")]
    public string Fyformat { get; set; } = string.Empty;

    /// <summary>
    /// 系列图类别
    /// </summary>
    [SugarColumn(ColumnName = "FMAPPINTTYPE")]
    public string Fmappinttype { get; set; } = string.Empty;

    /// <summary>
    /// 显示宽度
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public decimal Fwidth { get; set; }

    /// <summary>
    /// 元素颜色
    /// </summary>
    [SugarColumn(ColumnName = "FCOLOR")]
    public string Fcolor { get; set; } = string.Empty;

    /// <summary>
    /// 字体大小
    /// </summary>
    [SugarColumn(ColumnName = "FFONTSIZE")]
    public decimal Ffontsize { get; set; }

    /// <summary>
    /// 显示格式
    /// </summary>
    [SugarColumn(ColumnName = "FFORMAT")]
    public string Fformat { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
