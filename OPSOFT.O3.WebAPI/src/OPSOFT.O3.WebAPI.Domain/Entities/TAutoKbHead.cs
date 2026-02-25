using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 自定义看板表头
/// </summary>
[SugarTable("T_AUTOKB_HEAD")]
public class TAutoKbHead : BaseEntity
{
    /// <summary>
    /// 看板名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 看板样式
    /// </summary>
    [SugarColumn(ColumnName = "FSTYLE")]
    public string Fstyle { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用
    /// </summary>
    [SugarColumn(ColumnName = "FISUSED")]
    public bool Fisused { get; set; }

    /// <summary>
    /// 看板中公共信息
    /// </summary>
    [SugarColumn(ColumnName = "FCANVASPUBLIC")]
    public string Fcanvaspublic { get; set; } = string.Empty;

    /// <summary>
    /// 缩略图
    /// </summary>
    [SugarColumn(ColumnName = "FTHUMBNAIL")]
    public string Fthumbnail { get; set; } = string.Empty;

    /// <summary>
    /// 看板类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 是否是模板
    /// </summary>
    [SugarColumn(ColumnName = "FISTEMPLATE")]
    public bool Fistemplate { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    [SugarColumn(ColumnName = "FHEIGHT")]
    public int Fheight { get; set; }

    /// <summary>
    /// 宽
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public int Fwidth { get; set; }

    /// <summary>
    /// 分组类
    /// </summary>
    [SugarColumn(ColumnName = "FGROUPCLASS")]
    public string Fgroupclass { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
