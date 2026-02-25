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
}
