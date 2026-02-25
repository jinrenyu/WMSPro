using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验单测试明细
/// </summary>
[SugarTable("T_BD_QM_CL_ITEM_ITEMS")]
public class TBdQmClItemItems : BaseEntity
{
    /// <summary>
    /// 检验项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCITEMID")]
    public string FQcitemid { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCTOOLID")]
    public string FQctoolid { get; set; } = string.Empty;

    /// <summary>
    /// 检验序号
    /// </summary>
    [SugarColumn(ColumnName = "FQCSEQ")]
    public string FQcseq { get; set; } = string.Empty;

    /// <summary>
    /// 条码【ID】
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string FBarcode { get; set; } = string.Empty;

    /// <summary>
    /// 测试值
    /// </summary>
    [SugarColumn(ColumnName = "FQCVALUE")]
    public decimal FQcvalue { get; set; }

    /// <summary>
    /// 测试结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string FQcresult { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 抽样次数
    /// </summary>
    [SugarColumn(ColumnName = "FQCTIMES")]
    public decimal FQctimes { get; set; }

    /// <summary>
    /// 测试次数
    /// </summary>
    [SugarColumn(ColumnName = "FQCCOUNT")]
    public decimal FQccount { get; set; }

    /// <summary>
    /// 下阈值
    /// </summary>
    [SugarColumn(ColumnName = "FMINIMUM")]
    public decimal FMinimum { get; set; }

    /// <summary>
    /// 上阈值
    /// </summary>
    [SugarColumn(ColumnName = "FMAXIMUM")]
    public decimal FMaximum { get; set; }

    /// <summary>
    /// 系统检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FSYSQCRESULT")]
    public string FSysqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 检验单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string FUnitid { get; set; } = string.Empty;

    /// <summary>
    /// 测试单位
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLUNITID")]
    public string FToolunitid { get; set; } = string.Empty;

    /// <summary>
    /// 检验值
    /// </summary>
    [SugarColumn(ColumnName = "FJYVALUE")]
    public decimal FJyvalue { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int FEntryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string FBodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string FDetailid { get; set; } = string.Empty;
}
