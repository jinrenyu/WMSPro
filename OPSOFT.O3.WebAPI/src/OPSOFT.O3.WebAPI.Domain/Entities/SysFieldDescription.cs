using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统字段定义
/// </summary>
[SugarTable("SYS_FIELDDESCRIPTION")]
public class SysFieldDescription : BaseEntity
{
    /// <summary>
    /// 数据类型内码
    /// </summary>
    [SugarColumn(ColumnName = "FDDID")]
    public string Fddid { get; set; } = string.Empty;

    /// <summary>
    /// 字段名
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDTYPE")]
    public string Ffieldtype { get; set; } = string.Empty;

    /// <summary>
    /// 字段长度
    /// </summary>
    [SugarColumn(ColumnName = "FLENGTH")]
    public int Flength { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    [SugarColumn(ColumnName = "FSCALE")]
    public int Fscale { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULTVALUE")]
    public string Fdefaultvalue { get; set; } = string.Empty;

    /// <summary>
    /// 详细描述
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 字段备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 详细描述(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;
}
