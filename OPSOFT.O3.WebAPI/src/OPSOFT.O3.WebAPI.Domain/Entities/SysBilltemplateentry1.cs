using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 表单数据源相关表信息
/// </summary>
[SugarTable("SYS_BILLTEMPLATEENTRY1")]
public class SysBilltemplateentry1 : BaseEntity
{
    /// <summary>
    /// 父阶内码
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTID")]
    public string Fparentid { get; set; } = string.Empty;

    /// <summary>
    /// 子表内码
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID")]
    public string Ftableid { get; set; } = string.Empty;

    /// <summary>
    /// 数据表层级
    /// </summary>
    [SugarColumn(ColumnName = "FLEV")]
    public int Flev { get; set; }

    /// <summary>
    /// 关系内码
    /// </summary>
    [SugarColumn(ColumnName = "FREID")]
    public string Freid { get; set; } = string.Empty;

    /// <summary>
    /// 关系路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

    /// <summary>
    /// 子表代码
    /// </summary>
    [SugarColumn(ColumnName = "FSUBNAME")]
    public string Fsubname { get; set; } = string.Empty;

    /// <summary>
    /// 子表别名
    /// </summary>
    [SugarColumn(ColumnName = "FSUBNAMEAS")]
    public string Fsubnameas { get; set; } = string.Empty;

    /// <summary>
    /// 父表代码
    /// </summary>
    [SugarColumn(ColumnName = "FPANAME")]
    public string Fpaname { get; set; } = string.Empty;

    /// <summary>
    /// 关联类型
    /// </summary>
    [SugarColumn(ColumnName = "FRETYPE")]
    public string Fretype { get; set; } = string.Empty;

    /// <summary>
    /// 父表关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FMTABLEKEY")]
    public string Fmtablekey { get; set; } = string.Empty;

    /// <summary>
    /// 子表关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FRTABLEKEY")]
    public string Frtablekey { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
