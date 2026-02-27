using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 根据FODKDB来存储表视图名称信息
/// </summary>
[SugarTable("T_BD_SynDataConnectEntry")]
public class TBdSynDataConnectEntry : BaseEntryEntity
{
    /// <summary>
    /// 表结构内码(源表结构代码)
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID")]
    public string FTableId { get; set; } = string.Empty;

    /// <summary>
    /// 源表结构名称
    /// </summary>
    [SugarColumn(ColumnName = "FDATANAME")]
    public string FDataName { get; set; } = string.Empty;

    /// <summary>
    /// 源表结构描述
    /// </summary>
    [SugarColumn(ColumnName = "FDATADES")]
    public string FDataDes { get; set; } = string.Empty;

    /// <summary>
    /// 源表内码字段(唯一字段)
    /// </summary>
    [SugarColumn(ColumnName = "FDATAKEY")]
    public string FDataKey { get; set; } = string.Empty;

    /// <summary>
    /// U=表;V=视图
    /// </summary>
    [SugarColumn(ColumnName = "XTYPE")]
    public string XType { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }
}
