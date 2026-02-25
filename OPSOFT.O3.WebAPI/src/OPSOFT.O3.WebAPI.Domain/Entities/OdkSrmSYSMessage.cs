using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统消息
/// </summary>
[SugarTable("ODK_SRM_SYSMessage")]
public class OdkSrmSYSMessage : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 用户内码
    /// </summary>
    [SugarColumn(ColumnName = "FUSERID")]
    public string Fuserid { get; set; } = string.Empty;

    /// <summary>
    /// 单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FKEY")]
    public string Fkey { get; set; } = string.Empty;

    /// <summary>
    /// 写入功能编号
    /// </summary>
    [SugarColumn(ColumnName = "FPRGKEY")]
    public string Fprgkey { get; set; } = string.Empty;

    /// <summary>
    /// 功能按钮
    /// </summary>
    [SugarColumn(ColumnName = "FFUNCTION")]
    public string Ffunction { get; set; } = string.Empty;

    /// <summary>
    /// 读取功能编号
    /// </summary>
    [SugarColumn(ColumnName = "FREADPRGKEY")]
    public string Freadprgkey { get; set; } = string.Empty;

    /// <summary>
    /// 读取单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPE")]
    public string Fbilltype { get; set; } = string.Empty;

    /// <summary>
    /// 可看类型
    /// </summary>
    [SugarColumn(ColumnName = "FVISIBLTYPE")]
    public string Fvisibltype { get; set; } = string.Empty;

    /// <summary>
    /// 是否跳转
    /// </summary>
    [SugarColumn(ColumnName = "FISSKIP")]
    public bool Fisskip { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESC")]
    public string Fdesc { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD")]
    public int Fhasread { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME")]
    public DateTime? Fhasreadtime { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
