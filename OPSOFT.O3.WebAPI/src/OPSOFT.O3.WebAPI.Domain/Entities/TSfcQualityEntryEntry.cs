using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// MES检验单测试明细
/// </summary>
[SugarTable("T_SFC_QUALITY_ENTRY_ENTRY")]
public class TSfcQualityEntryEntry : BaseEntity
{
    /// <summary>
    /// 检验项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCITEMID")]
    public string Fqcitemid { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCTOOLID")]
    public string Fqctoolid { get; set; } = string.Empty;

    /// <summary>
    /// 检验序号
    /// </summary>
    [SugarColumn(ColumnName = "FQCSEQ")]
    public string Fqcseq { get; set; } = string.Empty;

    /// <summary>
    /// 条码【ID】
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 测试值
    /// </summary>
    [SugarColumn(ColumnName = "FQCVALUE")]
    public string Fqcvalue { get; set; } = string.Empty;

    /// <summary>
    /// 测试结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string Fqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 抽样次数
    /// </summary>
    [SugarColumn(ColumnName = "FQCTIMES")]
    public decimal Fqctimes { get; set; }

    /// <summary>
    /// 测试次数
    /// </summary>
    [SugarColumn(ColumnName = "FQCCOUNT")]
    public decimal Fqccount { get; set; }

    /// <summary>
    /// 下阈值
    /// </summary>
    [SugarColumn(ColumnName = "FMINIMUM")]
    public decimal Fminimum { get; set; }

    /// <summary>
    /// 上阈值
    /// </summary>
    [SugarColumn(ColumnName = "FMAXIMUM")]
    public decimal Fmaximum { get; set; }

    /// <summary>
    /// 系统检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FSYSQCRESULT")]
    public string Fsysqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 检验单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 测试单位
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLUNITID")]
    public string Ftoolunitid { get; set; } = string.Empty;

    /// <summary>
    /// 检验值
    /// </summary>
    [SugarColumn(ColumnName = "FJYVALUE")]
    public string Fjyvalue { get; set; } = string.Empty;

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
