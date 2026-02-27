using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单据编码规则表体档
/// </summary>
[SugarTable("SYS_BATCHCODEENTRY")]
public class SysBatchCodeEntry : BaseEntity
{
    /// <summary>
    /// 规则代码
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSTYPEID")]
    public string Fclasstypeid { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 编码对照表
    /// </summary>
    [SugarColumn(ColumnName = "FCODECONTRAST", IsNullable = true)]
    public string FCODECONTRAST { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 元素来源字段
    /// </summary>
    [SugarColumn(ColumnName = "FIELDID", IsNullable = true)]
    public string FIELDID { get; set; } = string.Empty;

    /// <summary>
    /// L=左边补位;R=右边补位
    /// </summary>
    [SugarColumn(ColumnName = "FCHARALIGNMENT", IsNullable = true)]
    public string FCHARALIGNMENT { get; set; } = string.Empty;

    /// <summary>
    /// 长度
    /// </summary>
    [SugarColumn(ColumnName = "FLENGHT", IsNullable = true)]
    public int? FLENGHT { get; set; }

    /// <summary>
    /// 编码依据
    /// </summary>
    [SugarColumn(ColumnName = "ISSERIAL", IsNullable = true)]
    public bool? ISSERIAL { get; set; }

    /// <summary>
    /// 编码元素
    /// </summary>
    [SugarColumn(ColumnName = "ISMEMBER", IsNullable = true)]
    public bool? ISMEMBER { get; set; }

    /// <summary>
    /// 元素来源名称
    /// </summary>
    [SugarColumn(ColumnName = "FIELDNAME", IsNullable = true)]
    public string FIELDNAME { get; set; } = string.Empty;

    /// <summary>
    /// 补位符
    /// </summary>
    [SugarColumn(ColumnName = "FCHAR", IsNullable = true)]
    public string FCHAR { get; set; } = string.Empty;

    /// <summary>
    /// 字串时 可选 L=小写 U =大写 及日期型格式
    /// </summary>
    [SugarColumn(ColumnName = "FORMATSTRING", IsNullable = true)]
    public string FORMATSTRING { get; set; } = string.Empty;

    /// <summary>
    /// 最大流水号(0表示不限制)
    /// </summary>
    [SugarColumn(ColumnName = "FMAX", IsNullable = true)]
    public int? FMAX { get; set; }

    /// <summary>
    /// 起始流水号
    /// </summary>
    [SugarColumn(ColumnName = "FMIN", IsNullable = true)]
    public int? FMIN { get; set; }

    /// <summary>
    /// 流水号步长
    /// </summary>
    [SugarColumn(ColumnName = "FSTEP", IsNullable = true)]
    public int? FSTEP { get; set; }

    /// <summary>
    /// 设置值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE", IsNullable = true)]
    public string FVALUE { get; set; } = string.Empty;
}
