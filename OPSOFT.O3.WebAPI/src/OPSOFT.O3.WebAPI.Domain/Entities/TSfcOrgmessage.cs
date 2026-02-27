using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 组织消息推送配置
/// </summary>
[SugarTable("T_SFC_ORGMESSAGE")]
public class TSfcOrgmessage : BaseEntity
{
    /// <summary>
    /// 组织ID
    /// </summary>
    [SugarColumn(ColumnName = "FORGID")]
    public string Forgid { get; set; } = string.Empty;

    /// <summary>
    /// 企业ID
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPID")]
    public string Fcompid { get; set; } = string.Empty;

    /// <summary>
    /// Serect
    /// </summary>
    [SugarColumn(ColumnName = "FSERECT")]
    public string Fserect { get; set; } = string.Empty;

    /// <summary>
    /// 发送人邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FSENDMAIL")]
    public string Fsendmail { get; set; } = string.Empty;

    /// <summary>
    /// 发送人账号
    /// </summary>
    [SugarColumn(ColumnName = "FSENDID")]
    public string Fsendid { get; set; } = string.Empty;

    /// <summary>
    /// 发送人密码
    /// </summary>
    [SugarColumn(ColumnName = "FSENDPSD")]
    public string Fsendpsd { get; set; } = string.Empty;

    /// <summary>
    /// 发送服务器
    /// </summary>
    [SugarColumn(ColumnName = "FSENDSERVER")]
    public string Fsendserver { get; set; } = string.Empty;

    /// <summary>
    /// 发送服务器端口
    /// </summary>
    [SugarColumn(ColumnName = "FSENDSERVERPORT")]
    public string Fsendserverport { get; set; } = string.Empty;

    /// <summary>
    /// KEY(钉钉
    /// </summary>
    [SugarColumn(ColumnName = "FKEY")]
    public string Fkey { get; set; } = string.Empty;

    /// <summary>
    /// API(手环)
    /// </summary>
    [SugarColumn(ColumnName = "FAPI", IsNullable = true)]
    public string FAPI { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER", IsNullable = true)]
    public string FNUMBER { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }
}
