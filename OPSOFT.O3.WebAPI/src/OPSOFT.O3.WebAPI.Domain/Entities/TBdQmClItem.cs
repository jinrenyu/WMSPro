using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验单检验项目明细
/// </summary>
[SugarTable("T_BD_QM_CL_ITEM")]
public class TBdQmClItem : BaseEntryEntity
{
    /// <summary>
    /// 检验项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCITEMID")]
    public string FQcItemId { get; set; } = string.Empty;

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FQCLEV")]
    public string FQcLev { get; set; } = string.Empty;

    /// <summary>
    /// 抽样数量
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEQTY")]
    public int FSampleQty { get; set; }

    /// <summary>
    /// AC
    /// </summary>
    [SugarColumn(ColumnName = "FAC")]
    public int FAc { get; set; }

    /// <summary>
    /// RE
    /// </summary>
    [SugarColumn(ColumnName = "FRE")]
    public int FRe { get; set; }

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL")]
    public string FAql { get; set; } = string.Empty;

    /// <summary>
    /// 测试方法
    /// </summary>
    [SugarColumn(ColumnName = "FQCMETHOD")]
    public string FQcMethod { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准
    /// </summary>
    [SugarColumn(ColumnName = "FQCSTD")]
    public string FQcStd { get; set; } = string.Empty;

    /// <summary>
    /// 测试次数
    /// </summary>
    [SugarColumn(ColumnName = "FQCCOUNT")]
    public int FQcCount { get; set; }

    /// <summary>
    /// 缺陷数
    /// </summary>
    [SugarColumn(ColumnName = "FQCNGQTY")]
    public int FQcNgQty { get; set; }

    /// <summary>
    /// 测试值
    /// </summary>
    [SugarColumn(ColumnName = "FQCVALUE")]
    public string FQcValue { get; set; } = string.Empty;

    /// <summary>
    /// 人工检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string FQcResult { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具分组内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCTOOLID")]
    public string FQcToolId { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具内码
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLID")]
    public string FToolId { get; set; } = string.Empty;

    /// <summary>
    /// 标准值
    /// </summary>
    [SugarColumn(ColumnName = "FSTDATA")]
    public string FStData { get; set; } = string.Empty;

    /// <summary>
    /// 系统检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FSYSQCRESULT")]
    public string FSysQcResult { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string FUnitId { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string FMaterialId { get; set; } = string.Empty;

    /// <summary>
    /// 检验员
    /// </summary>
    [SugarColumn(ColumnName = "FQCSTAFFID")]
    public string FQcStaffId { get; set; } = string.Empty;

    /// <summary>
    /// 检验日期
    /// </summary>
    [SugarColumn(ColumnName = "FQCTIME")]
    public DateTime? FQcTime { get; set; }
}
