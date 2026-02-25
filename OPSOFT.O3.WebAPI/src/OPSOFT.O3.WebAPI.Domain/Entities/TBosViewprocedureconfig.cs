using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 视图、储存过程配置表
/// </summary>
[SugarTable("T_BOS_VIEWPROCEDURECONFIG")]
public class TBosViewprocedureconfig : BaseEntity
{
    /// <summary>
    /// 目标数据库 1(ERP)2(MES)
    /// </summary>
    [SugarColumn(ColumnName = "FDATASOURCE")]
    public int Fdatasource { get; set; }

    /// <summary>
    /// 数据源版本 1=金蝶K3V10.0 2=金蝶K3V10.1
    /// </summary>
    [SugarColumn(ColumnName = "FDATASOURCETYPE")]
    public int Fdatasourcetype { get; set; }

    /// <summary>
    /// 脚本类型 1(View) 2(Procedure)
    /// </summary>
    [SugarColumn(ColumnName = "FSCRIPTETYPE")]
    public int Fscriptetype { get; set; }

    /// <summary>
    /// 脚本描述
    /// </summary>
    [SugarColumn(ColumnName = "FSCRIPTEMODE")]
    public string Fscriptemode { get; set; } = string.Empty;

    /// <summary>
    /// 脚本名称
    /// </summary>
    [SugarColumn(ColumnName = "FSCRIPTENAME")]
    public string Fscriptename { get; set; } = string.Empty;

    /// <summary>
    /// 视图、存储过程SQL
    /// </summary>
    [SugarColumn(ColumnName = "FVIEWPROCEDURE")]
    public string Fviewprocedure { get; set; } = string.Empty;

    /// <summary>
    /// 执行优先级 越小越高
    /// </summary>
    [SugarColumn(ColumnName = "FPRIORITY")]
    public int Fpriority { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
