using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP终端工位
/// </summary>
[SugarTable("T_BD_ESOPSTATION")]
public class TBdEsopstation : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 背景图片地址
    /// </summary>
    [SugarColumn(ColumnName = "FBACKADDRESS")]
    public string Fbackaddress { get; set; } = string.Empty;

    /// <summary>
    /// 使用状态
    /// </summary>
    [SugarColumn(ColumnName = "FUSESTATUS")]
    public bool Fusestatus { get; set; }

    /// <summary>
    /// 屏幕放置方式
    /// </summary>
    [SugarColumn(ColumnName = "FSTYLE")]
    public int Fstyle { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 自动翻页间隔时间
    /// </summary>
    [SugarColumn(ColumnName = "FINTERVAL")]
    public decimal Finterval { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [SugarColumn(ColumnName = "FIPADDRESS")]
    public string Fipaddress { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 最后在线时间
    /// </summary>
    [SugarColumn(ColumnName = "FLASTTIME")]
    public DateTime? Flasttime { get; set; }

    /// <summary>
    /// 在线状态
    /// </summary>
    [SugarColumn(ColumnName = "FONLINESTATUS")]
    public bool Fonlinestatus { get; set; }

    /// <summary>
    /// 几分屏
    /// </summary>
    [SugarColumn(ColumnName = "FSCREENSTYLE")]
    public int Fscreenstyle { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
