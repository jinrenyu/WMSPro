using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// MES检验单检验项目
/// </summary>
[SugarTable("T_SFC_QUALITY_ENTRY")]
public class TSfcQualityEntry : BaseEntity
{
    /// <summary>
    /// 检验项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCITEMID")]
    public string Fqcitemid { get; set; } = string.Empty;

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FQCLEV")]
    public string Fqclev { get; set; } = string.Empty;

    /// <summary>
    /// 抽样数量
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEQTY")]
    public int Fsampleqty { get; set; }

    /// <summary>
    /// AC
    /// </summary>
    [SugarColumn(ColumnName = "FAC")]
    public decimal Fac { get; set; }

    /// <summary>
    /// RE
    /// </summary>
    [SugarColumn(ColumnName = "FRE")]
    public decimal Fre { get; set; }

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL")]
    public string Faql { get; set; } = string.Empty;

    /// <summary>
    /// 测试方法
    /// </summary>
    [SugarColumn(ColumnName = "FQCMETHOD")]
    public string Fqcmethod { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准
    /// </summary>
    [SugarColumn(ColumnName = "FQCSTD")]
    public string Fqcstd { get; set; } = string.Empty;

    /// <summary>
    /// 测试次数
    /// </summary>
    [SugarColumn(ColumnName = "FQCCOUNT")]
    public int Fqccount { get; set; }

    /// <summary>
    /// 缺陷数
    /// </summary>
    [SugarColumn(ColumnName = "FQCNGQTY")]
    public int Fqcngqty { get; set; }

    /// <summary>
    /// 测试值
    /// </summary>
    [SugarColumn(ColumnName = "FQCVALUE")]
    public string Fqcvalue { get; set; } = string.Empty;

    /// <summary>
    /// 人工检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string Fqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具分组内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCTOOLID")]
    public string Fqctoolid { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具内码
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLID")]
    public string Ftoolid { get; set; } = string.Empty;

    /// <summary>
    /// 标准值
    /// </summary>
    [SugarColumn(ColumnName = "FSTDATA")]
    public string Fstdata { get; set; } = string.Empty;

    /// <summary>
    /// 系统检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FSYSQCRESULT")]
    public string Fsysqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 是否是破坏检
    /// </summary>
    [SugarColumn(ColumnName = "FDESTORYTEST")]
    public bool Fdestorytest { get; set; }

    /// <summary>
    /// 严格度
    /// </summary>
    [SugarColumn(ColumnName = "FSTRINGENCY")]
    public string Fstringency { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目人
    /// </summary>
    [SugarColumn(ColumnName = "FINSPERSONID")]
    public string Finspersonid { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSDATE")]
    public DateTime? Finsdate { get; set; }

    /// <summary>
    /// 检验标准内码
    /// </summary>
    [SugarColumn(ColumnName = "FQCSTDID")]
    public string Fqcstdid { get; set; } = string.Empty;

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
