using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 看板配置表-表头
/// </summary>
[SugarTable("SYS_KBSET")]
public class SysKbSet : BaseEntity
{
    /// <summary>
    /// 看板说明
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 看板标题高度
    /// </summary>
    [SugarColumn(ColumnName = "FTITLEHEIGHT")]
    public decimal Ftitleheight { get; set; }

    /// <summary>
    /// 是否显示主标题
    /// </summary>
    [SugarColumn(ColumnName = "FHASTITLE")]
    public bool Fhastitle { get; set; }

    /// <summary>
    /// 是否显示副标题
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWSUBTITLE")]
    public bool Fshowsubtitle { get; set; }

    /// <summary>
    /// 是否显示时间
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWTIME")]
    public bool Fshowtime { get; set; }

    /// <summary>
    /// 主标题
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 副标题
    /// </summary>
    [SugarColumn(ColumnName = "FSUBTITLE")]
    public string Fsubtitle { get; set; } = string.Empty;

    /// <summary>
    /// 副标题字体大小
    /// </summary>
    [SugarColumn(ColumnName = "FSFONTSIZE")]
    public int Fsfontsize { get; set; }

    /// <summary>
    /// 主标题字体大小
    /// </summary>
    [SugarColumn(ColumnName = "FMFONTSIZE")]
    public int Fmfontsize { get; set; }

    /// <summary>
    /// 看板刷新频率(秒
    /// </summary>
    [SugarColumn(ColumnName = "FPERIOD")]
    public int Fperiod { get; set; }

    /// <summary>
    /// 副标题宽
    /// </summary>
    [SugarColumn(ColumnName = "FSUBTITLEWIDHT", IsNullable = true)]
    public decimal? FSUBTITLEWIDHT { get; set; }

    /// <summary>
    /// 看板表格布局列数
    /// </summary>
    [SugarColumn(ColumnName = "FCOLUMN", IsNullable = true)]
    public int? FCOLUMN { get; set; }

    /// <summary>
    /// 看板内柱状图最大宽度
    /// </summary>
    [SugarColumn(ColumnName = "FBARMAXWIDTH", IsNullable = true)]
    public decimal? FBARMAXWIDTH { get; set; }

    /// <summary>
    /// 标题右边控件
    /// </summary>
    [SugarColumn(ColumnName = "FRIGHTCONTROL", IsNullable = true)]
    public string FRIGHTCONTROL { get; set; } = string.Empty;

    /// <summary>
    /// 显示分隔线
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWLINE", IsNullable = true)]
    public bool? FSHOWLINE { get; set; }

    /// <summary>
    /// 标题左边控件
    /// </summary>
    [SugarColumn(ColumnName = "FLEFTCONTROL", IsNullable = true)]
    public string FLEFTCONTROL { get; set; } = string.Empty;

    /// <summary>
    /// 看板表格布局行数
    /// </summary>
    [SugarColumn(ColumnName = "FROW", IsNullable = true)]
    public int? FROW { get; set; }

    /// <summary>
    /// 手机展示
    /// </summary>
    [SugarColumn(ColumnName = "FISPHONE", IsNullable = true)]
    public bool? FISPHONE { get; set; }

    /// <summary>
    /// 主标题宽
    /// </summary>
    [SugarColumn(ColumnName = "FTITLEWIDHT", IsNullable = true)]
    public decimal? FTITLEWIDHT { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 横屏展示
    /// </summary>
    [SugarColumn(ColumnName = "FISLANDSCAPE", IsNullable = true)]
    public bool? FISLANDSCAPE { get; set; }
}
