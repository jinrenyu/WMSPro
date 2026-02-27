using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 字段类型表
/// </summary>
[SugarTable("SYS_FIELDTYPE")]
public class SysFieldType : BaseEntity
{
    /// <summary>
    /// 类型代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 类型名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 类型名称(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;

    /// <summary>
    /// 字段长度
    /// </summary>
    [SugarColumn(ColumnName = "FLENGTH", IsNullable = true)]
    public int? FLENGTH { get; set; }

    /// <summary>
    /// 类型名称(第二语言)
    /// </summary>
    [SugarColumn(ColumnName = "FTNAME", IsNullable = true)]
    public string FTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人员
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 字段备注
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDNOTE", IsNullable = true)]
    public string FFIELDNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDTYPE", IsNullable = true)]
    public string FFIELDTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 类型名称(第三语言)
    /// </summary>
    [SugarColumn(ColumnName = "FENAME", IsNullable = true)]
    public string FENAME { get; set; } = string.Empty;

    /// <summary>
    /// 小数位数
    /// </summary>
    [SugarColumn(ColumnName = "FSCALE", IsNullable = true)]
    public int? FSCALE { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }
}
