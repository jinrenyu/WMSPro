using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 同步源数据库表字段信息
/// </summary>
[SugarTable("T_BD_ERPFieldDescription")]
public class TBdERPFieldDescription : BaseEntity
{
    /// <summary>
    /// 表结构名称
    /// </summary>
    [SugarColumn(ColumnName = "FDATANAME")]
    public string Fdataname { get; set; } = string.Empty;

    /// <summary>
    /// 源数据字段
    /// </summary>
    [SugarColumn(ColumnName = "FFIELD")]
    public string Ffield { get; set; } = string.Empty;

    /// <summary>
    /// 源数据字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDTYPE")]
    public string Ffieldtype { get; set; } = string.Empty;

    /// <summary>
    /// 源数据字段描述
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDDES")]
    public string Ffielddes { get; set; } = string.Empty;

    /// <summary>
    /// 源主键
    /// </summary>
    [SugarColumn(ColumnName = "FISFIELDKEY")]
    public bool Fisfieldkey { get; set; }

    /// <summary>
    /// S=源；A=目标
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
