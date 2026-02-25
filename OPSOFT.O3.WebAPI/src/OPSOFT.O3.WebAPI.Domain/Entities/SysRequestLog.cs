using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// API请求日志表
/// </summary>
[SugarTable("SYS_REQUESTLOG")]
public class SysRequestLog : BaseEntity
{
    /// <summary>
    /// HTTP方法
    /// </summary>
    [SugarColumn(ColumnName = "FMETHOD")]
    public string Fmethod { get; set; } = string.Empty;

    /// <summary>
    /// 请求路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

    /// <summary>
    /// 查询字符串
    /// </summary>
    [SugarColumn(ColumnName = "FQUERYSTRING")]
    public string Fquerystring { get; set; } = string.Empty;

    /// <summary>
    /// HTTP状态码
    /// </summary>
    [SugarColumn(ColumnName = "FSTATUSCODE")]
    public int Fstatuscode { get; set; }

    /// <summary>
    /// 响应耗时(ms)
    /// </summary>
    [SugarColumn(ColumnName = "FELAPSEDMS")]
    public long Felapsedms { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    [SugarColumn(ColumnName = "FIP")]
    public string Fip { get; set; } = string.Empty;

    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// User-Agent
    /// </summary>
    [SugarColumn(ColumnName = "FUSERAGENT")]
    public string Fuseragent { get; set; } = string.Empty;

    /// <summary>
    /// 关联ID
    /// </summary>
    [SugarColumn(ColumnName = "FCORRELATIONID")]
    public string Fcorrelationid { get; set; } = string.Empty;

    /// <summary>
    /// 请求时间
    /// </summary>
    [SugarColumn(ColumnName = "FREQUESTTIME")]
    public DateTime? Frequesttime { get; set; }

    /// <summary>
    /// 响应体（截断存储）
    /// </summary>
    [SugarColumn(ColumnName = "FRESPONSEBODY", Length = 4096)]
    public string Fresponsebody { get; set; } = string.Empty;
}
