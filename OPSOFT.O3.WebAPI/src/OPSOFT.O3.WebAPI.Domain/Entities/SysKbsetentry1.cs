using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 看板配置表-面板信息
/// </summary>
[SugarTable("SYS_KBSETENTRY1")]
public class SysKbsetentry1 : BaseEntity
{
    /// <summary>
    /// 面板所在行(从零开始
    /// </summary>
    [SugarColumn(ColumnName = "FROW")]
    public int Frow { get; set; }

    /// <summary>
    /// 面板所在列(从零开始)
    /// </summary>
    [SugarColumn(ColumnName = "FCOLUMN", IsNullable = true)]
    public int? FCOLUMN { get; set; }

    /// <summary>
    /// 面板标题
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE", IsNullable = true)]
    public string FTITLE { get; set; } = string.Empty;

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
    /// 显示方向(0=水平;1=垂直)
    /// </summary>
    [SugarColumn(ColumnName = "FORIENTATION", IsNullable = true)]
    public string FORIENTATION { get; set; } = string.Empty;

    /// <summary>
    /// 合并列数
    /// </summary>
    [SugarColumn(ColumnName = "FCOLUMNSPAN", IsNullable = true)]
    public int? FCOLUMNSPAN { get; set; }

    /// <summary>
    /// 是否显示标题栏
    /// </summary>
    [SugarColumn(ColumnName = "FHASTITLE", IsNullable = true)]
    public bool? FHASTITLE { get; set; }

    /// <summary>
    /// 合并行数
    /// </summary>
    [SugarColumn(ColumnName = "FROWSPAN", IsNullable = true)]
    public int? FROWSPAN { get; set; }
}
