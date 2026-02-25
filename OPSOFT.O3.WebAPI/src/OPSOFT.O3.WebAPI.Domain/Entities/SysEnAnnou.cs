using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 企业新闻公告
/// </summary>
[SugarTable("SYS_ENANNOU")]
public class SysEnAnnou : BaseEntity
{
    /// <summary>
    /// 公告主题
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 公告内容
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 是否外部公开
    /// </summary>
    [SugarColumn(ColumnName = "FISOUTALL")]
    public bool Fisoutall { get; set; }

    /// <summary>
    /// 是否内部公开
    /// </summary>
    [SugarColumn(ColumnName = "FISINALL")]
    public bool Fisinall { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 新闻公告图片
    /// </summary>
    [SugarColumn(ColumnName = "FIMAGE")]
    public byte[]? Fimage { get; set; }

    /// <summary>
    /// 发布人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 发布日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审批状态
    /// </summary>
    [SugarColumn(ColumnName = "FOASTATUS")]
    public string Foastatus { get; set; } = string.Empty;

    /// <summary>
    /// 审批结果
    /// </summary>
    [SugarColumn(ColumnName = "FOARESULT")]
    public string Foaresult { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
